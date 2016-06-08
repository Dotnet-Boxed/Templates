namespace Boilerplate.AspNetCore
{
    using Boilerplate.AspNetCore.Middleware;
    using Microsoft.AspNetCore.Builder;

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
    }
}
