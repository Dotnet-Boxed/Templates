namespace MvcBoilerplate
{
    using Boilerplate.Web.Mvc;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Hosting;
    using Microsoft.AspNet.Routing;
    using Microsoft.Dnx.Runtime;
    using Microsoft.Framework.Configuration;
    using Microsoft.Framework.DependencyInjection;
    using Microsoft.Framework.Logging;

    /// <summary>
    /// The main start-up class for the application.
    /// </summary>
    public partial class Startup
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="applicationEnvironment">The location the application is running in</param>
        /// <param name="hostingEnvironment">The environment the application is running under. This can be Development, 
        /// Staging or Production by default.</param>
        public Startup(
            IApplicationEnvironment applicationEnvironment,
            IHostingEnvironment hostingEnvironment)
        {
            this.Configuration = ConfigureConfiguration(applicationEnvironment, hostingEnvironment);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the application configuration, where key value pair settings are stored. See
        /// http://docs.asp.net/en/latest/fundamentals/configuration.html
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
            ConfigureOptionsServices(services, this.Configuration);
            ConfigureCachingServices(services);

            // Add many MVC services to the services container.
            IMvcBuilder mvcBuilder = services.AddMvc(
                mvcOptions =>
                {
                    ConfigureCacheProfiles(mvcOptions.CacheProfiles, this.Configuration);
                    ConfigureSecurityFilters(mvcOptions.Filters);
                    ConfigureContentSecurityPolicyFilters(mvcOptions.Filters);
                    ConfigureFormatters(mvcOptions);
                });
            ConfigureFormatters(mvcBuilder);
            // $Start-Swagger$

            ConfigureSwagger(services, this.Configuration);
            // $End-Swagger$

            // Configure MVC routing. We store the route options for use by ConfigureSearchEngineOptimizationFilters.
            RouteOptions routeOptions = null;
            services.ConfigureRouting(
                x =>
                {
                    routeOptions = x;
                    ConfigureRouting(routeOptions);
                });
            
            ConfigureAntiforgeryServices(services);
            ConfigureCustomServices(services);
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
            // Give the ASP.NET MVC Boilerplate NuGet package assembly access to the HttpContext, so it can generate 
            // absolute URL's and get the current request path.
            application.UseBoilerplate();

            ConfigureDebugging(application, environment, loggerfactory);
            ConfigureErrorPages(application, environment);

            // Add MVC to the request pipeline.
            application.UseMvc();

            ConfigureSwagger(application);

            // Display a welcome page at the root of the site.
            application.UseWelcomePage();
        }

        #endregion
    }
}