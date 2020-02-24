namespace OrleansTemplate.Server.IntegrationTest.Fixtures
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Orleans.Hosting;
    using Orleans.TestingHost;
    using OrleansTemplate.Abstractions.Constants;
    using Serilog.Extensions.Logging;

    public class TestSiloConfigurator : ISiloConfigurator
    {
        public void Configure(ISiloBuilder siloBuilder) =>
            siloBuilder
                .ConfigureServices(services => services.AddSingleton<ILoggerFactory>(x => new SerilogLoggerFactory()))
                .AddMemoryGrainStorageAsDefault()
                .AddMemoryGrainStorage("PubSubStore")
                .AddSimpleMessageStreamProvider(StreamProviderName.Default);
    }
}
