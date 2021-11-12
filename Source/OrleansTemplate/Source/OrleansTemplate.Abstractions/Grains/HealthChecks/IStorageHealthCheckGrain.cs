namespace OrleansTemplate.Abstractions.Grains.HealthChecks;

using Orleans;

public interface IStorageHealthCheckGrain : IGrainWithGuidKey
{
    ValueTask CheckAsync();
}
