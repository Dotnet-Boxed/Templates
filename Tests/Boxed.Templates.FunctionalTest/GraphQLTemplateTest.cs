namespace Boxed.Templates.FunctionalTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Boxed.DotnetNewTest;
    using Boxed.Templates.FunctionalTest.Constants;
    using Boxed.Templates.FunctionalTest.Models;
    using Xunit;

    public class GraphQLTemplateTest
    {
        public GraphQLTemplateTest() =>
            DotnetNew.InstallAsync<GraphQLTemplateTest>("GraphQLTemplate.sln").Wait();

        [Theory]
        [Trait("IsUsingDotnetRun", "false")]
        [InlineData("Default")]
        [InlineData("NoForwardedHeaders", "forwarded-headers=false")]
        [InlineData("NoHostFiltering", "host-filtering=false")]
        [InlineData("NoForwardedHeadersOrHostFiltering", "forwarded-headers=false", "host-filtering=false")]
        public async Task RestoreAndBuild_Default_Successful(string name, params string[] arguments)
        {
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var dictionary = arguments
                    .Select(x => x.Split('=', StringSplitOptions.RemoveEmptyEntries))
                    .ToDictionary(x => x.First(), x => x.Last());
                var project = await tempDirectory.DotnetNewAsync("graphql", name, dictionary);
                await project.DotnetRestoreAsync();
                await project.DotnetBuildAsync();
            }
        }

        [Fact]
        [Trait("IsUsingDotnetRun", "true")]
        public async Task Run_Default_Successful()
        {
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory.DotnetNewAsync("graphql", "Default");
                await project.DotnetRestoreAsync();
                await project.DotnetBuildAsync();
                await project.DotnetRunAsync(
                    @"Source\Default",
                    async (httpClient, httpsClient) =>
                    {
                        var httpResponse = await httpClient.GetAsync("/");
                        Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

                        var httpsResponse = await httpsClient.GetAsync("/");
                        Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

                        var statusResponse = await httpsClient.GetAsync("status");
                        Assert.Equal(HttpStatusCode.OK, statusResponse.StatusCode);

                        var statusSelfResponse = await httpsClient.GetAsync("status/self");
                        Assert.Equal(HttpStatusCode.OK, statusSelfResponse.StatusCode);

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
        [Trait("IsUsingDotnetRun", "true")]
        public async Task Run_HealthCheckFalse_Successful()
        {
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory.DotnetNewAsync(
                    "graphql",
                    "HealthCheckFalse",
                    new Dictionary<string, string>()
                    {
                        { "health-check", "false" },
                    });
                await project.DotnetRestoreAsync();
                await project.DotnetBuildAsync();
                await project.DotnetRunAsync(
                    @"Source\HealthCheckFalse",
                    async httpClient =>
                    {
                        var statusResponse = await httpClient.GetAsync("status");
                        Assert.Equal(HttpStatusCode.NotFound, statusResponse.StatusCode);

                        var statusSelfResponse = await httpClient.GetAsync("status/self");
                        Assert.Equal(HttpStatusCode.NotFound, statusSelfResponse.StatusCode);
                    });
            }
        }

        [Fact]
        [Trait("IsUsingDotnetRun", "true")]
        public async Task Run_QueryGraphQlIntrospection_ReturnsResults()
        {
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory.DotnetNewAsync("graphql", "Default");
                await project.DotnetRestoreAsync();
                await project.DotnetBuildAsync();
                await project.DotnetRunAsync(
                    @"Source\Default",
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
        [Trait("IsUsingDotnetRun", "true")]
        public async Task Run_HttpsEverywhereFalse_Successful()
        {
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory.DotnetNewAsync(
                    "graphql",
                    "HttpsEverywhereFalse",
                    new Dictionary<string, string>()
                    {
                        { "https-everywhere", "false" },
                    });
                await project.DotnetRestoreAsync();
                await project.DotnetBuildAsync();
                await project.DotnetRunAsync(
                    @"Source\HttpsEverywhereFalse",
                    async (httpClient) =>
                    {
                        var httpResponse = await httpClient.GetAsync("/");
                        Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
                    });
            }
        }

        [Fact]
        [Trait("IsUsingDotnetRun", "true")]
        public async Task Run_AuthorizationTrue_Returns400BadRequest()
        {
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory.DotnetNewAsync(
                    "graphql",
                    "AuthorizationTrue",
                    new Dictionary<string, string>()
                    {
                        { "authorization", "true" },
                    });
                await project.DotnetRestoreAsync();
                await project.DotnetBuildAsync();
                await project.DotnetRunAsync(
                    @"Source\AuthorizationTrue",
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
        [Trait("IsUsingDotnetRun", "true")]
        public async Task Run_AuthorizationFalse_DateOfBirthReturnedSuccessfully()
        {
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory.DotnetNewAsync(
                    "graphql",
                    "AuthorizationFalse",
                    new Dictionary<string, string>()
                    {
                        { "authorization", "false" },
                    });
                await project.DotnetRestoreAsync();
                await project.DotnetBuildAsync();
                await project.DotnetRunAsync(
                    @"Source\AuthorizationFalse",
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
