namespace OrleansTemplate.Grains
{
    using System;
    using System.Threading.Tasks;
    using Orleans;
    using OrleansTemplate.Abstractions.Constants;
    using OrleansTemplate.Abstractions.Grains;

    public class HelloGrain : Grain, IHelloGrain
    {
        public async Task<string> SayHello(string name)
        {
            await this.IncrementCounter();
            await this.PublishSaidHello(name);

            return $"Hello {name}!";
        }

        private Task IncrementCounter()
        {
            var counter = this.GrainFactory.GetGrain<ICounterStatelessGrain>(0L);
            return counter.Increment();
        }

        private Task PublishSaidHello(string name)
        {
            var streamProvider = this.GetStreamProvider(StreamProviderName.Default);
            var stream = streamProvider.GetStream<string>(Guid.Empty, StreamName.SaidHello);
            return stream.OnNextAsync(name);
        }
    }
}
