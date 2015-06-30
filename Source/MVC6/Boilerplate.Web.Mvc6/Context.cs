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
        private static IHttpContextAccessor HttpContextAccessor;

        /// <summary>
        /// Configures the <see cref="Context"/>.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
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
