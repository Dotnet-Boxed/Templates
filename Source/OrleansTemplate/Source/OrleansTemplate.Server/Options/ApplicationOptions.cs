namespace OrleansTemplate.Server.Options;

using Microsoft.AspNetCore.Server.Kestrel.Core;
using Orleans.Configuration;

public class ApplicationOptions
{
    public ClusterOptions Cluster { get; set; } = default!;

    public KestrelServerOptions Kestrel { get; set; } = default!;

    public QueueOptions Queue { get; set; } = default!;

    public StorageOptions Storage { get; set; } = default!;
}
