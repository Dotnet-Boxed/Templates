namespace MvcBoilerplate
{
    using Microsoft.Framework.DependencyInjection;
    using MvcBoilerplate.Services;

    public partial class Startup
    {
        /// <summary>
        /// Configures custom services to add to the ASP.NET MVC 6 Injection of Control (IoC) container.
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        private static void ConfigureCustomServices(IServiceCollection services)
        {
#if DNX451
            // The FeedService is not available for .NET Core because the System.ServiceModel.Syndication.SyndicationFeed 
            // type does not yet exist. See https://github.com/dotnet/wcf/issues/76.
            services.AddScoped<IFeedService, FeedService>();
#endif
            services.AddSingleton<ILoggingService, LoggingService>();
            services.AddScoped<IOpenSearchService, OpenSearchService>();
            services.AddScoped<IRobotsService, RobotsService>();
            services.AddScoped<ISitemapService, SitemapService>();
            services.AddScoped<ISitemapPingerService, SitemapPingerService>();

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
