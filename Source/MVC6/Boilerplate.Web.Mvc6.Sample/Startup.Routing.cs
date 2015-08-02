namespace MvcBoilerplate
{
    using Microsoft.AspNet.Routing;

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

            // TODO: IgnoreRoute does not yet exist in MVC 6.

            //            // IgnoreRoute - Tell the routing system to ignore certain routes for better performance.
            //            // Ignore .axd files.
            //            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //            // Ignore everything in the Content folder.
            //            routes.IgnoreRoute("Content/{*pathInfo}");
            //            // Ignore everything in the Scripts folder.
            //            routes.IgnoreRoute("Scripts/{*pathInfo}");
            //            // Ignore the Forbidden.html file.
            //            routes.IgnoreRoute("Error/Forbidden.html");
            //            // Ignore the GatewayTimeout.html file.
            //            routes.IgnoreRoute("Error/GatewayTimeout.html");
            //            // Ignore the ServiceUnavailable.html file.
            //            routes.IgnoreRoute("Error/ServiceUnavailable.html");
            //            // Ignore the humans.txt file.
            //            routes.IgnoreRoute("humans.txt");
        }
    }
}
