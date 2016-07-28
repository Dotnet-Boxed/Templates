namespace MvcBoilerplate
{
    using Boilerplate.AspNetCore;
    // $Start-RedirectToCanonicalUrl$
    using Boilerplate.AspNetCore.Filters;
    // $End-RedirectToCanonicalUrl$
    // $Start-CshtmlMinification$
    using Boilerplate.AspNetCore.Razor;
    // $End-CshtmlMinification$
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    // $Start-CshtmlMinification$
    using Microsoft.AspNetCore.Mvc.Razor;
    // $End-CshtmlMinification$
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using MvcBoilerplate.Settings;
    // $Start-JsonSerializerSettings$
    using Newtonsoft.Json.Serialization;
    // $End-JsonSerializerSettings$

    /// <summary>
    /// The main start-up class for the application.
    /// </summary>
    public partial class Startup
    {
        #region Fields

        /// <summary>
        /// Gets or sets the application configuration, where key value pair settings are stored. See
        /// http://docs.asp.net/en/latest/fundamentals/configuration.html
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// The environment the application is running under. This can be Development, Staging or Production by default.
        /// To set the hosting environment on Windows:
        /// 1. On your server, right click 'Computer' or 'My Computer' and click on 'Properties'.
        /// 2. Go to 'Advanced System Settings'.
        /// 3. Click on 'Environment Variables' in the Advanced tab.
        /// 4. Add a new System Variable with the name 'ASPNETCORE_ENVIRONMENT' and value of Production, Staging or
        /// whatever you want. See http://docs.asp.net/en/latest/fundamentals/environments.html
        /// </summary>
        private readonly IHostingEnvironment hostingEnvironment;
        // $Start-HttpsEverywhere-On$

        /// <summary>
        /// Gets or sets the port to use for HTTPS. Only used in the development environment.
        /// </summary>
        private readonly int? sslPort;
        // $End-HttpsEverywhere-On$

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

            this.configuration = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                // Add configuration from the config.json file.
                .AddJsonFile("config.json")
                // Add configuration from an optional config.development.json, config.staging.json or
                // config.production.json file, depending on the environment. These settings override the ones in the
                // config.json file.
                .AddJsonFile($"config.{hostingEnvironment.EnvironmentName}.json", optional: true)
                // This reads the configuration keys from the secret store. This allows you to store connection strings
                // and other sensitive settings, so you don't have to check them into your source control provider.
                // Only use this in Development, it is not intended for Production use. See
                // http://go.microsoft.com/fwlink/?LinkID=532709 and
                // http://docs.asp.net/en/latest/security/app-secrets.html
                .AddIf(
                    hostingEnvironment.IsDevelopment(),
                    x => x.AddUserSecrets())
                // Add configuration specific to the Development, Staging or Production environments. This config can
                // be stored on the machine being deployed to or if you are using Azure, in the cloud. These settings
                // override the ones in all of the above config files.
                // Note: To set environment variables for debugging navigate to:
                // Project Properties -> Debug Tab -> Environment Variables
                // Note: To get environment variables for the machine use the following command in PowerShell:
                // [System.Environment]::GetEnvironmentVariable("[VARIABLE_NAME]", [System.EnvironmentVariableTarget]::Machine)
                // Note: To set environment variables for the machine use the following command in PowerShell:
                // [System.Environment]::SetEnvironmentVariable("[VARIABLE_NAME]", "[VARIABLE_VALUE]", [System.EnvironmentVariableTarget]::Machine)
                // Note: Environment variables use a colon separator e.g. You can override the site title by creating a
                // variable named AppSettings:SiteTitle. See http://docs.asp.net/en/latest/security/app-secrets.html
                .AddEnvironmentVariables()
                // $Start-ApplicationInsights$
                // Push telemetry data through the Azure Application Insights pipeline faster in the development and
                // staging environments, allowing you to view results immediately.
                .AddApplicationInsightsSettings(developerMode: !hostingEnvironment.IsProduction())
                // $End-ApplicationInsights$
                .Build();
            // $Start-HttpsEverywhere-On$

            if (this.hostingEnvironment.IsDevelopment())
            {
                var launchConfiguration = new ConfigurationBuilder()
                    .SetBasePath(hostingEnvironment.ContentRootPath)
                    .AddJsonFile(@"Properties\launchSettings.json")
                    .Build();
                this.sslPort = launchConfiguration.GetValue<int>("iisSettings:iisExpress:sslPort");
            }
            // $End-HttpsEverywhere-On$
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
            services
                // $Start-ApplicationInsights$
                // Add Azure Application Insights data collection services to the services container.
                .AddApplicationInsightsTelemetry(this.configuration)
                // $End-ApplicationInsights$
                .AddAntiforgerySecurely()
                .AddCaching()
                .AddOptions(this.configuration)
                .AddRouting(
                    options =>
                    {
                        // Improve SEO by stopping duplicate URL's due to case differences or trailing slashes.
                        // See http://googlewebmastercentral.blogspot.co.uk/2010/04/to-slash-or-not-to-slash.html
                        // All generated URL's should append a trailing slash.
                        options.AppendTrailingSlash = true;
                        // All generated URL's should be lower-case.
                        options.LowercaseUrls = true;
                    })
                // Add useful interface for accessing the ActionContext outside a controller.
                .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
                // Add useful interface for accessing the HttpContext outside a controller.
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                // Add useful interface for accessing the IUrlHelper outside a controller.
                .AddScoped<IUrlHelper>(x => x
                    .GetRequiredService<IUrlHelperFactory>()
                    .GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext))
                // $Start-RedirectToCanonicalUrl$
                // Adds a filter which help improve search engine optimization (SEO).
                .AddSingleton<RedirectToCanonicalUrlAttribute>()
                // $End-RedirectToCanonicalUrl$
                // Add many MVC services to the services container.
                .AddMvc(
                    options =>
                    {
                        // Controls how controller actions cache content from the config.json file.
                        var cacheProfileSettings = this.configuration.GetSection<CacheProfileSettings>();
                        foreach (var keyValuePair in cacheProfileSettings.CacheProfiles)
                        {
                            options.CacheProfiles.Add(keyValuePair);
                        }
                        // $Start-HttpsEverywhere-On$

                        // Require HTTPS to be used across the whole site. Also set a custom port to use for SSL in
                        // Development. The port number to use is taken from the launchSettings.json file which Visual
                        // Studio uses to start the application.
                        options.Filters.Add(new RequireHttpsAttribute());
                        options.SslPort = sslPort;
                        // $End-HttpsEverywhere-On$
                        // $Start-RedirectToCanonicalUrl$

                        // Adds a filter which help improve search engine optimization (SEO).
                        options.Filters.AddService(typeof(RedirectToCanonicalUrlAttribute));
                        // $End-RedirectToCanonicalUrl$
                    })
                // $Start-JsonSerializerSettings$
                // Configures the JSON output formatter to use camel case property names like 'propertyName' instead of
                // pascal case 'PropertyName' as this is the more common JavaScript/JSON style.
                .AddJsonOptions(
                    x => x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                // $End-JsonSerializerSettings$
                // $Start-XmlFormatter-DataContractSerializer$
                // Adds the XML input and output formatter using the DataContractSerializer.
                .AddXmlDataContractSerializerFormatters()
                // $End-XmlFormatter-DataContractSerializer$
                // $Start-XmlFormatter-XmlSerializer$
                // Adds the XML input and output formatter using the XmlSerializer.
                .AddXmlSerializerFormatters()
                // $End-XmlFormatter-XmlSerializer$
                .Services
                // $Start-CshtmlMinification$
                // Adds a razor view location expander which looks for minified .min.cshtml files to execute if found.
                .Configure<RazorViewEngineOptions>(
                    options => options.ViewLocationExpanders.Add(new MinifiedViewLocationExpander()))
                // $End-CshtmlMinification$
                .AddCustomServices();
        }

        /// <summary>
        /// Configures the application and HTTP request pipeline. Configure is called after ConfigureServices is
        /// called by the ASP.NET runtime.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="loggerfactory">The logger factory.</param>
        public void Configure(IApplicationBuilder application, ILoggerFactory loggerfactory)
        {
            // Configure application logging. See http://docs.asp.net/en/latest/fundamentals/logging.html
            loggerfactory
                // Log to Serilog (A great logging framework) (See https://github.com/serilog/serilog-framework-logging).
                // Add the Serilog package to project.json before uncommenting the line below.
                // .AddSerilog()
                .AddIf(
                    hostingEnvironment.IsDevelopment(),
                    x => x
                        .AddConsole(configuration.GetSection("Logging"))
                        .AddDebug());

            application
                // Removes the Server HTTP header from the HTTP response for marginally better security and performance.
                .UseNoServerHttpHeader()
                // $Start-ApplicationInsights$
                // Add Azure Application Insights to the request pipeline to track HTTP request telemetry data.
                .UseApplicationInsightsRequestTelemetry()
                // Track data about exceptions from the application. Should be configured after all error handling
                // middleware in the request pipeline.
                .UseApplicationInsightsExceptionTelemetry()
                // $End-ApplicationInsights$
                // Add static files to the request pipeline e.g. hello.html or world.css.
                .UseStaticFiles()
                .UseCookiePolicy()
                .UseIfElse(
                    this.hostingEnvironment.IsDevelopment(),
                    x => x
                        .UseDebugging()
                        .UseDeveloperErrorPages(),
                    x => x.UseErrorPages())
                // $Start-NWebSec$
                // $Start-HttpsEverywhere-On$
                .UseStrictTransportSecurityHttpHeader()
                .UsePublicKeyPinsHttpHeader()
                .UseContentSecurityPolicyHttpHeader(this.sslPort, this.hostingEnvironment)
                // $End-HttpsEverywhere-On$
                // $Start-HttpsEverywhere-Off$
                // .UseContentSecurityPolicyHttpHeader(this.hostingEnvironment)
                // $End-HttpsEverywhere-Off$
                .UseSecurityHttpHeaders()
                // $End-NWebSec$
                // Add MVC to the request pipeline.
                .UseMvc();
        }

        #endregion
    }
}