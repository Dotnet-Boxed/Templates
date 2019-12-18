namespace OrleansTemplate.Grains
{
    using System.Threading.Tasks;
    using Orleans;
    using OrleansTemplate.Abstractions.Grains;

    public class CounterGrain : Grain<long>, ICounterGrain
    {
        public async Task<long> AddCountAsync(long value)
        {
            this.State += value;
            await this.WriteStateAsync().ConfigureAwait(true);
            return this.State;
        }

        public Task<long> GetCountAsync() => Task.FromResult(this.State);
    }
}
