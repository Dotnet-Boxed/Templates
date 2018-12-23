namespace Boxed.Templates.FunctionalTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Boxed.Templates.FunctionalTest.Constants;
    using Boxed.Templates.FunctionalTest.Models;
    using Xunit;

    public class GraphQLTemplateTest
    {
        public GraphQLTemplateTest() =>
            TemplateAssert.DotnetNewInstall<GraphQLTemplateTest>("GraphQLTemplate.csproj").Wait();

        [Theory]
        [InlineData("Default")]
        [InlineData("NoForwardedHeaders", "forwarded-headers=false")]
        [InlineData("NoHostFiltering", "host-filtering=false")]
        [InlineData("NoForwardedHeadersOrHostFiltering", "forwarded-headers=false", "host-filtering=false")]
        public async Task RestoreAndBuild_Default_Successful(string name, params string[] arguments)
        {
            using (var tempDirectory = TemplateAssert.GetTempDirectory())
            {
                var dictionary = arguments
                    .Select(x => x.Split('=', StringSplitOptions.RemoveEmptyEntries))
                    .ToDictionary(x => x.First(), x => x.Last());
                var project = await tempDirectory.DotnetNew("graphql", name, dictionary);
                await project.DotnetRestore();
                await project.DotnetBuild();
            }
        }

        [Fact]
        public async Task Run_Default_Successful()
        {
            using (var tempDirectory = TemplateAssert.GetTempDirectory())
            {
                var project = await tempDirectory.DotnetNew("graphql", "Default");
                await project.DotnetRestore();
                await project.DotnetBuild();
                await project.DotnetRun(
                    async (httpClient, httpsClient) =>
                    {
                        var httpResponse = await httpClient.GetAsync("/");
                        Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

                        var httpsResponse = await httpsClient.GetAsync("/");
                        Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

                        var liveResponse = await httpsClient.GetAsync("status/live");
                        Assert.Equal(HttpStatusCode.OK, liveResponse.StatusCode);

                        var readyResponse = await httpsClient.GetAsync("status/ready");
                        Assert.Equal(HttpStatusCode.OK, readyResponse.StatusCode);

                        var robotsTxtResponse = await httpsClient.GetAsync("robots.txt");
                        Assert.Equal(HttpStatusCode.OK, robotsTxtResponse.StatusCode);

                        var securityTxtResponse = await httpsClient.GetAsync(".well-known/security.txt");
                        Assert.Equal(HttpStatusCode.OK, securityTxtResponse.StatusCode);

                        var humansTxtResponse = await httpsClient.GetAsync("humans.txt");
                        Assert.Equal(HttpStatusCode.OK, humansTxtResponse.StatusCode);
                    });
            }
        }

        [Fact]
        public async Task Run_HealthCheckFalse_Successful()
        {
            using (var tempDirectory = TemplateAssert.GetTempDirectory())
            {
                var project = await tempDirectory.DotnetNew(
                    "graphql",
                    "HealthCheckFalse",
                    new Dictionary<string, string>()
                    {
                        { "health-check", "false" },
                    });
                await project.DotnetRestore();
                await project.DotnetBuild();
                await project.DotnetRun(
                    async httpClient =>
                    {
                        var liveResponse = await httpClient.GetAsync("status/live");
                        Assert.Equal(HttpStatusCode.NotFound, liveResponse.StatusCode);

                        var readyResponse = await httpClient.GetAsync("status/ready");
                        Assert.Equal(HttpStatusCode.NotFound, readyResponse.StatusCode);
                    });
            }
        }

        [Fact]
        public async Task Run_QueryGraphQlIntrospection_ReturnsResults()
        {
            using (var tempDirectory = TemplateAssert.GetTempDirectory())
            {
                var project = await tempDirectory.DotnetNew("graphql", "Default");
                await project.DotnetRestore();
                await project.DotnetBuild();
                await project.DotnetRun(
                    async (httpClient, httpsClient) =>
                    {
                        var introspectionQuery = await httpClient.PostGraphQL(GraphQlQuery.Introspection);
                        Assert.Equal(HttpStatusCode.OK, introspectionQuery.StatusCode);
                        var introspectionContent = await introspectionQuery.Content.ReadAsAsync<GraphQLResponse>();
                        Assert.Null(introspectionContent.Errors);
                    });
            }
        }

        [Fact]
        public async Task Run_HttpsEverywhereFalse_Successful()
        {
            using (var tempDirectory = TemplateAssert.GetTempDirectory())
            {
                var project = await tempDirectory.DotnetNew(
                    "graphql",
                    "HttpsEverywhereFalse",
                    new Dictionary<string, string>()
                    {
                        { "https-everywhere", "false" },
                    });
                await project.DotnetRestore();
                await project.DotnetBuild();
                await project.DotnetRun(
                    async (httpClient) =>
                    {
                        var httpResponse = await httpClient.GetAsync("/");
                        Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
                    });
            }
        }

        [Fact]
        public async Task Run_AuthorizationTrue_Returns400BadRequest()
        {
            using (var tempDirectory = TemplateAssert.GetTempDirectory())
            {
                var project = await tempDirectory.DotnetNew(
                    "graphql",
                    "AuthorizationTrue",
                    new Dictionary<string, string>()
                    {
                        { "authorization", "true" },
                    });
                await project.DotnetRestore();
                await project.DotnetBuild();
                await project.DotnetRun(
                    async (httpClient) =>
                    {
                        var httpResponse = await httpClient.PostGraphQL(
                            "query getHuman { human(id: \"94fbd693-2027-4804-bf40-ed427fe76fda\") { dateOfBirth } }");
                        var response = await httpResponse.Content.ReadAsAsync<GraphQLResponse>();

                        Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
                        var error = Assert.Single(response.Errors);
                        Assert.Equal(
                            "GraphQL.Validation.ValidationError: You are not authorized to run this query.\nRequired claim 'role' with any value of 'admin' is not present.",
                            error.Message);
                    });
            }
        }

        [Fact]
        public async Task Run_AuthorizationFalse_DateOfBirthReturnedSuccessfully()
        {
            using (var tempDirectory = TemplateAssert.GetTempDirectory())
            {
                var project = await tempDirectory.DotnetNew(
                    "graphql",
                    "AuthorizationFalse",
                    new Dictionary<string, string>()
                    {
                        { "authorization", "false" },
                    });
                await project.DotnetRestore();
                await project.DotnetBuild();
                await project.DotnetRun(
                    async (httpClient) =>
                    {
                        var httpResponse = await httpClient.PostGraphQL(
                            "query getHuman { human(id: \"94fbd693-2027-4804-bf40-ed427fe76fda\") { dateOfBirth } }");
                        Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
                    });
            }
        }
    }
}
