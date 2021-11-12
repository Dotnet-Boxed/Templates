namespace OrleansTemplate.Grains;

using Orleans;
using Orleans.Concurrency;
using OrleansTemplate.Abstractions.Grains;

/// <summary>
/// An implementation of the 'Reduce' pattern (See https://github.com/OrleansContrib/DesignPatterns/blob/master/Reduce.md).
/// </summary>
/// <seealso cref="Grain" />
/// <seealso cref="ICounterStatelessGrain" />
[StatelessWorker]
public class CounterStatelessGrain : Grain, ICounterStatelessGrain
{
    private long count;

    public ValueTask IncrementAsync()
    {
        this.count += 1;
        return ValueTask.CompletedTask;
    }

    public override Task OnActivateAsync()
    {
        // Timers are stored in-memory so are not resilient to nodes going down. They should be used for short
        // high-frequency timers their period should be measured in seconds.
        this.RegisterTimer(this.OnTimerTickAsync, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
        return base.OnActivateAsync();
    }

    private async Task OnTimerTickAsync(object arg)
    {
        var count = this.count;
        this.count = 0;
        var counter = this.GrainFactory.GetGrain<ICounterGrain>(Guid.Empty);
        await counter.AddCountAsync(count).ConfigureAwait(true);
    }
}
