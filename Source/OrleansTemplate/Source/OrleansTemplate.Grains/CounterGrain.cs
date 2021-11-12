namespace OrleansTemplate.Grains;

using Orleans;
using OrleansTemplate.Abstractions.Grains;

public class CounterGrain : Grain<long>, ICounterGrain
{
    public async ValueTask<long> AddCountAsync(long value)
    {
        this.State += value;
        await this.WriteStateAsync().ConfigureAwait(true);
        return this.State;
    }

    public ValueTask<long> GetCountAsync() => ValueTask.FromResult(this.State);
}
