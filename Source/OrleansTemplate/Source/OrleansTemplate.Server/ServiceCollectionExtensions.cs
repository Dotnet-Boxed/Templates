namespace OrleansTemplate.Server
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using OpenTelemetry.Exporter;
    using OpenTelemetry.Resources;
    using OpenTelemetry.Trace;
    using OrleansTemplate.Abstractions.Constants;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods which extend ASP.NET Core services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Open Telemetry services and configures instrumentation and exporters.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="webHostEnvironment">The environment the application is running under.</param>
        /// <returns>The services with open telemetry added.</returns>
        public static IServiceCollection AddCustomOpenTelemetryTracing(this IServiceCollection services, IWebHostEnvironment webHostEnvironment) =>
            services.AddOpenTelemetryTracing(
                builder =>
                {
                    builder
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(webHostEnvironment.ApplicationName))
                        .AddAspNetCoreInstrumentation(
                            options =>
                            {
                                // Enrich spans with additional request and response meta data.
                                // See https://github.com/open-telemetry/opentelemetry-specification/blob/master/specification/trace/semantic_conventions/http.md
                                options.Enrich = (activity, eventName, obj) =>
                                {
                                    if (!activity.IsAllDataRequested)
                                    {
                                        return;
                                    }

                                    if (obj is HttpRequest request)
                                    {
                                        var context = request.HttpContext;
                                        activity.AddTag(OpenTelemetryAttributeName.Http.Flavor, GetHttpFlavour(request.Protocol));
                                        activity.AddTag(OpenTelemetryAttributeName.Http.Scheme, request.Scheme);
                                        activity.AddTag(OpenTelemetryAttributeName.Http.ClientIP, context.Connection.RemoteIpAddress);
                                        activity.AddTag(OpenTelemetryAttributeName.Http.RequestContentLength, request.ContentLength);
                                        activity.AddTag(OpenTelemetryAttributeName.Http.RequestContentType, request.ContentType);

                                        var user = context.User;
                                        if (user.Identity.Name is not null)
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

                                    static string GetHttpFlavour(string protocol)
                                    {
                                        if (HttpProtocol.IsHttp10(protocol))
                                        {
                                            return "1.0";
                                        }
                                        else if (HttpProtocol.IsHttp11(protocol))
                                        {
                                            return "1.1";
                                        }
                                        else if (HttpProtocol.IsHttp2(protocol))
                                        {
                                            return "2.0";
                                        }
                                        else if (HttpProtocol.IsHttp3(protocol))
                                        {
                                            return "3.0";
                                        }

                                        throw new InvalidOperationException($"Protocol {protocol} not recognised.");
                                    }
                                };
                                options.RecordException = true;
                            });

                    if (webHostEnvironment.IsDevelopment())
                    {
                        builder.AddConsoleExporter(options => options.Targets = ConsoleExporterOutputTargets.Debug);
                    }

                    // TODO: Add OpenTelemetry.Instrumentation.* NuGet packages and configure them to collect more span data.
                    //       E.g. Add the OpenTelemetry.Instrumentation.Http package to instrument calls to HttpClient.
                    // TODO: Add OpenTelemetry.Exporter.* NuGet packages and configure them here to export open telemetry span data.
                    //       E.g. Add the OpenTelemetry.Exporter.OpenTelemetryProtocol package to export span data to Jaeger.
                });
    }
}
