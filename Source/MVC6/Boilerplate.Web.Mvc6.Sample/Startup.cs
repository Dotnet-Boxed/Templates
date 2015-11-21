namespace MvcBoilerplate
{
    using System.Reflection;
    using Boilerplate.Web.Mvc;
    // $Start-CshtmlMinification$
    using Boilerplate.Web.Mvc.Razor;
    // $End-CshtmlMinification$
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Hosting;
    using Microsoft.AspNet.Mvc.Razor;
    using Microsoft.AspNet.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.PlatformAbstractions;

    /// <summary>
    /// The main start-up class for the application.
    /// </summary>
    public partial class Startup
    {
        #region Fields

        /// <summary>
        /// The location the application is running in.
        /// </summary>
        private readonly IApplicationEnvironment applicationEnvironment;

        /// <summary>
        /// Gets or sets the application configuration, where key value pair settings are stored. See
        /// http://docs.asp.net/en/latest/fundamentals/configuration.html
        /// http://weblog.west-wind.com/posts/2015/Jun/03/Strongly-typed-AppSettings-Configuration-in-ASPNET-5
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// The environment the application is running under. This can be Development, Staging or Production by default.
        /// </summary>
        private readonly IHostingEnvironment hostingEnvironment; 

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="applicationEnvironment">The location the application is running in.</param>
        /// <param name="hostingEnvironment">The environment the application is running under. This can be Development, 
        /// Staging or Production by default.</param>
        public Startup(
            IApplicationEnvironment applicationEnvironment,
            IHostingEnvironment hostingEnvironment)
        {
            this.applicationEnvironment = applicationEnvironment;
            this.hostingEnvironment = hostingEnvironment;
            this.configuration = ConfigureConfiguration(applicationEnvironment, hostingEnvironment);
        }

        #endregion

        /// <summary>
        /// Entry point for the application.
        /// </summary>
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);

        #region Public Methods

        /// <summary>
        /// Configures the services to add to the ASP.NET MVC 6 Injection of Control (IoC) container. This method gets 
        /// called by the ASP.NET runtime. See:
        /// http://blogs.msdn.com/b/webdev/archive/2014/06/17/dependency-injection-in-asp-net-vnext.aspx
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // $Start-ApplicationInsights$
            // Add Azure Application Insights data collection services to the services container.
            services.AddApplicationInsightsTelemetry(this.configuration);

            // $End-ApplicationInsights$
            ConfigureOptionsServices(services, this.configuration);
            ConfigureCachingServices(services);

            // $Start-RedirectToCanonicalUrl$
            // Configure MVC routing. We store the route options for use by ConfigureSearchEngineOptimizationFilters.
            RouteOptions routeOptions = null;
            // $End-RedirectToCanonicalUrl$
            services.ConfigureRouting(
                x =>
                {
                    // $Start-RedirectToCanonicalUrl$
                    routeOptions = x;
                    // $End-RedirectToCanonicalUrl$
                    ConfigureRouting(x);
                });

            // Add many MVC services to the services container.
            IMvcBuilder mvcBuilder = services.AddMvc(
                mvcOptions =>
                {
                    ConfigureCacheProfiles(mvcOptions.CacheProfiles, this.configuration);
                    // $Start-RedirectToCanonicalUrl$
                    ConfigureSearchEngineOptimizationFilters(mvcOptions.Filters, routeOptions);
                    // $End-RedirectToCanonicalUrl$
                    ConfigureSecurityFilters(this.hostingEnvironment, mvcOptions.Filters);
                    ConfigureContentSecurityPolicyFilters(mvcOptions.Filters);
                });
#if !DEBUG
            // Use pre-compiled views in release mode for quicker application start-up.
            mvcBuilder.AddPrecompiledRazorViews(GetType().GetTypeInfo().Assembly);
#endif
            // $Start-CshtmlMinification$
            services.Configure<RazorViewEngineOptions>(
                options =>
                {
                    options.ViewLocationExpanders.Add(new MinifiedViewLocationExpander());
                });
            // services.AddTransient<IRazorViewEngine, MinifiedRazorViewEngine>();
            // $End-CshtmlMinification$
            ConfigureFormatters(mvcBuilder);

            ConfigureAntiforgeryServices(services);
            ConfigureCustomServices(services);
        }

        /// <summary>
        /// Configures the application and HTTP request pipeline. Configure is called after ConfigureServices is 
        /// called by the ASP.NET runtime.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="loggerfactory">The logger factory.</param>
        public void Configure(IApplicationBuilder application, ILoggerFactory loggerfactory)
        {
            // $Start-ApplicationInsights$
            // Add Azure Application Insights to the request pipeline to track HTTP request telemetry data.
            application.UseApplicationInsightsRequestTelemetry();

            // $End-ApplicationInsights$
            // Give the ASP.NET MVC Boilerplate NuGet package assembly access to the HttpContext, so it can generate 
            // absolute URL's and get the current request path.
            application.UseBoilerplate();

            // Add the IIS platform handler to the request pipeline.
            application.UseIISPlatformHandler();

            // Add static files to the request pipeline e.g. hello.html or world.css.
            application.UseStaticFiles();

            ConfigureCookies(application, this.hostingEnvironment);
            ConfigureDebugging(application, this.hostingEnvironment);
            ConfigureLogging(application, this.hostingEnvironment, loggerfactory, this.configuration);
            ConfigureErrorPages(application, this.hostingEnvironment);

            // Add MVC to the request pipeline.
            application.UseMvc();
            // $Start-ApplicationInsights$

            // Track data about exceptions from the application. Should be configured after all error handling 
            // middleware in the request pipeline.
            application.UseApplicationInsightsExceptionTelemetry();
            // $End-ApplicationInsights$

            // Add a 404 Not Found error page for visiting /this-resource-does-not-exist.
            Configure404NotFoundErrorPage(application, this.hostingEnvironment);
        }

        #endregion
    }
}