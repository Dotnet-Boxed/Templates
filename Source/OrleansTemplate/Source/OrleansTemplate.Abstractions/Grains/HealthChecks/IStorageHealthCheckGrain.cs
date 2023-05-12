namespace OrleansTemplate.Abstractions.Grains.HealthChecks;

public interface IStorageHealthCheckGrain : IGrainWithGuidKey
{
    ValueTask CheckAsync();
}
