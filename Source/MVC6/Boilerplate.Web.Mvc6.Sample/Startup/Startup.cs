namespace MvcBoilerplate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Boilerplate.Web.Mvc.Filters;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Diagnostics;
    using Microsoft.AspNet.Hosting;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.OptionDescriptors;
    using Microsoft.AspNet.Routing;
    using Microsoft.Framework.ConfigurationModel;
    using Microsoft.Framework.DependencyInjection;
    using Microsoft.Framework.Logging;
    using Microsoft.Framework.OptionsModel;
    using MvcBoilerplate.Constants;
    using MvcBoilerplate.Services;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// The main start-up class for the application.
    /// </summary>
    public partial class Startup
    {
        #region Constructors
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="environment">The environment the application is running under. This can be Development, 
        /// Staging or Production by default.</param>
        public Startup(IHostingEnvironment environment)
        {
            // Configuration replaces the old appSettings and connectionStrings in the web.config file from MVC 5. See:
            // http://docs.asp.net/en/latest/fundamentals/configuration.html?highlight=configuration
            // http://weblog.west-wind.com/posts/2015/Jun/03/Strongly-typed-AppSettings-Configuration-in-ASPNET-5
            this.Configuration = new Configuration()
                // Add configuration from the config.json file.
                .AddJsonFile("config.json")
                // Add configuration from an optional config.development.json, config.staging.json or 
                // config.production.json file, depending on on the environment. These settings override the ones in 
                // the config.json file.
                .AddJsonFile($"config.{environment.EnvironmentName}.json", optional: true)
                // Add configuration specific to the Development, Staging or Production environments. This config can 
                // be stored on the machine being deployed to or if you are using Azure, in the cloud. These settings 
                // override the ones in all of the above config files. To set environment variables for debugging:
                // Navigate to Project Properties -> Debug Tab -> Environment Variables
                // Note: Environment variables use a colon separator e.g. You can override the site title by creating a 
                // variable named AppSettings:SiteTitle.
                .AddEnvironmentVariables();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the configuration, where key value pair settings can be stored. See:
        /// http://docs.asp.net/en/latest/fundamentals/configuration.html?highlight=configuration
        /// http://weblog.west-wind.com/posts/2015/Jun/03/Strongly-typed-AppSettings-Configuration-in-ASPNET-5
        /// </summary>
        public IConfiguration Configuration { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Configures the services to add to the ASP.NET MVC 6 Injection of Control (IoC) container. This method gets 
        /// called by the ASP.NET runtime. See:
        /// http://blogs.msdn.com/b/webdev/archive/2014/06/17/dependency-injection-in-asp-net-vnext.aspx
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add IOptions<AppSettings> to the services container.
            services.Configure<AppSettings>(this.Configuration.GetSubKey("AppSettings"));

            // Add many MVC services to the services container.
            services.AddMvc();

            RouteOptions routeOptions = null;
            services.ConfigureRouting(x => { routeOptions = x; ConfigureRouting(routeOptions); });

            services.ConfigureMvc(mvcOptions =>
            {
                ConfigureAntiforgeryTokens(mvcOptions.AntiForgeryOptions);
                ConfigureCacheProfiles(mvcOptions.CacheProfiles);
                ConfigureSearchEngineOptimizationFilters(mvcOptions.Filters, routeOptions);
                ConfigureSecurityFilters(mvcOptions.Filters);
                ConfigureContentSecurityPolicyFilters(mvcOptions.Filters);
                ConfigureFormatters(mvcOptions);
                ConfigureViewEngines(mvcOptions.ViewEngines);
            });

            services.AddScoped<IFeedService, FeedService>();
            services.AddSingleton<ILoggingService, LoggingService>();
            services.AddScoped<IOpenSearchService, OpenSearchService>();
            services.AddScoped<IRobotsService, RobotsService>();
            services.AddScoped<ISitemapService, SitemapService>();
            services.AddScoped<ISitemapPingerService, SitemapPingerService>();

            // Add your own custom services here e.g.

            // Singleton - Only one instance is ever created and returned.
            // services.AddSingleton<IDatabaseService, DatabaseService>();

            // Scoped - A new instance is created and returned for each request/response cycle.
            // services.AddScoped<IDatabaseService, DatabaseService>();

            // Transient - A new instance is created and returned each time.
            // services.AddTransient<IDatabaseService, DatabaseService>();
        }

        /// <summary>
        /// Configures the application and HTTP request pipeline. Configure is called after ConfigureServices is 
        /// called by the ASP.NET runtime.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="environment">The environment the application is running under. This can be Development, 
        /// Staging or Production by default.</param>
        /// <param name="loggerfactory">The logger factory.</param>
        public void Configure(
            IApplicationBuilder application, 
            IHostingEnvironment environment, 
            ILoggerFactory loggerfactory)
        {
            // Add the following to the request pipeline only in development environment.
            if (environment.IsEnvironment(EnvironmentName.Development))
            {
                // Add the console logger, which logs events to the Console, including errors and trace information.
                loggerfactory.AddConsole();

                // Browse to /runtimeinfo to see information about the runtime that is being used and the packages that 
                // are included in the application. See http://docs.asp.net/en/latest/fundamentals/diagnostics.html
                application.UseRuntimeInfoPage();

                // Allow updates to your files in Visual Studio to be shown in the browser. You can use the Refresh 
                // browser link button in the Visual Studio toolbar or Ctrl+Alt+Enter to refresh the browser.
                application.UseBrowserLink();

                // When an error occurs, displays a detailed error page with full diagnostic information. It is unsafe
                // to use this in production. See http://docs.asp.net/en/latest/fundamentals/diagnostics.html
                application.UseErrorPage(ErrorPageOptions.ShowAll);

                // Browse to /throw to force an exception to be thrown. Useful for testing your error pages.
                application.Map("/throw", throwApp =>
                {
                    throwApp.Run(context => { throw new Exception("Deliberate exception thrown to test error handling."); });
                });
            }
            else // Staging or Production environments.
            {
                // Add Error handling middleware which catches all application specific errors and send the request to 
                // the following path or controller action.
                application.UseErrorHandler("/error/internalservererror");
            }

            // Give the ASP.NET MVC Boilerplate URL Helper access to the HttpContext, so it can generate absolute URL's.
            Boilerplate.Web.Mvc.UrlHelperExtensions.Configure(
                application.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            
            // Add static files to the request pipeline e.g. hello.html or world.css.
            application.UseStaticFiles();

            // Add MVC to the request pipeline.
            application.UseMvc();
        }

        #endregion
    }
}
