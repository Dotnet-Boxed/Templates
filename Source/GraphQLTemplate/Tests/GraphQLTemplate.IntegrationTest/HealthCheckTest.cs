namespace GraphQLTemplate.IntegrationTest.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using GraphQLTemplate.IntegrationTest.Fixtures;
    using Xunit;
    using Xunit.Abstractions;

    public class HealthCheckTest : CustomWebApplicationFactory<Startup>
    {
        private readonly HttpClient client;

        public HealthCheckTest(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper) =>
            this.client = this.CreateClient();

        [Fact]
        public async Task GetStatus_Default_Returns200OkAsync()
        {
            var response = await this.client.GetAsync(new Uri("/status", UriKind.Relative)).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetStatusSelf_Default_Returns200OkAsync()
        {
            var response = await this.client.GetAsync(new Uri("/status/self", UriKind.Relative)).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
