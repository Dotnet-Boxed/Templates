namespace OrleansTemplate.Server.IntegrationTest
{
    using System;
    using System.Threading.Tasks;
    using Orleans.Streams;
    using Orleans.TestingHost;
    using OrleansTemplate.Abstractions.Constants;
    using OrleansTemplate.Abstractions.Grains;
    using OrleansTemplate.Server.IntegrationTest.Fixtures;
    using Xunit;

    public class HelloGrainTest : IClassFixture<ClusterFixture>
    {
        private readonly TestCluster cluster;

        public HelloGrainTest(ClusterFixture fixture) => this.cluster = fixture.Cluster;

        [Fact]
        public async Task SayHello_PassName_ReturnsGreeting()
        {
            var grain = this.cluster.GrainFactory.GetGrain<IHelloGrain>(Guid.NewGuid());

            var greeting = await grain.SayHello("Rehan");

            Assert.Equal("Hello Rehan!", greeting);
        }

        [Fact]
        public async Task SayHello_PassName_CountIncremented()
        {
            var helloGrain = this.cluster.GrainFactory.GetGrain<IHelloGrain>(Guid.NewGuid());
            var counterGrain = this.cluster.GrainFactory.GetGrain<ICounterGrain>(Guid.Empty);

            await helloGrain.SayHello("Rehan");

            await Task.Delay(TimeSpan.FromSeconds(2));
            var count = await counterGrain.GetCount();

            Assert.Equal(1L, count);
        }

        [Fact]
        public async Task SayHello_PassName_SaidHelloPublished()
        {
            string hello = null;
            var helloGrain = this.cluster.GrainFactory.GetGrain<IHelloGrain>(Guid.NewGuid());
            var streamProvider = this.cluster.Client.GetStreamProvider(StreamProviderName.Default);
            var stream = streamProvider.GetStream<string>(Guid.Empty, StreamName.SaidHello);
            var subscription = await stream.SubscribeAsync(
                (x, token) =>
                {
                    hello = x;
                    return Task.CompletedTask;
                });

            await helloGrain.SayHello("Rehan");

            Assert.Equal("Rehan", hello);
        }
    }
}
