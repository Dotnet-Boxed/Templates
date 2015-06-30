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
        private static IHostingEnvironment Environment;
        private static IHttpContextAccessor HttpContextAccessor;

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
            Environment = environment;
            HttpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets the current <see cref="HttpContext"/>.
        /// </summary>
        public static HttpContext HttpContext
        {
            get { return HttpContextAccessor.HttpContext; }
        }

        /// <summary>
        /// Compares the current hosting environment name against the specified value.
        /// </summary>
        /// <param name="environmentName">Environment name to validate against.</param>
        /// <returns><c>true</c> if the specified name is same as the current environment.</returns>
        public static bool IsEnvironment(string environmentName)
        {
            return Environment.IsEnvironment(environmentName);
        }
    }
}
