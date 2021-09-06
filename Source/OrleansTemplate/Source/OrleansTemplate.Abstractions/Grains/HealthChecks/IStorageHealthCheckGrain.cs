namespace OrleansTemplate.Abstractions.Grains.HealthChecks
{
    using System.Threading.Tasks;
    using Orleans;

    public interface IStorageHealthCheckGrain : IGrainWithGuidKey
    {
        ValueTask CheckAsync();
    }
}
