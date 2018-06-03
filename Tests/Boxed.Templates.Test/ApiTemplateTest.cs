namespace Boxed.Templates.Test
{
    using System.Collections.Generic;
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
                        var httpResponse = await this.HttpClient.GetAsync("http://localhost:5000/status");
                        httpResponse.EnsureSuccessStatusCode();
                        var httpsResponse = await this.HttpClient.GetAsync("https://localhost:5001/status");
                        httpsResponse.EnsureSuccessStatusCode();
                    });
            }
        }

        [Fact]
        public async Task Build_HttpsEverywhereFalse_Successful()
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
                    async () =>
                    {
                        var httpResponse = await this.HttpClient.GetAsync("http://localhost:5000/status");
                        httpResponse.EnsureSuccessStatusCode();
                        var httpsResponse = await this.HttpClient.GetAsync("https://localhost:5001/status");
                        httpsResponse.EnsureSuccessStatusCode();
                    });
            }
        }
    }
}
