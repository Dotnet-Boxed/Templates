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
        private const string ServerHttpHeaderName = "Server";

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

        /// <summary>
        /// Removes the Server HTTP header from the HTTP response for marginally better security and performance.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <returns>The application.</returns>
        public static IApplicationBuilder RemoveServerHttpHeader(this IApplicationBuilder application)
        {
            return application.Use(
                (context, next) =>
                {
                    context.Response.Headers.Remove(ServerHttpHeaderName);
                    return next();
                });
        }
    }
}
