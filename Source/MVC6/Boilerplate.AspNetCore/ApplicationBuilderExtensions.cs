namespace Boilerplate.AspNetCore
{
    using System;
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
        /// Executes the specified action if the specified <paramref name="condition"/> is <c>true</c> which can be
        /// used to conditionally add to the request execution pipeline.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="condition">If set to <c>true</c> the action is executed.</param>
        /// <param name="action">The action used to add to the request execution pipeline.</param>
        /// <returns>The application.</returns>
        public static IApplicationBuilder UseIf(
            this IApplicationBuilder application,
            bool condition,
            Func<IApplicationBuilder, IApplicationBuilder> action)
        {
            if (condition)
            {
                application = action(application);
            }

            return application;
        }

        /// <summary>
        /// Executes the specified <paramref name="ifAction"/> if the specified <paramref name="condition"/> is
        /// <c>true</c>, otherwise executes the <paramref name="elseAction"/>. This can be used to conditionally add to
        /// the request execution pipeline.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="condition">If set to <c>true</c> the <paramref name="ifAction"/> is executed, otherwise the
        /// <paramref name="elseAction"/> is executed.</param>
        /// <param name="ifAction">The action used to add to the request execution pipeline if the condition is
        /// <c>true</c>.</param>
        /// <param name="elseAction">The action used to add to the request execution pipeline if the condition is
        /// <c>false</c>.</param>
        /// <returns>The application.</returns>
        public static IApplicationBuilder UseIfElse(
            this IApplicationBuilder application,
            bool condition,
            Func<IApplicationBuilder, IApplicationBuilder> ifAction,
            Func<IApplicationBuilder, IApplicationBuilder> elseAction)
        {
            if (condition)
            {
                application = ifAction(application);
            }
            else
            {
                application = elseAction(application);
            }

            return application;
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
