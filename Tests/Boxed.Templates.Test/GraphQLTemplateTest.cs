namespace Boxed.Templates.Test
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Xunit;

    public class GraphQLTemplateTest
    {
        public GraphQLTemplateTest() =>
            TemplateAssert.DotnetNewInstall<GraphQLTemplateTest>("GraphQLTemplate.csproj").Wait();

        [Fact]
        public async Task RestoreAndBuild_Default_Successful()
        {
            using (var tempDirectory = TemplateAssert.GetTempDirectory())
            {
                var project = await tempDirectory.DotnetNew("graphql", "RestoreAndBuild");
                await project.DotnetRestore();
                await project.DotnetBuild();
            }
        }

        [Fact]
        public async Task Run_Default_Successful()
        {
            using (var tempDirectory = TemplateAssert.GetTempDirectory())
            {
                var project = await tempDirectory.DotnetNew("graphql", "Default");
                await project.DotnetRun(
                    async (httpClient, httpsClient) =>
                    {
                        var httpResponse = await httpClient.GetAsync("/");
                        Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

                        var httpsResponse = await httpsClient.GetAsync("/");
                        Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
                    });
            }
        }

        [Fact]
        public async Task Run_HttpsEverywhereFalse_Successful()
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
                await project.DotnetRun(
                    async (httpClient) =>
                    {
                        var httpResponse = await httpClient.GetAsync("/");
                        Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
                    });
            }
        }
    }
}
