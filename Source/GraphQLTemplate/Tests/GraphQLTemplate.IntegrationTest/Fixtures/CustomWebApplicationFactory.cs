namespace GraphQLTemplate.IntegrationTest.Fixtures
{
    using System;
    using System.Net.Http;
    using GraphQLTemplate.Options;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        private IServiceScope serviceScope;

        public ApplicationOptions ApplicationOptions { get; private set; }

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
            }

            base.Dispose(disposing);
        }
    }
}
