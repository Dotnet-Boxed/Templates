namespace OrleansTemplate.Server.HealthChecks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Logging;
    using Orleans;
    using OrleansTemplate.Abstractions.Grains.HealthChecks;

    /// <summary>
    /// Verifies connectivity to a <see cref="ILocalHealthCheckGrain"/> activation. As this grain is a
    /// stateless worker, validation always occurs in the silo where the health check is issued.
    /// </summary>
    public class GrainHealthCheck : IHealthCheck
    {
        private const string FailedMessage = "Failed local health check.";
        private readonly IClusterClient client;
        private readonly ILogger<GrainHealthCheck> logger;

        public GrainHealthCheck(IClusterClient client, ILogger<GrainHealthCheck> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await this.client.GetGrain<ILocalHealthCheckGrain>(Guid.Empty).CheckAsync().ConfigureAwait(false);
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

            return HealthCheckResult.Healthy();
        }
    }
}
