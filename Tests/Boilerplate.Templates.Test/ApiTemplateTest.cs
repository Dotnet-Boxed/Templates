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
            TemplateAssert.TempDirectoryPath = ConfigurationService.GetTempDirectoryPath();

            var projectDirectoryPath = TemplateAssert.GetProjectDirectoryPath(
                typeof(ApiTemplateTest).GetTypeInfo().Assembly,
                ProjectFileName);
            TemplateAssert.DotnetNewInstall(projectDirectoryPath).Wait();
        }

        [Fact]
        public async Task Home_BuildsAndRuns_Returns200Ok()
        {
            using (var project = await TemplateAssert.DotnetNew("api", "HomeTest"))
            {
                await TemplateAssert.DotnetRestore(project.DirectoryPath);
                await TemplateAssert.DotnetBuild(project.DirectoryPath);
                await TemplateAssert.DotnetPublish(project.DirectoryPath, "netcoreapp1.1");
                await TemplateAssert.DotnetRun(
                    project.DirectoryPath,
                    async testServer =>
                    {
                        var response = await testServer.CreateClient().GetAsync("/");
                        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                    });
            }
        }
    }
}
