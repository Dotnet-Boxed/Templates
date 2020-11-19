namespace OrleansTemplate.Server.Options
{
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Orleans.Configuration;

    public class ApplicationOptions
    {
#if ApplicationInsights
        public ApplicationInsightsTelemetryConsumerOptions ApplicationInsights { get; set; }

#endif
        public ClusterOptions Cluster { get; set; }

        public KestrelServerOptions Kestrel { get; set; }

        public StorageOptions Storage { get; set; }
    }
}
