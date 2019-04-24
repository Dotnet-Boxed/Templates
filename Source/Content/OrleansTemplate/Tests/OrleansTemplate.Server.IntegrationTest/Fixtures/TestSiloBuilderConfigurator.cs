namespace OrleansTemplate.Server.IntegrationTest.Fixtures
{
    using Orleans.Hosting;
    using Orleans.TestingHost;
    using OrleansTemplate.Abstractions.Constants;

    public class TestSiloBuilderConfigurator : ISiloBuilderConfigurator
    {
        public void Configure(ISiloHostBuilder siloHostBuilder) =>
            siloHostBuilder
                .AddMemoryGrainStorageAsDefault()
                .AddMemoryGrainStorage("PubSubStore")
                .AddSimpleMessageStreamProvider(StreamProviderName.Default);
    }
}
