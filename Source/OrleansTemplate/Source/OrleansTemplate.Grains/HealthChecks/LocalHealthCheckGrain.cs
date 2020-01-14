namespace OrleansTemplate.Grains
{
    using System.Threading.Tasks;
    using Orleans;
    using Orleans.Concurrency;
    using OrleansTemplate.Abstractions.Grains.HealthChecks;

    [StatelessWorker(1)]
    public class LocalHealthCheckGrain : Grain, ILocalHealthCheckGrain
    {
        public Task CheckAsync() => Task.CompletedTask;
    }
}
