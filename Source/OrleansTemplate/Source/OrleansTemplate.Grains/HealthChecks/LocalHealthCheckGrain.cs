namespace OrleansTemplate.Grains;

using Orleans;
using Orleans.Concurrency;
using OrleansTemplate.Abstractions.Grains.HealthChecks;

[StatelessWorker(1)]
public class LocalHealthCheckGrain : Grain, ILocalHealthCheckGrain
{
    public ValueTask CheckAsync() => ValueTask.CompletedTask;
}
