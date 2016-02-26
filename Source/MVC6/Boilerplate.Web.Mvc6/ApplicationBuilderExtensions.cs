namespace Boilerplate.Web.Mvc
{
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Http;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// <see cref="IApplicationBuilder"/> extension methods.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configures the <see cref="UrlHelperExtensions"/> with the <see cref="IHttpContextAccessor"/>, so that they
        /// have access to the current <see cref="HttpContext"/> and can get the current request path.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <returns>The application.</returns>
        public static IApplicationBuilder UseBoilerplate(this IApplicationBuilder application)
        {
            IHttpContextAccessor httpContextAccessor = 
                application.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            Context.Configure(httpContextAccessor);
            return application;
        }
    }
}
