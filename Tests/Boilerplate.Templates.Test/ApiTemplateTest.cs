namespace Boilerplate.Templates.Test
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;

    public class ApiTemplateTest : HttpClientTest
    {
        public ApiTemplateTest() =>
            TemplateAssert.DotnetNewInstall<ApiTemplateTest>("ApiTemplate.csproj").Wait();

        [Fact]
        public async Task Build_Default_Successful()
        {
            using (var tempDirectory = TemplateAssert.GetTempDirectory())
            {
                var project = await tempDirectory.DotnetNew("api", "Default");
                await project.DotnetRestore();
                await project.DotnetBuild();
                await project.DotnetRun(
                    async () =>
                    {
                        await Assert.ThrowsAsync<HttpRequestException>(
                            () => this.HttpClient.GetAsync("http://localhost:5000/status"));

                        var response = await this.HttpClient.GetAsync("https://localhost:44300/status");
                        response.EnsureSuccessStatusCode();
                    });
            }
        }

        [Fact(Skip = "Temp")]
        public async Task Build_HttpsEverywhereFalse_Successful()
        {
            using (var tempDirectory = TemplateAssert.GetTempDirectory())
            {
                var project = await tempDirectory.DotnetNew(
                    "api",
                    "HttpsEverywhereFalse",
                    new Dictionary<string, string>()
                    {
                        { "HttpsEverywhere", "false" },
                    });
                await project.DotnetRestore();
                await project.DotnetBuild();
                await project.DotnetRun(
                    async () =>
                    {
                        var response = await this.HttpClient.GetAsync("http://localhost:5000/status");
                        response.EnsureSuccessStatusCode();

                        await Assert.ThrowsAsync<HttpRequestException>(
                            () => this.HttpClient.GetAsync("https://localhost:44300/status"));
                    });
            }
        }
    }
}
