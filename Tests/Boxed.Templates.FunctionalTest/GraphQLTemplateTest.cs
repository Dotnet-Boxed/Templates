namespace Boxed.Templates.FunctionalTest
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Boxed.DotnetNewTest;
    using Boxed.Templates.FunctionalTest.Constants;
    using Boxed.Templates.FunctionalTest.Models;
    using Xunit;
    using Xunit.Abstractions;

    [Trait("Template", "GraphQL")]
    public class GraphQLTemplateTest
    {
        private const string TemplateName = "graphql";
        private const string SolutionFileName = "GraphQLTemplate.sln";
        private static readonly string[] DefaultArguments = new string[]
        {
            "no-open-todo=true",
            "https-port={HTTPS_PORT}",
            "http-port={HTTP_PORT}",
        };

        public GraphQLTemplateTest(ITestOutputHelper testOutputHelper)
        {
            if (testOutputHelper is null)
            {
                throw new ArgumentNullException(nameof(testOutputHelper));
            }

            TestLogger.WriteMessage = testOutputHelper.WriteLine;
        }

        [Theory]
        [Trait("IsUsingDocker", "false")]
        [Trait("IsUsingDotnetRun", "false")]
        [InlineData("GraphQLTDefaults")]
        [InlineData("GraphQLTNoForwardedHeaders", "forwarded-headers=false")]
        [InlineData("GraphQLTNoHostFiltering", "host-filtering=false")]
        [InlineData("GraphQLTNoFwdHdrsOrHostFilter", "forwarded-headers=false", "host-filtering=false")]
        [InlineData("GraphQLStyleCop", "style-cop=true")]
        [InlineData("GraphQLOpenTelemetry", "open-telemetry=true")]
        public async Task RestoreBuildTest_GraphQLDefaults_SuccessfulAsync(string name, params string[] arguments)
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            await using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory
                    .DotnetNewAsync(TemplateName, name, DefaultArguments.ToArguments(arguments))
                    .ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project.DotnetTestAsync().ConfigureAwait(false);
            }
        }

        [Theory]
        [Trait("IsUsingDocker", "true")]
        [Trait("IsUsingDotnetRun", "false")]
        [InlineData("GraphQLTDefaults")]
        public async Task Cake_GraphQLDefaults_SuccessfulAsync(string name, params string[] arguments)
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            await using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory
                    .DotnetNewAsync(TemplateName, name, DefaultArguments.ToArguments(arguments))
                    .ConfigureAwait(false);
                await project.DotnetToolRestoreAsync().ConfigureAwait(false);
                await project.DotnetCakeAsync(timeout: TimeSpan.FromMinutes(5)).ConfigureAwait(false);
            }
        }

        [Fact]
        [Trait("IsUsingDocker", "false")]
        [Trait("IsUsingDotnetRun", "true")]
        public async Task RestoreBuildTestRun_GraphQLDefaults_SuccessfulAsync()
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            await using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory
                    .DotnetNewAsync(TemplateName, "GraphQLTDefaults", DefaultArguments.ToArguments())
                    .ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project.DotnetTestAsync().ConfigureAwait(false);
                await project
                    .DotnetRunAsync(
                        Path.Join("Source", "GraphQLTDefaults"),
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
        [Trait("IsUsingDocker", "false")]
        [Trait("IsUsingDotnetRun", "true")]
        public async Task RestoreBuildTestRun_HealthCheckFalse_SuccessfulAsync()
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            await using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory
                    .DotnetNewAsync(
                        TemplateName,
                        "GraphQLTHealthCheckFalse",
                        DefaultArguments.ToArguments(new string[] { "health-check=false" }))
                    .ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project.DotnetTestAsync().ConfigureAwait(false);
                await project
                    .DotnetRunAsync(
                        Path.Join("Source", "GraphQLTHealthCheckFalse"),
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
        [Trait("IsUsingDocker", "false")]
        [Trait("IsUsingDotnetRun", "true")]
        public async Task RestoreBuildTestRun_QueryGraphQlIntrospection_ReturnsResultsAsync()
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            await using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory
                    .DotnetNewAsync(TemplateName, "GraphQLTDefaults", DefaultArguments.ToArguments())
                    .ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project.DotnetTestAsync().ConfigureAwait(false);
                await project
                    .DotnetRunAsync(
                        Path.Join("Source", "GraphQLTDefaults"),
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
        [Trait("IsUsingDocker", "false")]
        [Trait("IsUsingDotnetRun", "true")]
        public async Task RestoreBuildTestRun_HttpsEverywhereTrue_SuccessfulAsync()
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            await using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory
                    .DotnetNewAsync(
                        TemplateName,
                        "GraphQLTHttpsEverywhereTrue",
                        DefaultArguments.ToArguments(new string[] { "https-everywhere=true" }))
                    .ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project.DotnetTestAsync().ConfigureAwait(false);
                await project
                    .DotnetRunAsync(
                        Path.Join("Source", "GraphQLTHttpsEverywhereTrue"),
                        ReadinessCheck.StatusSelfOverHttpAsync,
                        async (httpClient, httpsClient) =>
                        {
                            var httpResponse = await httpsClient
                                .GetAsync(new Uri("/", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
                        })
                    .ConfigureAwait(false);

                var files = new DirectoryInfo(project.DirectoryPath).GetFiles("*.*", SearchOption.AllDirectories);

                var dockerfileInfo = files.First(x => x.Name == "Dockerfile");
                var dockerfile = File.ReadAllText(dockerfileInfo.FullName);
                Assert.Contains("443", dockerfile, StringComparison.Ordinal);
            }
        }

        [Fact]
        [Trait("IsUsingDocker", "false")]
        [Trait("IsUsingDotnetRun", "true")]
        public async Task RestoreBuildTestRun_AuthorizationTrue_Returns400BadRequestAsync()
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            await using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory
                    .DotnetNewAsync(
                        TemplateName,
                        "GraphQLTAuthorizationTrue",
                        DefaultArguments.ToArguments(new string[] { "authorization=true" }))
                    .ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project.DotnetTestAsync().ConfigureAwait(false);
                await project
                    .DotnetRunAsync(
                        Path.Join("Source", "GraphQLTAuthorizationTrue"),
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
        [Trait("IsUsingDocker", "false")]
        [Trait("IsUsingDotnetRun", "true")]
        public async Task RestoreBuildTestRun_AuthorizationFalse_DateOfBirthReturnedSuccessfullyAsync()
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            await using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory
                    .DotnetNewAsync(
                        TemplateName,
                        "GraphQLTAuthorizationFalse",
                        DefaultArguments.ToArguments(new string[] { "authorization=false" }))
                    .ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project.DotnetTestAsync().ConfigureAwait(false);
                await project
                    .DotnetRunAsync(
                        Path.Join("Source", "GraphQLTAuthorizationFalse"),
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

        [Fact]
        [Trait("IsUsingDocker", "false")]
        [Trait("IsUsingDotnetRun", "false")]
        public async Task RestoreBuildTestRun_DockerFalse_SuccessfulAsync()
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            await using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory
                    .DotnetNewAsync(
                        TemplateName,
                        "GraphQLDockerFalse",
                        DefaultArguments.ToArguments(new string[] { "docker=false" }))
                    .ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project.DotnetTestAsync().ConfigureAwait(false);
                await project.DotnetToolRestoreAsync().ConfigureAwait(false);
                await project.DotnetCakeAsync(timeout: TimeSpan.FromMinutes(3)).ConfigureAwait(false);

                var files = new DirectoryInfo(project.DirectoryPath).GetFiles("*.*", SearchOption.AllDirectories);

                Assert.DoesNotContain(files, x => x.Name == ".dockerignore");
                Assert.DoesNotContain(files, x => x.Name == "Dockerfile");

                var cake = await File.ReadAllTextAsync(files.Single(x => x.Name == "build.cake").FullName).ConfigureAwait(false);

                Assert.DoesNotContain(cake, "Docker", StringComparison.Ordinal);
            }
        }

        private static Task InstallTemplateAsync() => DotnetNew.InstallAsync<GraphQLTemplateTest>(SolutionFileName);
    }
}
