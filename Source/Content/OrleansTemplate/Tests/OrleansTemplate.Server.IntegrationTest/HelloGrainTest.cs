namespace OrleansTemplate.Server.IntegrationTest
{
    using System;
    using System.Threading.Tasks;
    using Orleans.TestingHost;
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
            var helloGrain = this.cluster.GrainFactory.GetGrain<IHelloGrain>(Guid.NewGuid());

            var greeting = await helloGrain.SayHello("Rehan");

            Assert.Equal("Hello Rehan!", greeting);
        }
    }
}
