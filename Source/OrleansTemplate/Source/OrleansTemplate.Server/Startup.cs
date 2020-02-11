namespace OrleansTemplate.Server
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.Extensions.DependencyInjection;
    using OrleansTemplate.Server.HealthChecks;

#pragma warning disable CA1724 // The type name conflicts with the namespace name 'Orleans.Runtime.Startup'
    public class Startup
#pragma warning restore CA1724 // The type name conflicts with the namespace name 'Orleans.Runtime.Startup'
    {
        public virtual void ConfigureServices(IServiceCollection services) =>
            services
                .AddRouting()
                .AddHealthChecks()
                .AddCheck<ClusterHealthCheck>(nameof(ClusterHealthCheck))
                .AddCheck<GrainHealthCheck>(nameof(GrainHealthCheck))
                .AddCheck<SiloHealthCheck>(nameof(SiloHealthCheck))
                .AddCheck<StorageHealthCheck>(nameof(StorageHealthCheck));

        public virtual void Configure(IApplicationBuilder application) =>
            application
                .UseRouting()
                .UseCustomSerilogRequestLogging()
                .UseEndpoints(
                    builder =>
                    {
                        builder.MapHealthChecks("/status");
                        builder.MapHealthChecks("/status/self", new HealthCheckOptions() { Predicate = _ => false });
                    });
    }
}
