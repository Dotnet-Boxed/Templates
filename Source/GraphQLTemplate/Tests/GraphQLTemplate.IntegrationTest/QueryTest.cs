namespace GraphQLTemplate.IntegrationTest.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using GraphQLTemplate.IntegrationTest.Constants;
    using GraphQLTemplate.IntegrationTest.Fixtures;
    using GraphQLTemplate.IntegrationTest.Models;
    using Xunit;
    using Xunit.Abstractions;

    public class QueryTest : CustomWebApplicationFactory<Startup>
    {
        private readonly HttpClient client;

        public QueryTest(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper) =>
            this.client = this.CreateClient();

        [Fact]
        public async Task IntrospectionQuery_Default_Returns200OkAsync()
        {
            var response = await this.client.PostGraphQLAsync(GraphQlQuery.Introspection).ConfigureAwait(false);

            var graphQlResponse = await response.Content.ReadAsAsync<GraphQLResponse>().ConfigureAwait(false);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Empty(graphQlResponse.Errors);
        }
    }
}
