namespace MvcBoilerplate
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;
    using MvcBoilerplate.Constants;

    public partial class Startup
    {
        /// <summary>
        /// Controls how controller actions cache content in one central location.
        /// </summary>
        /// <param name="cacheProfiles">The settings for the <see cref="ResponseCacheAttribute"/>'s.</param>
        private static void ConfigureCacheProfiles(IDictionary<string, CacheProfile> cacheProfiles)
        {
            // Cache the error route (/error/404/notfound) by status code for a day.
            cacheProfiles.Add(
                CacheProfileName.Error,
                new CacheProfile()
                {
                    Duration = 86400,
                    Location = ResponseCacheLocation.Any,
                    // VaryByParam = "statusCode" // TODO: Does not exist in MVC 6 yet.
                });

            // Cache the Atom 1.0 feed route (/feed) for a day.
            cacheProfiles.Add(
                CacheProfileName.Feed,
                new CacheProfile()
                {
                    Duration = 86400,
                    Location = ResponseCacheLocation.Any,
                    // VaryByParam = "none" // TODO: Does not exist in MVC 6 yet.
                });

            // Cache the open search route (/opensearch.xml) for a day.
            cacheProfiles.Add(
                CacheProfileName.OpenSearchXml,
                new CacheProfile()
                {
                    Duration = 86400,
                    Location = ResponseCacheLocation.Any,
                    // VaryByParam = "none" // TODO: Does not exist in MVC 6 yet.
                });

            // Cache the robots.txt route for a day.
            cacheProfiles.Add(
                CacheProfileName.RobotsText,
                new CacheProfile()
                {
                    Duration = 86400,
                    Location = ResponseCacheLocation.Any,
                    // VaryByParam = "none" // TODO: Does not exist in MVC 6 yet.
                });
        }
    }
}
