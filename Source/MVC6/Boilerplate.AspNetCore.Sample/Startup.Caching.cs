namespace MvcBoilerplate
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MvcBoilerplate.Settings;

    public partial class Startup
    {
        /// <summary>
        /// Configures caching for the application. Registers the <see cref="IDistrbutedCache"/> and
        /// <see cref="IMemoryCache"/> types with the services collection or IoC container. The
        /// <see cref="IDistrbutedCache"/> is intended to be used in cloud hosted scenarios where there is a shared
        /// cache, which is shared between multiple instances of the application. Use the <see cref="IMemoryCache"/>
        /// otherwise.
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        private static void ConfigureCachingServices(IServiceCollection services)
        {
            // Adds IMemoryCache which is a simple in-memory cache.
            services.AddMemoryCache();

            // Adds IDistributedCache which is a distributed cache shared between multiple servers. This adds a default
            // implementation of IDistributedCache which is not distributed. See below.
            services.AddDistributedMemoryCache();

            // Uncomment the following line to use the Redis implementation of IDistributedCache. This will override
            // any previously registered IDistributedCache service.
            // Redis is a very fast cache provider and the recommended distributed cache provider.
            // services.AddTransient<IDistributedCache, RedisCache>();

            // Uncomment the following line to use the Microsoft SQL Server implementation of IDistributedCache.
            // Note that this would require setting up the session state database.
            // Redis is the preferred cache implementation but you can use SQL Server if you don't have an alternative.
            // services.AddSqlServerCache(
            //     x =>
            //     {
            //         x.ConnectionString = "Server=.;Database=ASPNET5SessionState;Trusted_Connection=True;";
            //         x.SchemaName = "dbo";
            //         x.TableName = "Sessions";
            //     });
        }

        /// <summary>
        /// Controls how controller actions cache content in one central location.
        /// </summary>
        /// <param name="cacheProfiles">The settings for the <see cref="ResponseCacheAttribute"/>'s.</param>
        /// <param name="configuration">Gets or sets the application configuration, where key value pair settings are
        /// stored.</param>
        private static void ConfigureCacheProfiles(
            IDictionary<string, CacheProfile> cacheProfiles,
            IConfiguration configuration)
        {
            CacheProfileSettings cacheProfileSettings = new CacheProfileSettings();
            configuration.GetSection(nameof(CacheProfileSettings)).Bind(cacheProfileSettings);

            foreach (KeyValuePair<string, CacheProfile> keyValuePair in cacheProfileSettings.CacheProfiles)
            {
                cacheProfiles.Add(keyValuePair);
            }
        }
    }
}
