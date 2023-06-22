namespace OrleansTemplate.Server.IntegrationTest.Fixtures;

using Microsoft.Extensions.Configuration;
using Orleans.TestingHost;
using OrleansTemplate.Abstractions.Constants;

public class TestClientBuilderConfigurator : IClientBuilderConfigurator
{
    public void Configure(IConfiguration configuration, IClientBuilder clientBuilder) =>
        clientBuilder.AddMemoryStreams(StreamProviderName.Default);
}
