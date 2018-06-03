namespace Boxed.Templates.Test
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class GraphQLTemplateTest : HttpClientTest
    {
        public GraphQLTemplateTest() =>
            TemplateAssert.DotnetNewInstall<GraphQLTemplateTest>("GraphQLTemplate.csproj").Wait();

        [Fact]
        public async Task Build_Default_Successful()
        {
            using (var tempDirectory = TemplateAssert.GetTempDirectory())
            {
                var project = await tempDirectory.DotnetNew("graphql", "Default");
                await project.DotnetRestore();
                await project.DotnetBuild();
                await project.DotnetRun(
                    async () =>
                    {
                        var httpResponse = await this.HttpClient.GetAsync("http://localhost:5000");
                        httpResponse.EnsureSuccessStatusCode();
                        var httpsResponse = await this.HttpClient.GetAsync("https://localhost:5001");
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
                    "graphql",
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
                        var httpResponse = await this.HttpClient.GetAsync("http://localhost:5000");
                        httpResponse.EnsureSuccessStatusCode();
                        var httpsResponse = await this.HttpClient.GetAsync("https://localhost:5001");
                        httpsResponse.EnsureSuccessStatusCode();
                    });
            }
        }
    }
}
