namespace ApiTemplate;

#if Versioning
using Microsoft.AspNetCore.Mvc.ApiExplorer;
#endif
#if Serilog
using Serilog;
#if HealthCheck
using Serilog.Events;
#endif
#endif

internal static class ApplicationBuilderExtensions
{
#if Serilog
    /// <summary>
    /// Uses custom serilog request logging. Adds additional properties to each log.
    /// See https://github.com/serilog/serilog-aspnetcore.
    /// </summary>
    /// <param name="application">The application builder.</param>
    /// <returns>The application builder with the Serilog middleware configured.</returns>
    public static IApplicationBuilder UseCustomSerilogRequestLogging(this IApplicationBuilder application) =>
        application.UseSerilogRequestLogging(
            options =>
            {
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    var request = httpContext.Request;
                    var response = httpContext.Response;
                    var endpoint = httpContext.GetEndpoint();

                    diagnosticContext.Set("Host", request.Host);
                    diagnosticContext.Set("Protocol", request.Protocol);
                    diagnosticContext.Set("Scheme", request.Scheme);

                    if (request.QueryString.HasValue)
                    {
                        diagnosticContext.Set("QueryString", request.QueryString.Value);
                    }

                    if (endpoint is not null)
                    {
                        diagnosticContext.Set("EndpointName", endpoint.DisplayName);
                    }

                    diagnosticContext.Set("ContentType", response.ContentType);
                };
#if HealthCheck
                options.GetLevel = GetLevel;

                static LogEventLevel GetLevel(HttpContext httpContext, double elapsedMilliseconds, Exception exception)
                {
                    if (exception == null && httpContext.Response.StatusCode <= 499)
                    {
                        if (IsHealthCheckEndpoint(httpContext))
                        {
                            return LogEventLevel.Verbose;
                        }

                        return LogEventLevel.Information;
                    }

                    return LogEventLevel.Error;
                }

                static bool IsHealthCheckEndpoint(HttpContext httpContext)
                {
                    var endpoint = httpContext.GetEndpoint();
                    if (endpoint is not null)
                    {
                        return endpoint.DisplayName == "Health checks";
                    }

                    return false;
                }
#endif
            });
#endif
#if Swagger

    public static IApplicationBuilder UseCustomSwaggerUI(this IApplicationBuilder application) =>
        application.UseSwaggerUI(
            options =>
            {
                // Set the Swagger UI browser document title.
                options.DocumentTitle = AssemblyInformation.Current.Product;
                // Set the Swagger UI to render at '/'.
                options.RoutePrefix = string.Empty;

                options.DisplayOperationId();
                options.DisplayRequestDuration();

#if Versioning
                var provider = application.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var apiVersionDescription in provider
                    .ApiVersionDescriptions
                    .OrderByDescending(x => x.ApiVersion))
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                        $"Version {apiVersionDescription.ApiVersion}");
                }
#else
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Version 1");
#endif
            });
#endif
}
