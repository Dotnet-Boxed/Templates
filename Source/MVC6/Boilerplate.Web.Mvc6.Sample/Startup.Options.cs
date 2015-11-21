namespace MvcBoilerplate
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MvcBoilerplate.Settings;

    public partial class Startup
    {
        /// <summary>
        /// Configures the settings by binding the contents of the config.json file to the specified Plain Old CLR 
        /// Objects (POCO) and adding <see cref="IOptions{}"/> objects to the services collection.
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        /// <param name="configuration">Gets or sets the application configuration, where key value pair settings are 
        /// stored.</param>
        private static void ConfigureOptionsServices(IServiceCollection services, IConfiguration configuration)
        {
            // Adds IOptions<AppSettings> to the services container.
            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));

            // Adds IOptions<CacheProfileSettings> to the services container.
            services.Configure<CacheProfileSettings>(configuration.GetSection(nameof(CacheProfileSettings)));
            // $Start-Sitemap$

            // Adds IOptions<SitemapSettings> to the services container.
            services.Configure<SitemapSettings>(configuration.GetSection(nameof(SitemapSettings)));
            // $End-Sitemap$
        }
    }
}