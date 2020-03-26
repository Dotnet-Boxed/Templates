namespace GraphQLTemplate
{
    using System;
    using System.Linq;
    using Boxed.AspNetCore;
    using GraphQLTemplate.Constants;
    using GraphQLTemplate.Options;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
#if HealthCheck
    using Serilog.Events;
#endif

    internal static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Uses the static files middleware to serve static files. Also adds the Cache-Control and Pragma HTTP
        /// headers. The cache duration is controlled from configuration.
        /// See http://andrewlock.net/adding-cache-control-headers-to-static-files-in-asp-net-core/.
        /// </summary>
        /// <param name="application">The application builder.</param>
        /// <returns>The application builder with the static files middleware configured.</returns>
        public static IApplicationBuilder UseStaticFilesWithCacheControl(this IApplicationBuilder application)
        {
            var cacheProfile = application
                .ApplicationServices
                .GetRequiredService<CacheProfileOptions>()
                .Where(x => string.Equals(x.Key, CacheProfileName.StaticFiles, StringComparison.Ordinal))
                .Select(x => x.Value)
                .SingleOrDefault();
            return application
                .UseStaticFiles(
                    new StaticFileOptions()
                    {
                        OnPrepareResponse = context => context.Context.ApplyCacheProfile(cacheProfile),
                    });
        }

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

                        if (endpoint is object)
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
                        if (endpoint is object)
                        {
                            return endpoint.DisplayName == "Health checks";
                        }

                        return false;
                    }
#endif
                });
    }
}
