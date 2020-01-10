namespace OrleansTemplate.Server.IntegrationTest
{
    using System;
    using System.Threading.Tasks;
    using OrleansTemplate.Abstractions.Grains;
    using OrleansTemplate.Server.IntegrationTest.Fixtures;
    using Xunit;
    using Xunit.Abstractions;

    public class CounterStatelessGrainTest : ClusterFixture
    {
        public CounterStatelessGrainTest(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Fact]
        public async Task Increment_Default_EventuallyIncrementsTotalCountAsync()
        {
            var grain = this.Cluster.GrainFactory.GetGrain<ICounterStatelessGrain>(0L);
            var counterGrain = this.Cluster.GrainFactory.GetGrain<ICounterGrain>(Guid.Empty);

            await grain.IncrementAsync().ConfigureAwait(false);
            var countBefore = await counterGrain.GetCountAsync().ConfigureAwait(false);

            Assert.Equal(0L, countBefore);

            await Task.Delay(TimeSpan.FromSeconds(2)).ConfigureAwait(false);

            var countAfter = await counterGrain.GetCountAsync().ConfigureAwait(false);

            Assert.Equal(1L, countAfter);
        }
    }
}
