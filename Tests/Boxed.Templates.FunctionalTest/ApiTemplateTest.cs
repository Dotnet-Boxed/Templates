namespace Boxed.Templates.FunctionalTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using ApiTemplate.ViewModels;
    using Boxed.AspNetCore;
    using Boxed.DotnetNewTest;
    using Xunit;
    using Xunit.Abstractions;

    [Trait("Template", "API")]
    public class ApiTemplateTest
    {
        private const string TemplateName = "api";
        private const string SolutionFileName = "ApiTemplate.sln";

        public ApiTemplateTest(ITestOutputHelper testOutputHelper)
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
        [InlineData("ApiDefaults")]
        [InlineData("ApiNoForwardedHeaders", "forwarded-headers=false")]
        [InlineData("ApiNoHostFiltering", "host-filtering=false")]
        [InlineData("ApiNoFwdHdrsOrHostFilter", "forwarded-headers=false", "host-filtering=false")]
        [InlineData("ApiStyleCop", "style-cop=true")]
        public async Task RestoreBuildTest_ApiDefaults_SuccessfulAsync(string name, params string[] arguments)
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var dictionary = arguments
                    .Select(x => x.Split('=', StringSplitOptions.RemoveEmptyEntries))
                    .ToDictionary(x => x.First(), x => x.Last());
                var project = await tempDirectory.DotnetNewAsync(TemplateName, name, dictionary).ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project.DotnetTestAsync().ConfigureAwait(false);
            }
        }

        [Theory]
        [Trait("IsUsingDocker", "true")]
        [Trait("IsUsingDotnetRun", "false")]
        [InlineData("ApiDefaults")]
        public async Task Cake_ApiDefaults_SuccessfulAsync(string name, params string[] arguments)
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var dictionary = arguments
                    .Select(x => x.Split('=', StringSplitOptions.RemoveEmptyEntries))
                    .ToDictionary(x => x.First(), x => x.Last());
                var project = await tempDirectory.DotnetNewAsync(TemplateName, name, dictionary).ConfigureAwait(false);
                await project.DotnetToolRestoreAsync().ConfigureAwait(false);
                await project.DotnetCakeAsync(timeout: TimeSpan.FromMinutes(5)).ConfigureAwait(false);
            }
        }

        [Fact]
        [Trait("IsUsingDocker", "false")]
        [Trait("IsUsingDotnetRun", "true")]
        public async Task RestoreBuildTestRun_ApiDefaults_SuccessfulAsync()
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory.DotnetNewAsync(TemplateName, "ApiDefaults").ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project.DotnetTestAsync().ConfigureAwait(false);
                await project
                    .DotnetRunAsync(
                        @"Source\ApiDefaults",
                        ReadinessCheck.StatusSelfAsync,
                        async (httpClient, httpsClient) =>
                        {
                            var httpResponse = await httpClient
                                .GetAsync(new Uri("status", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

                            var statusResponse = await httpsClient
                                .GetAsync(new Uri("status", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.OK, statusResponse.StatusCode);
                            Assert.Equal(ContentType.Text, statusResponse.Content.Headers.ContentType.MediaType);

                            var statusSelfResponse = await httpsClient
                                .GetAsync(new Uri("status/self", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.OK, statusSelfResponse.StatusCode);
                            Assert.Equal(ContentType.Text, statusSelfResponse.Content.Headers.ContentType.MediaType);

                            var carsResponse = await httpsClient
                                .GetAsync(new Uri("cars", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.OK, carsResponse.StatusCode);
                            Assert.Equal(ContentType.RestfulJson, carsResponse.Content.Headers.ContentType.MediaType);

                            var postCarResponse = await httpsClient
                                .PostAsJsonAsync(new Uri("cars", UriKind.Relative), new SaveCar())
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.BadRequest, postCarResponse.StatusCode);
                            Assert.Equal(ContentType.ProblemJson, postCarResponse.Content.Headers.ContentType.MediaType);

                            var notAcceptableCarsRequest = new HttpRequestMessage(
                                HttpMethod.Get,
                                new Uri("cars", UriKind.Relative));
                            notAcceptableCarsRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.Text));
                            var notAcceptableCarsResponse = await httpsClient
                                .SendAsync(notAcceptableCarsRequest)
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.NotAcceptable, notAcceptableCarsResponse.StatusCode);

                            var swaggerJsonResponse = await httpsClient
                                .GetAsync(new Uri("swagger/v1/swagger.json", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.OK, swaggerJsonResponse.StatusCode);
                            Assert.Equal(ContentType.Json, swaggerJsonResponse.Content.Headers.ContentType.MediaType);

                            var robotsTxtResponse = await httpsClient
                                .GetAsync(new Uri("robots.txt", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.OK, robotsTxtResponse.StatusCode);
                            Assert.Equal(ContentType.Text, robotsTxtResponse.Content.Headers.ContentType.MediaType);

                            var securityTxtResponse = await httpsClient
                                .GetAsync(new Uri(".well-known/security.txt", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.OK, securityTxtResponse.StatusCode);
                            Assert.Equal(ContentType.Text, securityTxtResponse.Content.Headers.ContentType.MediaType);

                            var humansTxtResponse = await httpsClient
                                .GetAsync(new Uri("humans.txt", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.OK, humansTxtResponse.StatusCode);
                            Assert.Equal(ContentType.Text, humansTxtResponse.Content.Headers.ContentType.MediaType);
                        },
                        timeout: TimeSpan.FromMinutes(2))
                    .ConfigureAwait(false);
            }
        }

        [Fact]
        [Trait("IsUsingDocker", "false")]
        [Trait("IsUsingDotnetRun", "true")]
        public async Task RestoreBuildTestRun_HealthCheckFalse_SuccessfulAsync()
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory
                    .DotnetNewAsync(
                        TemplateName,
                        "ApiHealthCheckFalse",
                        new Dictionary<string, string>()
                        {
                            { "health-check", "false" },
                        })
                    .ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project.DotnetTestAsync().ConfigureAwait(false);
                await project
                    .DotnetRunAsync(
                        @"Source\ApiHealthCheckFalse",
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
        public async Task RestoreBuildTestRun_HttpsEverywhereFalse_SuccessfulAsync()
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory
                    .DotnetNewAsync(
                        TemplateName,
                        "ApiHttpsEverywhereFalse",
                        new Dictionary<string, string>()
                        {
                            { "https-everywhere", "false" },
                        })
                    .ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project.DotnetTestAsync().ConfigureAwait(false);
                await project
                    .DotnetRunAsync(
                        @"Source\ApiHttpsEverywhereFalse",
                        ReadinessCheck.StatusSelfOverHttpAsync,
                        async (httpClient, httpsClient) =>
                        {
                            var statusResponse = await httpClient
                                .GetAsync(new Uri("status", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.OK, statusResponse.StatusCode);
                        })
                    .ConfigureAwait(false);

                var files = new DirectoryInfo(project.DirectoryPath).GetFiles("*.*", SearchOption.AllDirectories);

                var dockerfileInfo = files.First(x => x.Name == "Dockerfile");
                var dockerfile = File.ReadAllText(dockerfileInfo.FullName);
                Assert.DoesNotContain("443", dockerfile, StringComparison.Ordinal);
            }
        }

        [Fact]
        [Trait("IsUsingDocker", "false")]
        [Trait("IsUsingDotnetRun", "true")]
        public async Task RestoreBuildTestRun_SwaggerFalse_SuccessfulAsync()
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory
                    .DotnetNewAsync(
                        TemplateName,
                        "ApiSwaggerFalse",
                        new Dictionary<string, string>()
                        {
                            { "swagger", "false" },
                        })
                    .ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project.DotnetTestAsync().ConfigureAwait(false);
                await project
                    .DotnetRunAsync(
                        @"Source\ApiSwaggerFalse",
                        ReadinessCheck.StatusSelfAsync,
                        async (httpClient, httpsClient) =>
                        {
                            var swaggerJsonResponse = await httpsClient
                                .GetAsync(new Uri("swagger/v1/swagger.json", UriKind.Relative))
                                .ConfigureAwait(false);
                            Assert.Equal(HttpStatusCode.NotFound, swaggerJsonResponse.StatusCode);
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
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory
                    .DotnetNewAsync(
                        TemplateName,
                        "ApiDockerFalse",
                        new Dictionary<string, string>()
                        {
                            { "docker", "false" },
                        })
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

        private static Task InstallTemplateAsync() => DotnetNew.InstallAsync<ApiTemplateTest>(SolutionFileName);
    }
}
