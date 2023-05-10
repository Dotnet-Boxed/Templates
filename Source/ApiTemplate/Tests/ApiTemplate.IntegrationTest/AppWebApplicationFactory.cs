namespace ApiTemplate.IntegrationTest;

using System.Globalization;
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

public class AppWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
    where TEntryPoint : class
{
#if Serilog
    public AppWebApplicationFactory(ITestOutputHelper testOutputHelper)
    {
        this.ClientOptions.AllowAutoRedirect = false;
#if HttpsEverywhere
        this.ClientOptions.BaseAddress = new Uri("https://localhost");
#endif

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Debug(formatProvider: CultureInfo.InvariantCulture)
            .WriteTo.TestOutput(testOutputHelper, LogEventLevel.Verbose, formatProvider: CultureInfo.InvariantCulture)
            .CreateLogger();
    }
#elif HttpsEverywhere
    public CustomWebApplicationFactory()
    {
        this.ClientOptions.AllowAutoRedirect = false;
        this.ClientOptions.BaseAddress = new Uri("https://localhost");
    }
#else
    public CustomWebApplicationFactory() => this.ClientOptions.AllowAutoRedirect = false;
#endif

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
#if (DistributedCacheRedis || Controllers)
            .ConfigureServices(this.ConfigureServices)
#endif
            .UseEnvironment(Constants.EnvironmentName.Test);
#if (DistributedCacheRedis && Controllers)

    protected virtual void ConfigureServices(IServiceCollection services) =>
        services
            .AddDistributedMemoryCache()
            .AddSingleton(this.CarRepositoryMock.Object)
            .AddSingleton(this.ClockServiceMock.Object);
#elif DistributedCacheRedis

    protected virtual void ConfigureServices(IServiceCollection services) =>
        services
            .AddDistributedMemoryCache();
#elif Controllers

    protected virtual void ConfigureServices(IServiceCollection services) =>
        services
            .AddSingleton(this.CarRepositoryMock.Object)
            .AddSingleton(this.ClockServiceMock.Object);
#endif
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
