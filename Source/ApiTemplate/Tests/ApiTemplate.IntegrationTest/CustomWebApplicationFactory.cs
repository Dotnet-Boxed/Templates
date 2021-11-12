namespace ApiTemplate.IntegrationTest;

using ApiTemplate.Options;
#if Controllers
using ApiTemplate.Repositories;
using ApiTemplate.Services;
#endif
using Microsoft.AspNetCore.Mvc.Testing;
#if Controllers
using Moq;
#endif
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
#if Serilog

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Debug()
            .WriteTo.TestOutput(testOutputHelper, LogEventLevel.Verbose)
            .CreateLogger();
#endif
    }

    public ApplicationOptions ApplicationOptions { get; private set; } = default!;

#if Controllers
    public Mock<ICarRepository> CarRepositoryMock { get; } = new Mock<ICarRepository>(MockBehavior.Strict);

    public Mock<IClockService> ClockServiceMock { get; } = new Mock<IClockService>(MockBehavior.Strict);

    public void VerifyAllMocks() => Mock.VerifyAll(this.CarRepositoryMock, this.ClockServiceMock);

#endif
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

    protected virtual void ConfigureServices(IServiceCollection services)
    {
#if DistributedCacheRedis
        services.AddDistributedMemoryCache();
#endif
#if Controllers
        services
            .AddSingleton(this.CarRepositoryMock.Object)
            .AddSingleton(this.ClockServiceMock.Object);
#endif
    }
#if Controllers

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.VerifyAllMocks();
        }

        base.Dispose(disposing);
    }
#endif
}
