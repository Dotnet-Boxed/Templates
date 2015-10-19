namespace MvcBoilerplate
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Hosting;
    using Microsoft.AspNet.Http;

    public partial class Startup
    {
        /// <summary>
        /// Configures the error pages for the application.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="environment">The environment the application is running under. This can be Development, 
        /// Staging or Production by default.</param>
        private static void ConfigureErrorPages(
            IApplicationBuilder application,
            IHostingEnvironment environment)
        {
            // Add the following to the request pipeline only in the development environment.
            if (environment.IsDevelopment())
            {
                // When an error occurs, displays a detailed error page with full diagnostic information. It is unsafe
                // to use this in production. See http://docs.asp.net/en/latest/fundamentals/diagnostics.html
                application.UseDeveloperExceptionPage();

                // When a database error occurs, displays a detailed error page with full diagnostic information. It is 
                // unsafe to use this in production. Uncomment this if using a database.
                // application.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
            }
            else // Add the following to the request pipeline only in the staging or production environments.
            {
                // Add error handling middle-ware which handles all HTTP status codes from 400 to 599 by re-executing
                // the request pipeline for the following URL. '{0}' is the HTTP status code e.g. 404.
                application.UseStatusCodePagesWithReExecute("/error/{0}");
            }
        }

        /// <summary>
        /// Configures the 404 Not Found error page for the application. Used when no other route matches.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="environment">The environment the application is running under. This can be Development, 
        /// Staging or Production by default.</param>
        private static void Configure404NotFoundErrorPage(
            IApplicationBuilder application,
            IHostingEnvironment environment)
        {
            // Add the following to the request pipeline only in the staging or production environments.
            if (!environment.IsDevelopment())
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
}