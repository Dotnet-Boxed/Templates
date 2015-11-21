namespace Boilerplate.Web.Mvc
{
    using Microsoft.AspNet.Hosting;
    using Microsoft.AspNet.Http;

    /// <summary>
    /// Holds an instance of the <see cref="IHttpContextAccessor"/>, which contains information about the current 
    /// context.
    /// </summary>
    internal static class Context
    {
        private static IHostingEnvironment HostingEnvironment;
        private static IHttpContextAccessor HttpContextAccessor;

        /// <summary>
        /// Configures the <see cref="Context"/>.
        /// </summary>
        /// <param name="hostingEnvironment">The environment the application is running under. This can be Development, 
        /// Staging or Production by default.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public static void Configure(
            IHostingEnvironment hostingEnvironment,
            IHttpContextAccessor httpContextAccessor)
        {
            HostingEnvironment = hostingEnvironment;
            HttpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets the current <see cref="IHostingEnvironment"/>.
        /// </summary>
        public static IHostingEnvironment Environment
        {
            get { return HostingEnvironment; }
        }

        /// <summary>
        /// Gets the current <see cref="HttpContext"/>.
        /// </summary>
        public static HttpContext HttpContext
        {
            get { return HttpContextAccessor.HttpContext; }
        }
    }
}
