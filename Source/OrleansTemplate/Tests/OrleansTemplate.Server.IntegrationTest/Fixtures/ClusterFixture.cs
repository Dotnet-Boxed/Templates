namespace OrleansTemplate.Server.IntegrationTest.Fixtures
{
    using Orleans.TestingHost;
    using Serilog;
    using Serilog.Events;
    using Xunit.Abstractions;

    public class ClusterFixture : Disposable
    {
        public ClusterFixture(ITestOutputHelper testOutputHelper)
        {
            this.TestOutputHelper = testOutputHelper;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Debug()
                .WriteTo.TestOutput(testOutputHelper, LogEventLevel.Verbose)
                .CreateLogger();

            this.Cluster = this.CreateTestCluster();
            this.Cluster.Deploy();
        }

        public TestCluster Cluster { get; }

        public ITestOutputHelper TestOutputHelper { get; }

#pragma warning disable CA1822 // Mark members as static
        public TestCluster CreateTestCluster() =>
#pragma warning restore CA1822 // Mark members as static
            new TestClusterBuilder()
                .AddClientBuilderConfigurator<TestClientBuilderConfigurator>()
                .AddSiloBuilderConfigurator<TestSiloConfigurator>()
                .Build();

        // Switch to IAsyncDisposable.DisposeAsync and call Cluster.DisposeAsync in the next Orleans update.
        protected override void DisposeManaged() => this.Cluster.Dispose();
    }
}
