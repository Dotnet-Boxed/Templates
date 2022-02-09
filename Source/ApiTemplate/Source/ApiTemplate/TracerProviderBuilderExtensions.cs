namespace ApiTemplate;

using System.Diagnostics;
using ApiTemplate.Constants;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

public static class TracerProviderBuilderExtensions
{
    public static TracerProviderBuilder AddCustomTracing(
        this TracerProviderBuilder builder,
        IWebHostEnvironment webHostEnvironment) =>
        builder
            .SetResourceBuilder(GetResourceBuilder(webHostEnvironment))
            .AddAspNetCoreInstrumentation(
                options =>
                {
                    options.Enrich = Enrich;
                    options.RecordException = true;
                })
            .AddConsoleExporter(
                options => options.Targets = ConsoleExporterOutputTargets.Console | ConsoleExporterOutputTargets.Debug);

    public static TracerProviderBuilder AddIf(
        this TracerProviderBuilder builder,
        bool condition,
        Func<TracerProviderBuilder, TracerProviderBuilder> action)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(action);

        if (condition)
        {
            builder = action(builder);
        }

        return builder;
    }

    private static ResourceBuilder GetResourceBuilder(IWebHostEnvironment webHostEnvironment) =>
        ResourceBuilder
            .CreateEmpty()
            .AddService(
                webHostEnvironment.ApplicationName,
                serviceVersion: AssemblyInformation.Current.Version)
            .AddAttributes(
                new KeyValuePair<string, object>[]
                {
                    new(OpenTelemetryAttributeName.Deployment.Environment, webHostEnvironment.EnvironmentName),
                    new(OpenTelemetryAttributeName.Host.Name, Environment.MachineName),
                })
            .AddEnvironmentVariableDetector();

    /// <summary>
    /// Enrich spans with additional request and response meta data.
    /// See https://github.com/open-telemetry/opentelemetry-specification/blob/master/specification/trace/semantic_conventions/http.md.
    /// </summary>
    private static void Enrich(Activity activity, string eventName, object obj)
    {
        if (obj is HttpRequest request)
        {
            var context = request.HttpContext;
            activity.AddTag(OpenTelemetryAttributeName.Http.Flavor, OpenTelemetryHttpFlavour.GetHttpFlavour(request.Protocol));
            activity.AddTag(OpenTelemetryAttributeName.Http.Scheme, request.Scheme);
            activity.AddTag(OpenTelemetryAttributeName.Http.ClientIP, context.Connection.RemoteIpAddress);
            activity.AddTag(OpenTelemetryAttributeName.Http.RequestContentLength, request.ContentLength);
            activity.AddTag(OpenTelemetryAttributeName.Http.RequestContentType, request.ContentType);

            var user = context.User;
            if (user.Identity?.Name is not null)
            {
                activity.AddTag(OpenTelemetryAttributeName.EndUser.Id, user.Identity.Name);
                activity.AddTag(OpenTelemetryAttributeName.EndUser.Scope, string.Join(',', user.Claims.Select(x => x.Value)));
            }
        }
        else if (obj is HttpResponse response)
        {
            activity.AddTag(OpenTelemetryAttributeName.Http.ResponseContentLength, response.ContentLength);
            activity.AddTag(OpenTelemetryAttributeName.Http.ResponseContentType, response.ContentType);
        }
    }
}
