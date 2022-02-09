namespace OrleansTemplate.Server;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
#if Serilog
using OrleansTemplate.ConfigureOptions;
#endif
using OrleansTemplate.Server.HealthChecks;
#if Serilog
using Serilog;
#endif

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
#if Serilog
            .ConfigureOptions<ConfigureRequestLoggingOptions>()
#endif
#if ApplicationInsights
            // Add Azure Application Insights data collection services to the services container.
            .AddApplicationInsightsTelemetry(this.configuration)
#endif
            .AddRouting(options => options.LowercaseUrls = true)
#if OpenTelemetry
            .AddOpenTelemetryTracing(builder => builder.AddCustomTracing(this.webHostEnvironment))
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
            .UseSerilogRequestLogging()
#endif
            .UseEndpoints(
                builder =>
                {
                    builder.MapHealthChecks("/status");
                    builder.MapHealthChecks("/status/self", new HealthCheckOptions() { Predicate = _ => false });
                });
}
