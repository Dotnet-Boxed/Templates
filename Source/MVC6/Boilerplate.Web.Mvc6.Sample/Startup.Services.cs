namespace MvcBoilerplate
{
    using Microsoft.Extensions.DependencyInjection;
    // $Start-Services$
    using MvcBoilerplate.Services;
    // $End-Services$

    public partial class Startup
    {
        /// <summary>
        /// Configures custom services to add to the ASP.NET MVC 6 Injection of Control (IoC) container.
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        private static void ConfigureCustomServices(IServiceCollection services)
        {
            // $Start-Windows81IE11EdgeFavicon$
            services.AddScoped<IBrowserConfigService, BrowserConfigService>();
            // $End-Windows81IE11EdgeFavicon$
            // $Start-Feed$
#if DNX451
            // The FeedService is not available for .NET Core because the System.ServiceModel.Syndication.SyndicationFeed 
            // type does not yet exist. See https://github.com/dotnet/wcf/issues/76.
            services.AddScoped<IFeedService, FeedService>();
#endif
            // $End-Feed$
            // $Start-AndroidChromeM39Favicons$
            services.AddScoped<IManifestService, ManifestService>();
            // $End-AndroidChromeM39Favicons$
            // $Start-Search$
            services.AddScoped<IOpenSearchService, OpenSearchService>();
            // $End-Search$
            // $Start-RobotsText$
            services.AddScoped<IRobotsService, RobotsService>();
            // $End-RobotsText$
            // $Start-Sitemap$
            services.AddScoped<ISitemapService, SitemapService>();
            services.AddScoped<ISitemapPingerService, SitemapPingerService>();
            // $End-Sitemap$

            // Add your own custom services here e.g.

            // Singleton - Only one instance is ever created and returned.
            // services.AddSingleton<IExampleService, ExampleService>();

            // Scoped - A new instance is created and returned for each request/response cycle.
            // services.AddScoped<IExampleService, ExampleService>();

            // Transient - A new instance is created and returned each time.
            // services.AddTransient<IExampleService, ExampleService>();
        }
    }
}
