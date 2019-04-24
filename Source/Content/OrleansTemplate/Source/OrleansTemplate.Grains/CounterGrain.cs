namespace OrleansTemplate.Grains
{
    using System.Threading.Tasks;
    using Orleans;
    using OrleansTemplate.Abstractions.Grains;

    public class CounterGrain : Grain<long>, ICounterGrain
    {
        public async Task<long> AddCount(long value)
        {
            this.State += value;
            await this.WriteStateAsync();
            return this.State;
        }

        public Task<long> GetCount() => Task.FromResult(this.State);
    }
}
