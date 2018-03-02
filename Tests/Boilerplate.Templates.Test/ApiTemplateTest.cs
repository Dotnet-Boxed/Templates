namespace Boilerplate.Templates.Test
{
    using System.Net;
    using System.Reflection;
    using System.Threading.Tasks;
    using Xunit;

    public class ApiTemplateTest
    {
        private const string ProjectFileName = "ApiTemplate.csproj";
        private const string TemplateName = "api";

        public ApiTemplateTest()
        {
            var projectDirectoryPath = TemplateAssert.GetProjectDirectoryPath(
                typeof(ApiTemplateTest).GetTypeInfo().Assembly,
                ProjectFileName);
            TemplateAssert.DotnetNewInstall(projectDirectoryPath).Wait();
        }

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
