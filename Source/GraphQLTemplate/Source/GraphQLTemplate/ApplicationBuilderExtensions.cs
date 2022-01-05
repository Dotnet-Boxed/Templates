namespace GraphQLTemplate;

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

                    var clientName = request.Headers["apollographql-client-name"];
                    if (clientName.Any())
                    {
                        diagnosticContext.Set("ClientName", clientName);
                    }

                    var clientVersion = request.Headers["apollographql-client-version"];
                    if (clientVersion.Any())
                    {
                        diagnosticContext.Set("ClientVersion", clientVersion);
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
}
