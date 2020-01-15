namespace OrleansTemplate.Server.HealthChecks
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Logging;
    using Orleans;
    using Orleans.Runtime;

    /// <summary>
    /// Verifies whether any silos are unavailable by querying the <see cref="IManagementGrain"/>.
    /// </summary>
    public class ClusterHealthCheck : IHealthCheck
    {
        private const string DegradedMessage = " silo(s) unavailable.";
        private const string FailedMessage = "Failed cluster status health check.";
        private readonly IClusterClient client;
        private readonly ILogger<ClusterHealthCheck> logger;

        public ClusterHealthCheck(IClusterClient client, ILogger<ClusterHealthCheck> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            var manager = this.client.GetGrain<IManagementGrain>(0);

            try
            {
                var hosts = await manager.GetHosts().ConfigureAwait(false);
                var count = hosts.Values.Count(x => x.IsUnavailable());
                return count > 0 ? HealthCheckResult.Degraded(count + DegradedMessage) : HealthCheckResult.Healthy();
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception exception)
#pragma warning restore CA1031 // Do not catch general exception types
            {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                this.logger.LogError(exception, FailedMessage);
#pragma warning restore CA1303 // Do not pass literals as localized parameters
                return HealthCheckResult.Unhealthy(FailedMessage, exception);
            }
        }
    }
}
