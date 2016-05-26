namespace MvcBoilerplate
{
    using Boilerplate.AspNetCore;
    // $Start-CshtmlMinification$
    using Boilerplate.AspNetCore.Razor;
    // $End-CshtmlMinification$
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The main start-up class for the application.
    /// </summary>
    public partial class Startup
    {
        #region Fields

        /// <summary>
        /// Gets or sets the application configuration, where key value pair settings are stored. See
        /// http://docs.asp.net/en/latest/fundamentals/configuration.html
        /// http://weblog.west-wind.com/posts/2015/Jun/03/Strongly-typed-AppSettings-Configuration-in-ASPNET-5
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// The environment the application is running under. This can be Development, Staging or Production by default.
        /// To set the hosting environment on Windows:
        /// 1. On your server, right click 'Computer' or 'My Computer' and click on 'Properties'.
        /// 2. Go to 'Advanced System Settings'.
        /// 3. Click on 'Environment Variables' in the Advanced tab.
        /// 4. Add a new System Variable with the name 'ASPNET_ENV' for RC1 or 'ASPNETCORE_ENVIRONMENT' for RC2 and a
        /// value of Production, Staging or whatever you want.
        /// See http://docs.asp.net/en/latest/fundamentals/environments.html
        /// </summary>
        private readonly IHostingEnvironment hostingEnvironment;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="hostingEnvironment">The environment the application is running under. This can be Development,
        /// Staging or Production by default.</param>
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.configuration = ConfigureConfiguration(hostingEnvironment);
        }

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
            // $Start-ApplicationInsights$
            // Add Azure Application Insights data collection services to the services container.
            services.AddApplicationInsightsTelemetry(this.configuration);

            // $End-ApplicationInsights$
            // $Start-Glimpse$
            ConfigureDebuggingServices(services, this.hostingEnvironment);
            // $End-Glimpse$
            ConfigureOptionsServices(services, this.configuration);
            ConfigureCachingServices(services);

            // $Start-RedirectToCanonicalUrl$
            // Configure MVC routing. We store the route options for use by ConfigureSearchEngineOptimizationFilters.
            RouteOptions routeOptions = null;
            // $End-RedirectToCanonicalUrl$
            services.AddRouting(
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
                    // $Start-NWebSec$
                    ConfigureContentSecurityPolicyFilters(this.hostingEnvironment, mvcOptions.Filters);
                    // $End-NWebSec$
                });

            // $Start-CshtmlMinification$
            services.Configure<RazorViewEngineOptions>(
                options =>
                {
                    options.ViewLocationExpanders.Add(new MinifiedViewLocationExpander());
                });
            // services.AddTransient<IRazorViewEngine, MinifiedRazorViewEngine>();
            // $End-CshtmlMinification$
            ConfigureFormatters(mvcBuilder);

            ConfigureAntiforgeryServices(services, this.hostingEnvironment);
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
            // Removes the Server HTTP header from the HTTP response for marginally better security and performance.
            application.UseNoServerHttpHeader();

            // $Start-ApplicationInsights$

            // Add Azure Application Insights to the request pipeline to track HTTP request telemetry data.
            application.UseApplicationInsightsRequestTelemetry();
            // Track data about exceptions from the application. Should be configured after all error handling
            // middleware in the request pipeline.
            application.UseApplicationInsightsExceptionTelemetry();
            // $End-ApplicationInsights$

            // Add static files to the request pipeline e.g. hello.html or world.css.
            application.UseStaticFiles();

            ConfigureCookies(application, this.hostingEnvironment);
            ConfigureDebugging(application, this.hostingEnvironment);
            ConfigureLogging(this.hostingEnvironment, loggerfactory, this.configuration);
            ConfigureErrorPages(application, this.hostingEnvironment);
            // $Start-HttpsEverywhere$
            ConfigureContentSecurityPolicy(application, this.hostingEnvironment);
            ConfigureSecurity(application, this.hostingEnvironment);
            // $End-HttpsEverywhere$

            // Add MVC to the request pipeline.
            application.UseMvc();

            // Add a 404 Not Found error page for visiting /this-resource-does-not-exist.
            Configure404NotFoundErrorPage(application, this.hostingEnvironment);
        }

        #endregion
    }
}