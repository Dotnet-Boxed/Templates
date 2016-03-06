namespace Boilerplate.Web.Mvc
{
    using System.Threading.Tasks;
    using Boilerplate.Web.Mvc.Middleware;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Http;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// <see cref="IApplicationBuilder"/> extension methods.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Allows the use of <see cref="HttpException"/> as an alternative method of returning an error result.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <returns>The application.</returns>
        public static IApplicationBuilder UseHttpException(this IApplicationBuilder application)
        {
            return application.UseMiddleware<HttpExceptionMiddleware>();
        }

        /// <summary>
        /// Returns a 500 Internal Server Error response when an unhandled exception occurs.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <returns>The application.</returns>
        public static IApplicationBuilder UseInternalServerErrorOnException(this IApplicationBuilder application)
        {
            return application.UseMiddleware<InternalServerErrorOnExceptionMiddleware>();
        }

        /// <summary>
        /// Removes the Server HTTP header from the HTTP response for marginally better security and performance.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <returns>The application.</returns>
        public static IApplicationBuilder UseNoServerHttpHeader(this IApplicationBuilder application)
        {
            return application.UseMiddleware<NoServerHttpHeaderMiddleware>();
        }

        /// <summary>
        /// Configures the <see cref="UrlHelperExtensions"/> with the <see cref="IHttpContextAccessor"/>, so that they
        /// have access to the current <see cref="HttpContext"/> and can get the current request path.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <returns>The application.</returns>
        public static IApplicationBuilder UseUrlHelperExtensions(this IApplicationBuilder application)
        {
            IHttpContextAccessor httpContextAccessor =
                application.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            Context.Configure(httpContextAccessor);
            return application;
        }

        /// <summary>
        /// Runs a terminal middleware that returns a 404 Not Found response.
        /// </summary>
        /// <param name="application">The application.</param>
        public static void RunNotFound(this IApplicationBuilder application)
        {
            application.Run(
                context =>
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    return Task.FromResult<object>(null);
                });
        }
    }
}
