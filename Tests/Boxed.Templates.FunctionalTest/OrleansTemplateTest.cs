namespace Boxed.Templates.FunctionalTest
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Boxed.DotnetNewTest;
    using Xunit;
    using Xunit.Abstractions;

    [Trait("Template", "Orleans")]
    public class OrleansTemplateTest
    {
        private const string TemplateName = "orleans";
        private const string SolutionFileName = "OrleansTemplate.sln";

        public OrleansTemplateTest(ITestOutputHelper testOutputHelper)
        {
            if (testOutputHelper is null)
            {
                throw new ArgumentNullException(nameof(testOutputHelper));
            }

            TestLogger.WriteMessage = testOutputHelper.WriteLine;
        }

        [Theory]
        [Trait("IsUsingDocker", "false")]
        [Trait("IsUsingDotnetRun", "false")]
        [InlineData("OrleansDefaults")]
        [InlineData("OrleansStyleCop", "style-cop=true")]
        [InlineData("OrleansNoHealthCheck", "health-check=false")]
        public async Task RestoreBuild_OrleansDefaults_SuccessfulAsync(string name, params string[] arguments)
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var dictionary = arguments
                    .Select(x => x.Split('=', StringSplitOptions.RemoveEmptyEntries))
                    .ToDictionary(x => x.First(), x => x.Last());
                var project = await tempDirectory.DotnetNewAsync(TemplateName, name, dictionary).ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
            }
        }

        [Theory]
        [Trait("IsUsingDocker", "true")]
        [Trait("IsUsingDotnetRun", "false")]
        [InlineData("OrleansDefaults")]
        [InlineData("OrleansNoDocker", "docker=false")]
        public async Task Cake_ApiDefaults_SuccessfulAsync(string name, params string[] arguments)
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var dictionary = arguments
                    .Select(x => x.Split('=', StringSplitOptions.RemoveEmptyEntries))
                    .ToDictionary(x => x.First(), x => x.Last());
                var project = await tempDirectory.DotnetNewAsync(TemplateName, name, dictionary).ConfigureAwait(false);
                await project.DotnetToolRestoreAsync().ConfigureAwait(false);
                await project.DotnetCakeAsync(timeout: TimeSpan.FromMinutes(5)).ConfigureAwait(false);
            }
        }

        private static Task InstallTemplateAsync() => DotnetNew.InstallAsync<OrleansTemplateTest>(SolutionFileName);
    }
}
