namespace OrleansTemplate.Server
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using OrleansTemplate.Server.HealthChecks;

#pragma warning disable CA1724 // The type name conflicts with the namespace name 'Orleans.Runtime.Startup'
    public class Startup
#pragma warning restore CA1724 // The type name conflicts with the namespace name 'Orleans.Runtime.Startup'
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration, where key value pair settings are stored (See
        /// http://docs.asp.net/en/latest/fundamentals/configuration.html).</param>
        /// <param name="webHostEnvironment">The environment the application is running under. This can be Development,
        /// Staging or Production by default (See http://docs.asp.net/en/latest/fundamentals/environments.html).</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
        }

        public virtual void ConfigureServices(IServiceCollection services) =>
            services
                .AddRouting()
#if OpenTelemetry
                .AddCustomOpenTelemetryTracing(this.webHostEnvironment)
#endif
                .AddHealthChecks()
                .AddCheck<ClusterHealthCheck>(nameof(ClusterHealthCheck))
                .AddCheck<GrainHealthCheck>(nameof(GrainHealthCheck))
                .AddCheck<SiloHealthCheck>(nameof(SiloHealthCheck))
                .AddCheck<StorageHealthCheck>(nameof(StorageHealthCheck));

        public virtual void Configure(IApplicationBuilder application) =>
            application
                .UseRouting()
#if Serilog
                .UseCustomSerilogRequestLogging()
#endif
                .UseEndpoints(
                    builder =>
                    {
                        builder.MapHealthChecks("/status");
                        builder.MapHealthChecks("/status/self", new HealthCheckOptions() { Predicate = _ => false });
                    });
    }
}
