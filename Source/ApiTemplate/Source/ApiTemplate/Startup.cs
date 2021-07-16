namespace ApiTemplate
{
#if CORS
    using ApiTemplate.Constants;
#endif
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.Builder;
#if HealthCheck
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
#endif
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// The main start-up class for the application.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration, where key value pair settings are stored. See
        /// http://docs.asp.net/en/latest/fundamentals/configuration.html</param>
        /// <param name="webHostEnvironment">The environment the application is running under. This can be Development,
        /// Staging or Production by default. See http://docs.asp.net/en/latest/fundamentals/environments.html</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Configures the services to add to the ASP.NET Core Injection of Control (IoC) container. This method gets
        /// called by the ASP.NET runtime. See
        /// http://blogs.msdn.com/b/webdev/archive/2014/06/17/dependency-injection-in-asp-net-vnext.aspx
        /// </summary>
        /// <param name="services">The services.</param>
        public virtual void ConfigureServices(IServiceCollection services) =>
            services
#if ApplicationInsights
                // Add Azure Application Insights data collection services to the services container.
                .AddApplicationInsightsTelemetry(this.configuration)
#endif
                .AddCustomCaching()
#if CORS
                .AddCustomCors()
#endif
                .AddCustomOptions(this.configuration)
                .AddCustomRouting()
#if ResponseCaching
                .AddResponseCaching()
#endif
#if ResponseCompression
                .AddCustomResponseCompression(this.configuration)
#endif
#if HttpsEverywhere
                .AddCustomStrictTransportSecurity()
#endif
#if HealthCheck
                .AddCustomHealthChecks()
#endif
#if OpenTelemetry
                .AddCustomOpenTelemetryTracing(this.webHostEnvironment)
#endif
#if Swagger
                .AddCustomSwagger()
#endif
                .AddHttpContextAccessor()
                // Add useful interface for accessing the ActionContext outside a controller.
                .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
#if Versioning
                .AddCustomApiVersioning()
#endif
                .AddServerTiming()
                .AddControllers()
                    .AddCustomJsonOptions(this.webHostEnvironment)
#if DataContractSerializer
                    // Adds the XML input and output formatter using the DataContractSerializer.
                    .AddXmlDataContractSerializerFormatters()
#elif XmlSerializer
                    // Adds the XML input and output formatter using the XmlSerializer.
                    .AddXmlSerializerFormatters()
#endif
                    .AddCustomMvcOptions(this.configuration)
                .Services
                .AddProjectCommands()
                .AddProjectMappers()
                .AddProjectRepositories()
                .AddProjectServices();

        /// <summary>
        /// Configures the application and HTTP request pipeline. Configure is called after ConfigureServices is
        /// called by the ASP.NET runtime.
        /// </summary>
        /// <param name="application">The application builder.</param>
        public virtual void Configure(IApplicationBuilder application) =>
            application
                .UseIf(
                    this.webHostEnvironment.IsDevelopment(),
                    x => x.UseServerTiming())
#if ForwardedHeaders
                .UseForwardedHeaders()
#elif HostFiltering
                .UseHostFiltering()
#endif
                .UseRouting()
#if CORS
                .UseCors(CorsPolicyName.AllowAny)
#endif
#if ResponseCaching
                .UseResponseCaching()
#endif
#if ResponseCompression
                .UseResponseCompression()
#endif
#if HttpsEverywhere
                .UseIf(
                    !this.webHostEnvironment.IsDevelopment(),
                    x => x.UseHsts())
#endif
                .UseIf(
                    this.webHostEnvironment.IsDevelopment(),
                    x => x.UseDeveloperExceptionPage())
                .UseStaticFilesWithCacheControl()
#if Serilog
                .UseCustomSerilogRequestLogging()
#endif
                .UseEndpoints(
                    builder =>
                    {
#if CORS
                        builder.MapControllers().RequireCors(CorsPolicyName.AllowAny);
#else
                        builder.MapControllers();
#endif
#if (HealthCheck && CORS)
                        builder
                            .MapHealthChecks("/status")
                            .RequireCors(CorsPolicyName.AllowAny);
                        builder
                            .MapHealthChecks("/status/self", new HealthCheckOptions() { Predicate = _ => false })
                            .RequireCors(CorsPolicyName.AllowAny);
#elif HealthCheck
                        builder.MapHealthChecks("/status");
                        builder.MapHealthChecks("/status/self", new HealthCheckOptions() { Predicate = _ => false });
#endif
#if Swagger
                    })
                .UseSwagger()
                .UseCustomSwaggerUI();
#else
                    });
#endif
    }
}
