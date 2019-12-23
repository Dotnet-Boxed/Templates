namespace OrleansTemplate.Server.IntegrationTest.Fixtures
{
    using Orleans.TestingHost;

    public class ClusterFixture : Disposable
    {
        public ClusterFixture()
        {
            this.Cluster = this.CreateTestCluster();
            this.Cluster.Deploy();
        }

        public TestCluster Cluster { get; }

#pragma warning disable CA1822 // Mark members as static
        public TestCluster CreateTestCluster() =>
#pragma warning restore CA1822 // Mark members as static
            new TestClusterBuilder()
                .AddClientBuilderConfigurator<TestClientBuilderConfigurator>()
                .AddSiloBuilderConfigurator<TestSiloBuilderConfigurator>()
                .Build();

        // Switch to IAsyncDisposable.DisposeAsync and call Cluster.DisposeAsync in the next Orleans update.
        protected override void DisposeManaged() => this.Cluster.Dispose();
    }
}
