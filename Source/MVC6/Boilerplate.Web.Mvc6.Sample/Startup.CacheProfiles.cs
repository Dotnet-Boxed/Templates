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

            // TODO: Error pages cache profiles.
            //  <!-- Cache the 400 Bad Request route for a day. -->
            //  <add name="BadRequest" duration="86400" location="Any" varyByParam="none" />
            //  <!-- Cache the 403 Forbidden route for a day. -->
            //  <add name="Forbidden" duration="86400" location="Any" varyByParam="none" />
            //  <!-- Cache the 405 Method Not Allowed route for a day. -->
            //  <add name="MethodNotAllowed" duration="86400" location="Any" varyByParam="none" />
            //  <!-- Cache the 404 Not Found route for a day. -->
            //  <add name="NotFound" duration="86400" location="Any" varyByParam="none" />
            //  <!-- Cache the 401 Unauthorized route for a day. -->
            //  <add name="Unauthorized" duration="86400" location="Any" varyByParam="none" />
            //  <!-- Cache the 500 Internal Server Error route for a day. -->
            //  <add name="InternalServerError" duration="86400" location="Any" varyByParam="none" />
        }
    }
}
