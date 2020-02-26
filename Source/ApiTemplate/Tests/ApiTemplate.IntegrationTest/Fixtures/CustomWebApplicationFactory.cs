namespace ApiTemplate.IntegrationTest.Fixtures
{
    using System;
    using System.Net.Http;
    using ApiTemplate.Options;
    using ApiTemplate.Repositories;
    using ApiTemplate.Services;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using Moq;
    using Serilog;
    using Serilog.Events;
    using Xunit.Abstractions;

    public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
        where TEntryPoint : class
    {
        public CustomWebApplicationFactory(ITestOutputHelper testOutputHelper)
        {
            this.ClientOptions.AllowAutoRedirect = false;
#if HttpsEverywhere
            this.ClientOptions.BaseAddress = new Uri("https://localhost");
#endif

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Debug()
                .WriteTo.TestOutput(testOutputHelper, LogEventLevel.Verbose)
                .CreateLogger();
        }

        public ApplicationOptions ApplicationOptions { get; private set; }

        public Mock<ICarRepository> CarRepositoryMock { get; private set; }

        public Mock<IClockService> ClockServiceMock { get; private set; }

        public void VerifyAllMocks() => Mock.VerifyAll(this.CarRepositoryMock, this.ClockServiceMock);

        protected override void ConfigureClient(HttpClient client)
        {
            using (var serviceScope = this.Services.CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;
                this.ApplicationOptions = serviceProvider.GetRequiredService<IOptions<ApplicationOptions>>().Value;
                this.CarRepositoryMock = serviceProvider.GetRequiredService<Mock<ICarRepository>>();
                this.ClockServiceMock = serviceProvider.GetRequiredService<Mock<IClockService>>();
            }

            base.ConfigureClient(client);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder) =>
            builder
                .UseEnvironment("Test")
                .UseStartup<TestStartup>();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.VerifyAllMocks();
            }

            base.Dispose(disposing);
        }
    }
}
