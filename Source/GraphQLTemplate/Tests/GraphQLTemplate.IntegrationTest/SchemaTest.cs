namespace GraphQLTemplate.IntegrationTest.Controllers;

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
#if Serilog
using Xunit.Abstractions;
#endif

public class SchemaTest : CustomWebApplicationFactory<Program>
{
    private readonly HttpClient client;

#if Serilog
    public SchemaTest(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) =>
#else
    public SchemaTest() =>
#endif
            this.client = this.CreateClient();

    [Fact]
    public async Task GetSchemaDescriptionLanguage_Default_Returns200OkAsync()
    {
        var response = await this.client.GetAsync(new Uri("/graphql?sdl", UriKind.Relative)).ConfigureAwait(false);
        var sdl = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotEmpty(sdl);
    }
}
