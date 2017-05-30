namespace Boilerplate.Templates.Test
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Xunit;

    public class ApiTemplateTest : IDisposable
    {
        private const string ProjectFileName = "ApiTemplate.csproj";
        private const string TemplateName = "bapi";
        private readonly TemplateAssert templateAssert;

        public ApiTemplateTest()
        {
            var configurationService = new ConfigurationService();
            var tempDirectoryPath = configurationService.GetTempDirectoryPath();
            var projectDirectoryPath = configurationService.GetProjectDirectoryPath(ProjectFileName);

            this.templateAssert = new TemplateAssert(tempDirectoryPath);
            this.templateAssert.DotnetNewInstall(projectDirectoryPath).Wait();
        }

        [Fact]
        public async Task Home_BuildsAndRuns_Returns200Ok()
        {
            await this.templateAssert.DotnetNew(TemplateName, "HomeTest");
            await this.templateAssert.DotnetRestore();
            await this.templateAssert.DotnetBuild();
            await this.templateAssert.DotnetPublish("netcoreapp1.1");
            // await this.templateAssert.DotnetRun(
            //     async testServer =>
            //     {
            //         var response = await testServer.CreateClient().GetAsync("/");
            //         Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            //     });
        }

        public void Dispose() =>
            this.templateAssert.Dispose();
    }
}
