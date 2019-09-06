namespace ApiTemplate
{
    using System;
#if CORS
    using ApiTemplate.Constants;
#endif
    using Boxed.AspNetCore;
#if CorrelationId
    using CorrelationId;
#endif
    using Microsoft.AspNetCore.Builder;
#if HealthCheck
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
#endif
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The main start-up class for the application.
    /// </summary>
    public class Startup : StartupBase
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration, where key value pair settings are stored. See
        /// http://docs.asp.net/en/latest/fundamentals/configuration.html</param>
        /// <param name="hostingEnvironment">The environment the application is running under. This can be Development,
        /// Staging or Production by default. See http://docs.asp.net/en/latest/fundamentals/environments.html</param>
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            this.configuration = configuration;
            this.hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Configures the services to add to the ASP.NET Core Injection of Control (IoC) container. This method gets
        /// called by the ASP.NET runtime. See
        /// http://blogs.msdn.com/b/webdev/archive/2014/06/17/dependency-injection-in-asp-net-vnext.aspx
        /// </summary>
        public override void ConfigureServices(IServiceCollection services) =>
            services
#if ApplicationInsights
                // Add Azure Application Insights data collection services to the services container.
                .AddApplicationInsightsTelemetry(this.configuration)
#endif
#if CorrelationId
                .AddCorrelationIdFluent()
#endif
                .AddCustomCaching()
                .AddCustomOptions(this.configuration)
                .AddCustomRouting()
#if ResponseCaching
                .AddResponseCaching()
#endif
#if ResponseCompression
                .AddCustomResponseCompression()
#endif
#if HttpsEverywhere
                .AddCustomStrictTransportSecurity()
#endif
#if HealthCheck
                .AddCustomHealthChecks()
#endif
#if Swagger
                .AddCustomSwagger()
#endif
                .AddHttpContextAccessor()
                // Add useful interface for accessing the ActionContext outside a controller.
                .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
                // Add useful interface for accessing the IUrlHelper outside a controller.
                .AddScoped(x => x
                    .GetRequiredService<IUrlHelperFactory>()
                    .GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext))
#if Versioning
                .AddCustomApiVersioning()
#endif
#if Swagger && Versioning
                .AddVersionedApiExplorer(x => x.GroupNameFormat = "'v'VVV") // Version format: 'v'major[.minor][-status]
#endif
                .AddMvcCore()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddApiExplorer()
                    .AddAuthorization()
                    .AddDataAnnotations()
                    .AddJsonFormatters()
                    .AddCustomJsonOptions(this.hostingEnvironment)
#if CORS
                    .AddCustomCors()
#endif
#if DataContractSerializer
                    // Adds the XML input and output formatter using the DataContractSerializer.
                    .AddXmlDataContractSerializerFormatters()
#elif XmlSerializer
                    // Adds the XML input and output formatter using the XmlSerializer.
                    .AddXmlSerializerFormatters()
#endif
                    .AddCustomMvcOptions()
                .Services
                .AddProjectCommands()
                .AddProjectMappers()
                .AddProjectRepositories()
                .AddProjectServices();

        /// <summary>
        /// Configures the application and HTTP request pipeline. Configure is called after ConfigureServices is
        /// called by the ASP.NET runtime.
        /// </summary>
        public override void Configure(IApplicationBuilder application) =>
            application
#if CorrelationId
                // Pass a GUID in a X-Correlation-ID HTTP header to set the HttpContext.TraceIdentifier.
                // UpdateTraceIdentifier must be false due to a bug. See https://github.com/aspnet/AspNetCore/issues/5144
                .UseCorrelationId(new CorrelationIdOptions() { UpdateTraceIdentifier = false })
#endif
#if ForwardedHeaders
                .UseForwardedHeaders()
#elif HostFiltering
                .UseHostFiltering()
#endif
#if ResponseCaching
                .UseResponseCaching()
#endif
#if ResponseCompression
                .UseResponseCompression()
#endif
#if CORS
                .UseCors(CorsPolicyName.AllowAny)
#endif
#if HttpsEverywhere
                .UseIf(
                    !this.hostingEnvironment.IsDevelopment(),
                    x => x.UseHsts())
#endif
                .UseIf(
                    this.hostingEnvironment.IsDevelopment(),
                    x => x.UseDeveloperErrorPages())
#if HealthCheck
                .UseHealthChecks("/status")
                .UseHealthChecks("/status/self", new HealthCheckOptions() { Predicate = _ => false })
#endif
                .UseStaticFilesWithCacheControl()
#if Swagger
                .UseMvc()
                .UseSwagger(options => options.PreSerializeFilters.Add(
                    (swaggerDoc, httpReq) => swaggerDoc.Host = httpReq.Host.Value))
                .UseCustomSwaggerUI();
#else
                .UseMvc();
#endif
    }
}
