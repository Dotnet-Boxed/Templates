namespace OrleansTemplate.Server.IntegrationTest.Fixtures
{
    using Orleans.Hosting;
    using Orleans.TestingHost;

    public class TestSiloBuilderConfigurator : ISiloBuilderConfigurator
    {
        public void Configure(ISiloHostBuilder siloHostBuilder) =>
            siloHostBuilder
                .AddMemoryGrainStorageAsDefault();
    }
}
