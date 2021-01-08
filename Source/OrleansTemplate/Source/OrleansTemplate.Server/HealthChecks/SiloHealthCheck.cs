namespace OrleansTemplate.Server.HealthChecks
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Orleans.Runtime;

    /// <summary>
    /// Verifies if Orleans services that support health checks implementing <see cref="IHealthCheckParticipant"/>
    /// are healthy.
    /// </summary>
    public class SiloHealthCheck : IHealthCheck
    {
        private static long lastCheckTime = DateTime.UtcNow.ToBinary();
        private readonly IEnumerable<IHealthCheckParticipant> participants;

        public SiloHealthCheck(IEnumerable<IHealthCheckParticipant> participants) => this.participants = participants;

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var thisLastCheckTime = DateTime.FromBinary(Interlocked.Exchange(ref lastCheckTime, DateTime.UtcNow.ToBinary()));

            foreach (var participant in this.participants)
            {
                if (!participant.CheckHealth(thisLastCheckTime, out var reason))
                {
                    return Task.FromResult(HealthCheckResult.Degraded(reason));
                }
            }

            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}
