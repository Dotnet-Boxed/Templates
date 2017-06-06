namespace MvcBoilerplate
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    // $Start-Services$
    using MvcBoilerplate.Services;
    // $End-Services$
    using MvcBoilerplate.Settings;

    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures the anti-forgery tokens for better security. See:
        /// http://www.asp.net/mvc/overview/security/xsrfcsrf-prevention-in-aspnet-mvc-and-web-pages
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        public static IServiceCollection AddAntiforgerySecurely(this IServiceCollection services) =>
            services.AddAntiforgery(
                options =>
                {
                    // Rename the Anti-Forgery cookie from "__RequestVerificationToken" to "f". This adds a little
                    // security through obscurity and also saves sending a few characters over the wire.
                    options.CookieName = "f";

                    // Rename the form input name from "__RequestVerificationToken" to "f" for the same reason above
                    // e.g. <input name="__RequestVerificationToken" type="hidden" value="..." />
                    options.FormFieldName = "f";

                    // Rename the Anti-Forgery HTTP header from RequestVerificationToken to X-XSRF-TOKEN. X-XSRF-TOKEN
                    // is not a standard but a common name given to this HTTP header popularized by Angular.
                    options.HeaderName = "X-XSRF-TOKEN";
                    // $Start-HttpsEverywhere$

                    // If you have enabled SSL/TLS. Uncomment this line to ensure that the Anti-Forgery cookie requires
                    // SSL /TLS to be sent across the wire.
                    options.RequireSsl = true;
                    // $End-HttpsEverywhere$
                });

        /// <summary>
        /// Configures caching for the application. Registers the <see cref="IDistrbutedCache"/> and
        /// <see cref="IMemoryCache"/> types with the services collection or IoC container. The
        /// <see cref="IDistrbutedCache"/> is intended to be used in cloud hosted scenarios where there is a shared
        /// cache, which is shared between multiple instances of the application. Use the <see cref="IMemoryCache"/>
        /// otherwise.
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        public static IServiceCollection AddCaching(this IServiceCollection services) =>
            services
                // Adds IMemoryCache which is a simple in-memory cache.
                .AddMemoryCache()
                // Adds IDistributedCache which is a distributed cache shared between multiple servers. This adds a
                // default implementation of IDistributedCache which is not distributed. See below:
                .AddDistributedMemoryCache();
                // Uncomment the following line to use the Redis implementation of IDistributedCache. This will
                // override any previously registered IDistributedCache service.
                // Redis is a very fast cache provider and the recommended distributed cache provider.
                // .AddDistributedRedisCache(
                //     options =>
                //     {
                //     });
                // Uncomment the following line to use the Microsoft SQL Server implementation of IDistributedCache.
                // Note that this would require setting up the session state database.
                // Redis is the preferred cache implementation but you can use SQL Server if you don't have an alternative.
                // .AddSqlServerCache(
                //     x =>
                //     {
                //         x.ConnectionString = "Server=.;Database=ASPNET5SessionState;Trusted_Connection=True;";
                //         x.SchemaName = "dbo";
                //         x.TableName = "Sessions";
                //     });

        /// <summary>
        /// Configures the settings by binding the contents of the config.json file to the specified Plain Old CLR
        /// Objects (POCO) and adding <see cref="IOptionsSnapshot{}"/> objects to the services collection.
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        /// <param name="configuration">Gets or sets the application configuration, where key value pair settings are
        /// stored.</param>
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration) =>
            services
                // Adds IOptionsSnapshot<AppSettings> to the services container.
                .Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)))
                // $Start-Sitemap$
                // Adds IOptionsSnapshot<SitemapSettings> to the services container.
                .Configure<SitemapSettings>(configuration.GetSection(nameof(SitemapSettings)))
                // $End-Sitemap$
                // Adds IOptionsSnapshot<CacheProfileSettings> to the services container.
                .Configure<CacheProfileSettings>(configuration.GetSection(nameof(CacheProfileSettings)));

        // $Start-CORS$
        /// <summary>
        /// Add cross-origin resource sharing (CORS) services and configures named CORS policies. See
        /// https://docs.asp.net/en/latest/security/cors.html
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        public static IServiceCollection AddCorsPolicies(IServiceCollection services) =>
            services.AddCors(
                options =>
                {
                    options.AddPolicy(
                        options.DefaultPolicyName,
                        x =>
                        {
                            x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials();
                        });
                    options.AddPolicy(
                        "MyCustomPolicy",
                        x =>
                        {
                        });
                });

        // $End-CORS$
        /// <summary>
        /// Configures custom services to add to the ASP.NET Core Injection of Control (IoC) container.
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            // $Start-Windows81IE11EdgeFavicon$
            services.AddScoped<IBrowserConfigService, BrowserConfigService>();
            // $End-Windows81IE11EdgeFavicon$
            // $Start-Feed$
            // $Start-TargetFramework-NetFramework$
#if NET461
            // The FeedService is not available for .NET Core because the System.ServiceModel.Syndication.SyndicationFeed
            // type does not yet exist. See https://github.com/dotnet/wcf/issues/76.
            // $End-TargetFramework-NetFramework$
            services.AddScoped<IFeedService, FeedService>();
            // $Start-TargetFramework-NetFramework$
#endif
            // $End-TargetFramework-NetFramework$
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

            return services;
        }
    }
}
