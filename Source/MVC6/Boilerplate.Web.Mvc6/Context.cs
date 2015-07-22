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
        private static IHostingEnvironment environment;
        private static IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Configures the <see cref="Context"/>.
        /// </summary>
        /// <param name="environment">The environment the application is running under. This can be Development, 
        /// Staging or Production by default.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public static void Configure(
            IHostingEnvironment environment,
            IHttpContextAccessor httpContextAccessor)
        {
            environment = environment;
            httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets the current <see cref="IHostingEnvironment"/>.
        /// </summary>
        public static IHostingEnvironment Environment
        {
            get { return environment; }
        }

        /// <summary>
        /// Gets the current <see cref="HttpContext"/>.
        /// </summary>
        public static HttpContext HttpContext
        {
            get { return httpContextAccessor.HttpContext; }
        }
    }
}
