namespace OrleansTemplate.Server.IntegrationTest
{
    using System;
    using System.Threading.Tasks;
    using OrleansTemplate.Abstractions.Grains;
    using OrleansTemplate.Server.IntegrationTest.Fixtures;
    using Xunit;

    public class CounterGrainTest
    {
        [Fact]
        public async Task AddCount_PassValue_ReturnsTotalCount()
        {
            using (var fixture = new ClusterFixture())
            {
                var grain = fixture.Cluster.GrainFactory.GetGrain<ICounterGrain>(Guid.Empty);

                var count = await grain.AddCount(10L);

                Assert.Equal(10L, count);
            }
        }

        [Fact]
        public async Task GetCount_Default_ReturnsTotalCount()
        {
            using (var fixture = new ClusterFixture())
            {
                var grain = fixture.Cluster.GrainFactory.GetGrain<ICounterGrain>(Guid.Empty);

                var count = await grain.GetCount();

                Assert.Equal(0L, count);
            }
        }
    }
}
