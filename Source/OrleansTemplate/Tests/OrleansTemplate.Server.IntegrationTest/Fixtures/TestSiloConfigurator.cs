namespace OrleansTemplate.Server.IntegrationTest.Fixtures;
#if Serilog
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
#endif
using Orleans.Hosting;
using Orleans.TestingHost;
using OrleansTemplate.Abstractions.Constants;
#if Serilog
using Serilog.Extensions.Logging;
#endif

public class TestSiloConfigurator : ISiloConfigurator
{
    public void Configure(ISiloBuilder siloBuilder) =>
        siloBuilder
#if Serilog
            .ConfigureServices(services => services.AddSingleton<ILoggerFactory>(x => new SerilogLoggerFactory()))
#endif
            .AddMemoryGrainStorageAsDefault()
            .AddMemoryGrainStorage("PubSubStore")
            .AddSimpleMessageStreamProvider(StreamProviderName.Default);
}
