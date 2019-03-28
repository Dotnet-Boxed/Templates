namespace OrleansTemplate.Server.Options
{
    using Orleans.Configuration;

    public class ApplicationOptions
    {
        public ClusterOptions Cluster { get; set; }

        public StorageOptions Storage { get; set; }
    }
}
