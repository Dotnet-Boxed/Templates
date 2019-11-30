namespace ApiTemplate
{
    using System;
    using System.Linq;
#if Versioning
    using System.Reflection;
#endif
    using ApiTemplate.Constants;
    using ApiTemplate.Options;
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
#if Versioning
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.AspNetCore.Routing;
#endif
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
                    var endpoint = httpContext.GetEndpoint();
                    var routeName = endpoint?.Metadata?.GetMetadata<RouteNameMetadata>()?.RouteName;
                    diagnosticContext.Set("RouteName", routeName);

                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                });

#if Swagger

        public static IApplicationBuilder UseCustomSwaggerUI(this IApplicationBuilder application) =>
            application.UseSwaggerUI(
                options =>
                {
                    // Set the Swagger UI browser document title.
                    options.DocumentTitle = typeof(Startup)
                        .Assembly
                        .GetCustomAttribute<AssemblyProductAttribute>()
                        .Product;
                    // Set the Swagger UI to render at '/'.
                    options.RoutePrefix = string.Empty;

                    options.DisplayOperationId();
                    options.DisplayRequestDuration();

#if Versioning
                    var provider = application.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
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
}
