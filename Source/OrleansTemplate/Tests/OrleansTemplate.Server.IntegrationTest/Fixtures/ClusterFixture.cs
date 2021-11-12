namespace OrleansTemplate.Server.IntegrationTest.Fixtures;

using System.Threading.Tasks;
using Orleans.TestingHost;
#if Serilog
using Serilog;
using Serilog.Events;
#endif
using Xunit;
using Xunit.Abstractions;

public class ClusterFixture : IAsyncLifetime
{
    public ClusterFixture(ITestOutputHelper testOutputHelper)
    {
        this.TestOutputHelper = testOutputHelper;

#if Serilog
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Debug()
            .WriteTo.TestOutput(testOutputHelper, LogEventLevel.Verbose)
            .CreateLogger();

#endif
        this.Cluster = new TestClusterBuilder()
            .AddClientBuilderConfigurator<TestClientBuilderConfigurator>()
            .AddSiloBuilderConfigurator<TestSiloConfigurator>()
            .Build();
    }

    public TestCluster Cluster { get; }

    public ITestOutputHelper TestOutputHelper { get; }

    public Task DisposeAsync() => this.Cluster.DisposeAsync().AsTask();

    public Task InitializeAsync() => this.Cluster.DeployAsync();
}
