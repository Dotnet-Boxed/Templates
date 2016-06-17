namespace MvcBoilerplate
{
    using Microsoft.AspNetCore.Routing;

    public partial class Startup
    {
        /// <summary>
        /// Improve SEO by stopping duplicate URL's due to case differences or trailing slashes.
        /// See http://googlewebmastercentral.blogspot.co.uk/2010/04/to-slash-or-not-to-slash.html
        /// </summary>
        /// <param name="routeOptions">The routing options.</param>
        private static void ConfigureRouting(RouteOptions routeOptions)
        {
            // All generated URL's should append a trailing slash.
            routeOptions.AppendTrailingSlash = true;

            // All generated URL's should be lower-case.
            routeOptions.LowercaseUrls = true;
        }
    }
}
