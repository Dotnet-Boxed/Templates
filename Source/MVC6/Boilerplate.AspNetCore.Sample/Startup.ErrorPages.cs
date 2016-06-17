namespace MvcBoilerplate
{
    using Boilerplate.AspNetCore;
    using Microsoft.AspNetCore.Builder;

    public partial class Startup
    {
        /// <summary>
        /// Adds developer friendly error pages for the application which contain extra debug and exception information.
        /// Note: It is unsafe to use this in production.
        /// </summary>
        /// <param name="application">The application.</param>
        private static void UseDeveloperErrorPages(IApplicationBuilder application)
        {
            // When an error occurs, displays a detailed error page with full diagnostic information.
            // See http://docs.asp.net/en/latest/fundamentals/diagnostics.html
            application.UseDeveloperExceptionPage();

            // When a database error occurs, displays a detailed error page with full diagnostic information. It is
            // unsafe to use this in production. Uncomment this if using a database.
            // application.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
        }

        /// <summary>
        /// Adds user friendly error pages.
        /// </summary>
        /// <param name="application">The application.</param>
        private static void UseErrorPages(IApplicationBuilder application)
        {
            // Add error handling middle-ware which handles all HTTP status codes from 400 to 599 by re-executing
            // the request pipeline for the following URL. '{0}' is the HTTP status code e.g. 404.
            application.UseStatusCodePagesWithReExecute("/error/{0}/");

            // Returns a 500 Internal Server Error response when an unhandled exception occurs.
            application.UseInternalServerErrorOnException();
            // $Start-HttpException$

            // Allows the use of HttpException as an alternative method of returning an error result.
            application.UseHttpException();
            // $End-HttpException$
        }
    }
}