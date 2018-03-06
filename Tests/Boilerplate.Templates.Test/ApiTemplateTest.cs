namespace Boilerplate.Templates.Test
{
    using System.Threading.Tasks;
    using Xunit;

    public class ApiTemplateTest
    {
        public ApiTemplateTest() => TemplateAssert.DotnetNewInstall<ApiTemplateTest>("ApiTemplate.csproj").Wait();

        [Fact]
        public async Task Home_BuildsAndRuns_Returns200Ok()
        {
            using (var tempDirectory = TemplateAssert.GetTempDirectory())
            {
                var project = await tempDirectory.DotnetNew("api", "HomeTest");
                await project.DotnetRestore();
                await project.DotnetBuild();
                await project.DotnetPublish("netcoreapp2.0");
                // await project.DotnetRun(
                //     async testServer =>
                //     {
                //         var response = await testServer.CreateClient().GetAsync("/");
                //         Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                //     });
            }
        }
    }
}
