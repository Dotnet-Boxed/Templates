namespace Boxed.Templates.FunctionalTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Xunit;

    public class ApiTemplateTest
    {
        public ApiTemplateTest() =>
            TemplateAssert.DotnetNewInstall<ApiTemplateTest>("ApiTemplate.csproj").Wait();

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
                var project = await tempDirectory.DotnetNew("api", name, dictionary);
                await project.DotnetRestore();
                await project.DotnetBuild();
            }
        }

        [Fact]
        public async Task Run_Default_Successful()
        {
            using (var tempDirectory = TemplateAssert.GetTempDirectory())
            {
                var project = await tempDirectory.DotnetNew("api", "Default");
                await project.DotnetRestore();
                await project.DotnetBuild();
                await project.DotnetRun(
                    async (httpClient, httpsClient) =>
                    {
                        var httpResponse = await httpClient.GetAsync("status");
                        Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

                        var statusResponse = await httpsClient.GetAsync("status");
                        Assert.Equal(HttpStatusCode.OK, statusResponse.StatusCode);

                        var statusSelfResponse = await httpsClient.GetAsync("status/self");
                        Assert.Equal(HttpStatusCode.OK, statusSelfResponse.StatusCode);

                        var carsResponse = await httpsClient.GetAsync("/cars");
                        Assert.Equal(HttpStatusCode.OK, carsResponse.StatusCode);

                        var swaggerJsonResponse = await httpsClient.GetAsync("swagger/v1/swagger.json");
                        Assert.Equal(HttpStatusCode.OK, swaggerJsonResponse.StatusCode);

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
                    "api",
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
                        var statusResponse = await httpClient.GetAsync("status");
                        Assert.Equal(HttpStatusCode.NotFound, statusResponse.StatusCode);

                        var statusSelfResponse = await httpClient.GetAsync("status/self");
                        Assert.Equal(HttpStatusCode.NotFound, statusSelfResponse.StatusCode);
                    });
            }
        }

        [Fact]
        public async Task Run_HttpsEverywhereFalse_Successful()
        {
            using (var tempDirectory = TemplateAssert.GetTempDirectory())
            {
                var project = await tempDirectory.DotnetNew(
                    "api",
                    "HttpsEverywhereFalse",
                    new Dictionary<string, string>()
                    {
                        { "https-everywhere", "false" },
                    });
                await project.DotnetRestore();
                await project.DotnetBuild();
                await project.DotnetRun(
                    async httpClient =>
                    {
                        var statusResponse = await httpClient.GetAsync("status");
                        Assert.Equal(HttpStatusCode.OK, statusResponse.StatusCode);
                    });
            }
        }

        [Fact]
        public async Task Run_SwaggerFalse_Successful()
        {
            using (var tempDirectory = TemplateAssert.GetTempDirectory())
            {
                var project = await tempDirectory.DotnetNew(
                    "api",
                    "SwaggerFalse",
                    new Dictionary<string, string>()
                    {
                        { "swagger", "false" },
                    });
                await project.DotnetRestore();
                await project.DotnetBuild();
                await project.DotnetRun(
                    async (httpClient, httpsClient) =>
                    {
                        var swaggerJsonResponse = await httpsClient.GetAsync("swagger/v1/swagger.json");
                        Assert.Equal(HttpStatusCode.NotFound, swaggerJsonResponse.StatusCode);
                    });
            }
        }
    }
}
