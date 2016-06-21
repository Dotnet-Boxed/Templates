namespace MvcBoilerplate
{
    // $Start-RedirectToCanonicalUrl$
    using Boilerplate.AspNetCore.Filters;
    // $End-RedirectToCanonicalUrl$
    using Microsoft.AspNetCore.Mvc;
    // $Start-RedirectToCanonicalUrl$
    using Microsoft.AspNetCore.Routing;
    // $End-RedirectToCanonicalUrl$
    using Microsoft.Extensions.Configuration;
    using MvcBoilerplate.Settings;

    public static partial class MvcOptionsExtensions
    {
        /// <summary>
        /// Controls how controller actions cache content in one central location.
        /// </summary>
        /// <param name="options">The ASP.NET MVC options.</param>
        /// <param name="configuration">Gets or sets the application configuration, where key value pair settings are
        /// stored.</param>
        public static MvcOptions AddCacheProfilesFromConfiguration(this MvcOptions options, IConfiguration configuration)
        {
            var cacheProfileSettings = new CacheProfileSettings();
            configuration.GetSection(nameof(CacheProfileSettings)).Bind(cacheProfileSettings);

            foreach (var keyValuePair in cacheProfileSettings.CacheProfiles)
            {
                options.CacheProfiles.Add(keyValuePair);
            }

            return options;
        }
        // $Start-RedirectToCanonicalUrl$

        /// <summary>
        /// Adds filters which help improve search engine optimization (SEO).
        /// </summary>
        public static MvcOptions AddRedirectToCanonicalUrlFilter(this MvcOptions options, RouteOptions routeOptions)
        {
            options.Filters.Add(new RedirectToCanonicalUrlAttribute(
                 appendTrailingSlash: routeOptions.AppendTrailingSlash,
                 lowercaseUrls: routeOptions.LowercaseUrls));
            return options;
        }
        // $End-RedirectToCanonicalUrl$
        // $Start-HttpsEverywhere$

        /// <summary>
        /// Require HTTPS to be used across the whole site. Also sets a custom port to use for SSL in Development. The
        /// port number to use is taken from the launchSettings.json file which Visual Studio uses to start the
        /// application using IIS Express or the command line.
        /// </summary>
        public static MvcOptions AddRequireHttpsFilter(this MvcOptions options, int? sslPort)
        {
            options.Filters.Add(new RequireHttpsAttribute());
            options.SslPort = sslPort;
            return options;
        }
        // $End-HttpsEverywhere$
    }
}
