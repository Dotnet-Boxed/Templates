namespace ApiTemplate.IntegrationTest.Fixtures
{
    using System;
    using ApiTemplate.Options;
    using ApiTemplate.Repositories;
    using ApiTemplate.Services;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Moq;

    public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
        where TEntryPoint : class
    {
        public CustomWebApplicationFactory()
        {
            this.ClientOptions.AllowAutoRedirect = false;
#if HttpsEverywhere
            this.ClientOptions.BaseAddress = new Uri("https://localhost");
#endif
        }

        public ApplicationOptions ApplicationOptions { get; private set; }

        public Mock<ICarRepository> CarRepositoryMock { get; private set; }

        public Mock<IClockService> ClockServiceMock { get; private set; }

        public void VerifyAllMocks() => Mock.VerifyAll(this.CarRepositoryMock, this.ClockServiceMock);

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            this.CarRepositoryMock = new Mock<ICarRepository>(MockBehavior.Strict);
            this.ClockServiceMock = new Mock<IClockService>(MockBehavior.Strict);

            builder
                .UseEnvironment("Testing")
                .ConfigureServices(
                    services =>
                    {
                    })
                .ConfigureTestServices(
                    services =>
                    {
                        services.AddSingleton(this.CarRepositoryMock.Object);
                        services.AddSingleton(this.ClockServiceMock.Object);
                    });

            var testServer = base.CreateServer(builder);

            using (var serviceScope = testServer.Host.Services.CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;
                this.ApplicationOptions = serviceProvider.GetRequiredService<IOptions<ApplicationOptions>>().Value;
            }

            return testServer;
        }
    }
}
