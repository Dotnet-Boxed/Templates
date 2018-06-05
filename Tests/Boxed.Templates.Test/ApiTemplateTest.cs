namespace Boxed.Templates.Test
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class ApiTemplateTest
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
                    async (httpClient, httpsClient) =>
                    {
                        var httpResponse = await httpClient.GetAsync("status");
                        httpResponse.EnsureSuccessStatusCode();
                        var httpsResponse = await httpsClient.GetAsync("status");
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
                    async (httpClient, httpsClient) =>
                    {
                        var httpResponse = await httpClient.GetAsync("status");
                        httpResponse.EnsureSuccessStatusCode();
                        var httpsResponse = await httpsClient.GetAsync("status");
                        httpsResponse.EnsureSuccessStatusCode();
                    });
            }
        }
    }
}
