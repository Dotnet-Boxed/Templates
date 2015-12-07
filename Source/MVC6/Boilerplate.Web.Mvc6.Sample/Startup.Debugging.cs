namespace MvcBoilerplate
{    
    // $Start-Glimpse$
    using Glimpse;
    // $End-Glimpse$
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Diagnostics;
    using Microsoft.AspNet.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public partial class Startup
    {
        // $Start-Glimpse$
        /// <summary>
        /// Configure tools used to help with debugging the application.
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        /// <param name="environment">The environment the application is running under. This can be Development, 
        /// Staging or Production by default.</param>
        private static void ConfigureDebuggingServices(
            IServiceCollection services,
            IHostingEnvironment environment)
        {
            // Add the following services only in development environment.
            if (environment.IsDevelopment())
            {
                // Add Glimpse to help with debugging (See http://getglimpse.com/).
                services.AddGlimpse();
            }
        }

        // $End-Glimpse$
        /// <summary>
        /// Configure tools used to help with debugging the application.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="environment">The environment the application is running under. This can be Development, 
        /// Staging or Production by default.</param>
        private static void ConfigureDebugging(
            IApplicationBuilder application,
            IHostingEnvironment environment)
        {
            // Add the following to the request pipeline only in development environment.
            if (environment.IsDevelopment())
            {
                // Browse to /runtimeinfo to see information about the runtime that is being used and the packages that 
                // are included in the application. See http://docs.asp.net/en/latest/fundamentals/diagnostics.html
                application.UseRuntimeInfoPage();

                // Allow updates to your files in Visual Studio to be shown in the browser. You can use the Refresh 
                // browser link button in the Visual Studio toolbar or Ctrl+Alt+Enter to refresh the browser.
                application.UseBrowserLink();
                // $Start-Glimpse$

                // Add Glimpse to help with debugging (See http://getglimpse.com/).
                application.UseGlimpse();
                // $End-Glimpse$
            }
        }
    }
}