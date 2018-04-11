namespace Boilerplate.Templates.Test
{
    using System.Net.Http;
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
                        await Assert.ThrowsAsync<HttpRequestException>(
                            () => this.HttpClient.GetAsync("http://localhost:5000/status"));

                        var response = await this.HttpClient.GetAsync("https://localhost:44300/status");
                        response.EnsureSuccessStatusCode();
                    });
            }
        }
    }
}
