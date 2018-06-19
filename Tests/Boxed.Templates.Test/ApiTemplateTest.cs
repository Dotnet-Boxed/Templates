namespace Boxed.Templates.Test
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Xunit;

    public class ApiTemplateTest
    {
        public ApiTemplateTest() =>
            TemplateAssert.DotnetNewInstall<ApiTemplateTest>("ApiTemplate.csproj").Wait();

        [Fact]
        public async Task RestoreAndBuild_Default_Successful()
        {
            using (var tempDirectory = TemplateAssert.GetTempDirectory())
            {
                var project = await tempDirectory.DotnetNew("api", "RestoreAndBuild");
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
                await project.DotnetRun(
                    async (httpClient, httpsClient) =>
                    {
                        var httpResponse = await httpClient.GetAsync("status");
                        Assert.Equal(HttpStatusCode.NoContent, httpResponse.StatusCode);

                        var httpsResponse = await httpsClient.GetAsync("status");
                        Assert.Equal(HttpStatusCode.NoContent, httpsResponse.StatusCode);

                        var swaggerJsonResponse = await httpsClient.GetAsync("swagger/v1/swagger.json");
                        Assert.Equal(HttpStatusCode.OK, swaggerJsonResponse.StatusCode);
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
                await project.DotnetRun(
                    async httpClient =>
                    {
                        var httpResponse = await httpClient.GetAsync("status");
                        Assert.Equal(HttpStatusCode.NoContent, httpResponse.StatusCode);
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
