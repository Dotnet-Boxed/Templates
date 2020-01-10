namespace OrleansTemplate.Server.IntegrationTest
{
    using System;
    using System.Threading.Tasks;
    using Orleans.Streams;
    using OrleansTemplate.Abstractions.Constants;
    using OrleansTemplate.Abstractions.Grains;
    using OrleansTemplate.Server.IntegrationTest.Fixtures;
    using Xunit;
    using Xunit.Abstractions;

    public class HelloGrainTest : ClusterFixture
    {
        public HelloGrainTest(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Fact]
        public async Task SayHello_PassName_ReturnsGreetingAsync()
        {
            var grain = this.Cluster.GrainFactory.GetGrain<IHelloGrain>(Guid.NewGuid());

            var greeting = await grain.SayHelloAsync("Rehan").ConfigureAwait(false);

            Assert.Equal("Hello Rehan!", greeting);
        }

        [Fact]
        public async Task SayHello_PassName_CountIncrementedAsync()
        {
            var helloGrain = this.Cluster.GrainFactory.GetGrain<IHelloGrain>(Guid.NewGuid());
            var counterGrain = this.Cluster.GrainFactory.GetGrain<ICounterGrain>(Guid.Empty);

            await helloGrain.SayHelloAsync("Rehan").ConfigureAwait(false);

            await Task.Delay(TimeSpan.FromSeconds(2)).ConfigureAwait(false);
            var count = await counterGrain.GetCountAsync().ConfigureAwait(false);

            Assert.Equal(1L, count);
        }

        [Fact]
        public async Task SayHello_PassName_SaidHelloPublishedAsync()
        {
            string hello = null;
            var helloGrain = this.Cluster.GrainFactory.GetGrain<IHelloGrain>(Guid.NewGuid());
            var streamProvider = this.Cluster.Client.GetStreamProvider(StreamProviderName.Default);
            var stream = streamProvider.GetStream<string>(Guid.Empty, StreamName.SaidHello);
            var subscription = await stream
                .SubscribeAsync(
                    (x, token) =>
                    {
                        hello = x;
                        return Task.CompletedTask;
                    })
                .ConfigureAwait(false);

            await helloGrain.SayHelloAsync("Rehan").ConfigureAwait(false);

            Assert.Equal("Rehan", hello);
        }
    }
}
