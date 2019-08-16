namespace ApiTemplate.IntegrationTest.Fixtures
{
    using System;
    using System.Net.Http;
    using ApiTemplate.Options;
    using ApiTemplate.Repositories;
    using ApiTemplate.Services;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Moq;

    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        public CustomWebApplicationFactory()
        {
            this.CarRepositoryMock = new Mock<ICarRepository>(MockBehavior.Strict);
            this.ClockServiceMock = new Mock<IClockService>(MockBehavior.Strict);
        }

        public ApplicationOptions ApplicationOptions { get; private set; }

        public Mock<ICarRepository> CarRepositoryMock { get; }

        public Mock<IClockService> ClockServiceMock { get; }

        public new HttpClient CreateClient() =>
            base.CreateClient(new WebApplicationFactoryClientOptions() { AllowAutoRedirect = false });

        public void VerifyAllMocks() => Mock.VerifyAll(this.CarRepositoryMock, this.ClockServiceMock);

#if HttpsEverywhere
        protected override void ConfigureClient(HttpClient client) =>
            client.BaseAddress = new Uri("https://localhost");

#endif
        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            builder
                .ConfigureServices(
                    services =>
                    {
                        var serviceProvider = services.BuildServiceProvider();
                        using (var serviceScope = serviceProvider.CreateScope())
                        {
                            this.ApplicationOptions = serviceProvider.GetRequiredService<IOptions<ApplicationOptions>>().Value;
                        }
                    })
                .ConfigureTestServices(
                    services =>
                    {
                        services.AddSingleton(this.CarRepositoryMock.Object);
                        services.AddSingleton(this.ClockServiceMock.Object);
                    });

            return base.CreateServer(builder);
        }
    }
}
