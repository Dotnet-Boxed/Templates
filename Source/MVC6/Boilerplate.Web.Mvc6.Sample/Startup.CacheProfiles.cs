namespace MvcBoilerplate
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Extensions.Configuration;
    using MvcBoilerplate.Settings;

    public partial class Startup
    {
        /// <summary>
        /// Controls how controller actions cache content in one central location.
        /// </summary>
        /// <param name="cacheProfiles">The settings for the <see cref="ResponseCacheAttribute"/>'s.</param>
        /// <param name="configuration">Gets or sets the application configuration, where key value pair settings are 
        /// stored.</param>
        private static void ConfigureCacheProfiles(IDictionary<string, CacheProfile> cacheProfiles, IConfiguration configuration)
        {
            var configurationSection = configuration.GetSection(nameof(CacheProfileSettings));
            CacheProfileSettings cacheProfileSettings = new CacheProfileSettings();
            ConfigurationBinder.Bind(configurationSection, cacheProfileSettings);

            foreach (KeyValuePair<string, CacheProfile> keyValuePair in cacheProfileSettings.CacheProfiles)
            {
                cacheProfiles.Add(keyValuePair);
            }
        }
    }
}
