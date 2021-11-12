namespace GraphQLTemplate.IntegrationTest;

using System;
using System.Net.Http;
using GraphQLTemplate.Options;
using GraphQLTemplate.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
#if Serilog
using Serilog;
using Serilog.Events;
#endif
#if Serilog
using Xunit.Abstractions;
#endif

public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
    where TEntryPoint : class
{
#if Serilog
    public CustomWebApplicationFactory(ITestOutputHelper testOutputHelper)
#else
    public CustomWebApplicationFactory()
#endif
    {
        this.ClientOptions.AllowAutoRedirect = false;
#if HttpsEverywhere
        this.ClientOptions.BaseAddress = new Uri("https://localhost");
#endif
        this.Server.AllowSynchronousIO = true;
#if Serilog

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Debug()
            .WriteTo.TestOutput(testOutputHelper, LogEventLevel.Verbose)
            .CreateLogger();
#endif
    }

    public ApplicationOptions ApplicationOptions { get; private set; } = default!;

    public Mock<IClockService> ClockServiceMock { get; } = new Mock<IClockService>(MockBehavior.Strict);

    public void VerifyAllMocks() => Mock.VerifyAll(this.ClockServiceMock);

    protected override void ConfigureClient(HttpClient client)
    {
        using (var serviceScope = this.Services.CreateScope())
        {
            var serviceProvider = serviceScope.ServiceProvider;
            this.ApplicationOptions = serviceProvider.GetRequiredService<ApplicationOptions>();
        }

        base.ConfigureClient(client);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder) =>
        builder
            .UseEnvironment(Constants.EnvironmentName.Test)
            .ConfigureServices(this.ConfigureServices);

    protected virtual void ConfigureServices(IServiceCollection services) =>
        services
#if DistributedCacheRedis
            .AddDistributedMemoryCache()
#endif
            .AddSingleton(this.ClockServiceMock.Object);

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.VerifyAllMocks();
        }

        base.Dispose(disposing);
    }
}
