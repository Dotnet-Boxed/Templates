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
    using Xunit.Abstractions;

    public class GraphQLTemplateTest
    {
        public GraphQLTemplateTest(ITestOutputHelper testOutputHelper)
        {
            if (testOutputHelper is null)
            {
                throw new ArgumentNullException(nameof(testOutputHelper));
            }

            TestLogger.WriteMessage = testOutputHelper.WriteLine;
        }

        [Theory]
        [Trait("IsUsingDotnetRun", "false")]
        [InlineData("GraphQLTDefaults")]
        [InlineData("GraphQLTNoForwardedHeaders", "forwarded-headers=false")]
        [InlineData("GraphQLTNoHostFiltering", "host-filtering=false")]
        [InlineData("GraphQLTNoFwdHdrsOrHostFilter", "forwarded-headers=false", "host-filtering=false")]
        [InlineData("GraphQLNoStyleCop", "style-cop=false")]
        public async Task RestoreAndBuild_GraphQLDefaults_SuccessfulAsync(string name, params string[] arguments)
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var dictionary = arguments
                    .Select(x => x.Split('=', StringSplitOptions.RemoveEmptyEntries))
                    .ToDictionary(x => x.First(), x => x.Last());
                var project = await tempDirectory.DotnetNewAsync("graphql", name, dictionary).ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
            }
        }

        [Fact]
        [Trait("IsUsingDotnetRun", "true")]
        public async Task Run_GraphQLDefaults_SuccessfulAsync()
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory.DotnetNewAsync("graphql", "GraphQLTDefaults").ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project
                    .DotnetRunAsync(
                        @"Source\GraphQLTDefaults",
                        ReadinessCheck.StatusSelfAsync,
                        async (httpClient, httpsClient) =>
                        {
                            var httpResponse = await httpClient
                                .GetAsync(new Uri("/", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

                            var httpsResponse = await httpsClient
                                .GetAsync(new Uri("/", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

                            var statusResponse = await httpsClient
                                .GetAsync(new Uri("status", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.OK, statusResponse.StatusCode);

                            var statusSelfResponse = await httpsClient
                                .GetAsync(new Uri("status/self", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.OK, statusSelfResponse.StatusCode);

                            var robotsTxtResponse = await httpsClient
                                .GetAsync(new Uri("robots.txt", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.OK, robotsTxtResponse.StatusCode);

                            var securityTxtResponse = await httpsClient
                                .GetAsync(new Uri(".well-known/security.txt", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.OK, securityTxtResponse.StatusCode);

                            var humansTxtResponse = await httpsClient
                                .GetAsync(new Uri("humans.txt", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.OK, humansTxtResponse.StatusCode);
                        })
                    .ConfigureAwait(false);
            }
        }

        [Fact]
        [Trait("IsUsingDotnetRun", "true")]
        public async Task Run_HealthCheckFalse_SuccessfulAsync()
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory
                    .DotnetNewAsync(
                        "graphql",
                        "GraphQLTHealthCheckFalse",
                        new Dictionary<string, string>()
                        {
                            { "health-check", "false" },
                        })
                    .ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project
                    .DotnetRunAsync(
                        @"Source\GraphQLTHealthCheckFalse",
                        ReadinessCheck.FaviconAsync,
                        async (httpClient, httpsClient) =>
                        {
                            var statusResponse = await httpsClient
                                .GetAsync(new Uri("status", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.NotFound, statusResponse.StatusCode);

                            var statusSelfResponse = await httpsClient
                                .GetAsync(new Uri("status/self", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.NotFound, statusSelfResponse.StatusCode);
                        })
                    .ConfigureAwait(false);
            }
        }

        [Fact]
        [Trait("IsUsingDotnetRun", "true")]
        public async Task Run_QueryGraphQlIntrospection_ReturnsResultsAsync()
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory.DotnetNewAsync("graphql", "GraphQLTDefaults").ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project
                    .DotnetRunAsync(
                        @"Source\GraphQLTDefaults",
                        ReadinessCheck.StatusSelfAsync,
                        async (httpClient, httpsClient) =>
                        {
                            var introspectionQuery = await httpClient
                                .PostGraphQLAsync(GraphQlQuery.Introspection)
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.OK, introspectionQuery.StatusCode);
                            var introspectionContent = await introspectionQuery.Content
                                .ReadAsAsync<GraphQLResponse>()
                                .ConfigureAwait(false);
                            Assert.Empty(introspectionContent.Errors);
                        })
                    .ConfigureAwait(false);
            }
        }

        [Fact]
        [Trait("IsUsingDotnetRun", "true")]
        public async Task Run_HttpsEverywhereFalse_SuccessfulAsync()
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory
                    .DotnetNewAsync(
                        "graphql",
                        "GraphQLTHttpsEverywhereFalse",
                        new Dictionary<string, string>()
                        {
                            { "https-everywhere", "false" },
                        })
                    .ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project
                    .DotnetRunAsync(
                        @"Source\GraphQLTHttpsEverywhereFalse",
                        ReadinessCheck.StatusSelfOverHttpAsync,
                        async (httpClient, httpsClient) =>
                        {
                            var httpResponse = await httpClient
                                .GetAsync(new Uri("/", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
                        })
                    .ConfigureAwait(false);
            }
        }

        [Fact]
        [Trait("IsUsingDotnetRun", "true")]
        public async Task Run_AuthorizationTrue_Returns400BadRequestAsync()
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory
                    .DotnetNewAsync(
                        "graphql",
                        "GraphQLTAuthorizationTrue",
                        new Dictionary<string, string>()
                        {
                            { "authorization", "true" },
                        })
                    .ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project
                    .DotnetRunAsync(
                        @"Source\GraphQLTAuthorizationTrue",
                        ReadinessCheck.StatusSelfAsync,
                        async (httpClient, httpsClient) =>
                        {
                            var httpResponse = await httpsClient
                                .PostGraphQLAsync(
                                    "query getHuman { human(id: \"94fbd693-2027-4804-bf40-ed427fe76fda\") { dateOfBirth } }")
                                .ConfigureAwait(false);
                            var response = await httpResponse.Content
                                .ReadAsAsync<GraphQLResponse>()
                                .ConfigureAwait(false);

                            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
                            var error = Assert.Single(response.Errors);
                            Assert.Equal(
                                "GraphQL.Validation.ValidationError: You are not authorized to run this query.\nRequired claim 'role' with any value of 'admin' is not present.",
                                error.Message);
                        })
                    .ConfigureAwait(false);
            }
        }

        [Fact]
        [Trait("IsUsingDotnetRun", "true")]
        public async Task Run_AuthorizationFalse_DateOfBirthReturnedSuccessfullyAsync()
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory
                    .DotnetNewAsync(
                        "graphql",
                        "GraphQLTAuthorizationFalse",
                        new Dictionary<string, string>()
                        {
                            { "authorization", "false" },
                        })
                    .ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project
                    .DotnetRunAsync(
                        @"Source\GraphQLTAuthorizationFalse",
                        ReadinessCheck.StatusSelfAsync,
                        async (httpClient, httpsClient) =>
                        {
                            var httpResponse = await httpsClient
                                .PostGraphQLAsync(
                                    "query getHuman { human(id: \"94fbd693-2027-4804-bf40-ed427fe76fda\") { dateOfBirth } }")
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
                        })
                    .ConfigureAwait(false);
            }
        }

        private static Task InstallTemplateAsync() => DotnetNew.InstallAsync<ApiTemplateTest>("GraphQLTemplate.sln");
    }
}
