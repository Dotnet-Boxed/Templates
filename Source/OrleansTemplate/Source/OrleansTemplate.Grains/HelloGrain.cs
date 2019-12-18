namespace OrleansTemplate.Grains
{
    using System;
    using System.Threading.Tasks;
    using Orleans;
    using OrleansTemplate.Abstractions.Constants;
    using OrleansTemplate.Abstractions.Grains;

    public class HelloGrain : Grain, IHelloGrain
    {
        public async Task<string> SayHelloAsync(string name)
        {
            await this.IncrementCounterAsync().ConfigureAwait(true);
            await this.PublishSaidHelloAsync(name).ConfigureAwait(true);

            return $"Hello {name}!";
        }

        private Task IncrementCounterAsync()
        {
            var counter = this.GrainFactory.GetGrain<ICounterStatelessGrain>(0L);
            return counter.IncrementAsync();
        }

        private Task PublishSaidHelloAsync(string name)
        {
            var streamProvider = this.GetStreamProvider(StreamProviderName.Default);
            var stream = streamProvider.GetStream<string>(Guid.Empty, StreamName.SaidHello);
            return stream.OnNextAsync(name);
        }
    }
}
