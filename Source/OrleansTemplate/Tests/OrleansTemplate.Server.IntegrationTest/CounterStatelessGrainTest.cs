namespace OrleansTemplate.Server.IntegrationTest
{
    using System;
    using System.Threading.Tasks;
    using OrleansTemplate.Abstractions.Grains;
    using OrleansTemplate.Server.IntegrationTest.Fixtures;
    using Xunit;

    public class CounterStatelessGrainTest : ClusterFixture
    {
        [Fact]
        public async Task Increment_Default_EventuallyIncrementsTotalCount()
        {
            var grain = this.Cluster.GrainFactory.GetGrain<ICounterStatelessGrain>(0L);
            var counterGrain = this.Cluster.GrainFactory.GetGrain<ICounterGrain>(Guid.Empty);

            await grain.Increment();
            var countBefore = await counterGrain.GetCount();

            Assert.Equal(0L, countBefore);

            await Task.Delay(TimeSpan.FromSeconds(2));

            var countAfter = await counterGrain.GetCount();
        }
    }
}
