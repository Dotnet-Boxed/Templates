namespace OrleansTemplate.Server.IntegrationTest.Fixtures
{
    using System;
    using Orleans.TestingHost;

    public class ClusterFixture : IDisposable
    {
        public ClusterFixture()
        {
            var builder = new TestClusterBuilder();
            builder.AddClientBuilderConfigurator<TestClientBuilderConfigurator>();
            builder.AddSiloBuilderConfigurator<TestSiloBuilderConfigurator>();
            this.Cluster = builder.Build();
            this.Cluster.Deploy();
        }

        public void Dispose() => this.Cluster.StopAllSilos();

        public TestCluster Cluster { get; private set; }
    }
}
