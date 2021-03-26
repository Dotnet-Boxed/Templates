namespace GraphQLTemplate.IntegrationTest.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;
    using Xunit.Abstractions;

    public class SchemaTest : CustomWebApplicationFactory<Startup>
    {
        private readonly HttpClient client;

        public SchemaTest(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper) =>
            this.client = this.CreateClient();

        [Fact]
        public async Task GetSchemaDescriptionLanguage_Default_Returns200OkAsync()
        {
            var response = await this.client.GetAsync("/graphql?sdl").ConfigureAwait(false);
            var sdl = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(sdl);
        }
    }
}
