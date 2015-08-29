namespace MvcBoilerplate
{
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Hosting;

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
                application.UseErrorPage();

                // When a database error occurs, displays a detailed error page with full diagnostic information. It is 
                // unsafe to use this in production. Uncomment this if using a database.
                // application.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
            }
        }
    }
}