namespace ApiTemplate.IntegrationTest.Fixtures
{
    using System;
    using System.Net.Http;
    using ApiTemplate.Options;
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
        private IServiceScope serviceScope;

        public CustomWebApplicationFactory() =>
            this.ClockServiceMock = new Mock<IClockService>(MockBehavior.Strict);

        public ApplicationOptions ApplicationOptions { get; private set; }

        public Mock<IClockService> ClockServiceMock { get; private set; }

        protected override void ConfigureClient(HttpClient client) =>
            client.BaseAddress = new Uri("http://localhost");

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            builder
                .ConfigureServices(
                    services =>
                    {
                    })
                .ConfigureTestServices(
                    services =>
                    {
                        services.AddSingleton(this.ClockServiceMock.Object);
                    });

            var testServer = base.CreateServer(builder);

            this.serviceScope = testServer.Host.Services.CreateScope();
            var serviceProvider = this.serviceScope.ServiceProvider;
            this.ApplicationOptions = serviceProvider.GetRequiredService<IOptions<ApplicationOptions>>().Value;

            return testServer;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.serviceScope != null)
                {
                    this.serviceScope.Dispose();
                }

                Mock.VerifyAll(this.ClockServiceMock);
            }

            base.Dispose(disposing);
        }
    }
}
