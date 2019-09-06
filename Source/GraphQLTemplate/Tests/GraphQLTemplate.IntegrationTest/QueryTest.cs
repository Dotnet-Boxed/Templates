namespace GraphQLTemplate.IntegrationTest.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using GraphQLTemplate.IntegrationTest.Constants;
    using GraphQLTemplate.IntegrationTest.Fixtures;
    using GraphQLTemplate.IntegrationTest.Models;
    using Xunit;

    public class QueryTest : CustomWebApplicationFactory<Startup>
    {
        private readonly HttpClient client;

        public QueryTest() => this.client = this.CreateClient();

        [Fact]
        public async Task IntrospectionQuery_Default_Returns200Ok()
        {
            var introspectionQuery = await this.client.PostGraphQL(GraphQlQuery.Introspection);
            var introspectionContent = await introspectionQuery.Content.ReadAsAsync<GraphQLResponse>();

            Assert.Equal(HttpStatusCode.OK, introspectionQuery.StatusCode);
            Assert.Null(introspectionContent.Errors);
        }
    }
}
