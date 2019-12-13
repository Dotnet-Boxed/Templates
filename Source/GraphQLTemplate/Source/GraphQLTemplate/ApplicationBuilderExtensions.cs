namespace GraphQLTemplate
{
    using System;
    using System.Linq;
    using Boxed.AspNetCore;
    using GraphQLTemplate.Constants;
    using GraphQLTemplate.Options;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;

    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Uses the static files middleware to serve static files. Also adds the Cache-Control and Pragma HTTP
        /// headers. The cache duration is controlled from configuration.
        /// See http://andrewlock.net/adding-cache-control-headers-to-static-files-in-asp-net-core/.
        /// </summary>
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
        public static IApplicationBuilder UseCustomSerilogRequestLogging(this IApplicationBuilder application) =>
            application.UseSerilogRequestLogging(
                options => options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    var clientName = httpContext.Request.Headers["apollographql-client-name"];
                    if (clientName.Any())
                    {
                        diagnosticContext.Set("ClientName", clientName);
                    }

                    var clientVersion = httpContext.Request.Headers["apollographql-client-version"];
                    if (clientVersion.Any())
                    {
                        diagnosticContext.Set("ClientVersion", clientVersion);
                    }

                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                });
    }
}
