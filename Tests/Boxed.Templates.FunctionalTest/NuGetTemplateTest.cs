namespace Boxed.Templates.FunctionalTest
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Boxed.DotnetNewTest;
    using Xunit;
    using Xunit.Abstractions;

    public class NuGetTemplateTest
    {
        private const string TemplateName = "nuget";
        private const string SolutionFileName = "NuGetTemplate.sln";

        public NuGetTemplateTest(ITestOutputHelper testOutputHelper)
        {
            if (testOutputHelper is null)
            {
                throw new ArgumentNullException(nameof(testOutputHelper));
            }

            TestLogger.WriteMessage = testOutputHelper.WriteLine;
        }

        [Theory]
        [Trait("IsUsingDotnetRun", "false")]
        [InlineData("NuGetDefaults")]
        [InlineData("NuGetStyleCop", "style-cop=true")]
        [InlineData("NuGetNoDotnetCore", "dotnet-core=false")]
        [InlineData("NuGetNoDotnetFramework", "dotnet-framework=false")]
        public async Task RestoreAndBuild_NuGetDefaults_SuccessfulAsync(string name, params string[] arguments)
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

        [Theory(Skip = "#if pre-processor statements don't work in .cake files. See https://github.com/dotnet/templating/issues/2243")]
        [InlineData("NuGetNoAppVeyor", "appveyor.yml", "AppVeyor", "appveyor=false")]
        [InlineData("NuGetNoAzurePipelines", "azure-pipelines.yml", "AzurePipelines", "azure-pipelines=false")]
        [InlineData("NuGetNoGitHubActions", @"/.github/workflows/build.yml", "GitHubActions", "github-actions=false")]
        public async Task RestoreAndBuild_NoContinuousIntegration_SuccessfulAsync(
            string name,
            string relativeFilePath,
            string cakeContent,
            params string[] arguments)
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

                Assert.False(File.Exists(Path.Combine(project.DirectoryPath, relativeFilePath)));
                var cake = await File.ReadAllTextAsync(Path.Combine(project.DirectoryPath, "build.cake")).ConfigureAwait(false);
                Assert.DoesNotContain(cakeContent, cake, StringComparison.Ordinal);
            }
        }

        private static Task InstallTemplateAsync() => DotnetNew.InstallAsync<NuGetTemplateTest>(SolutionFileName);
    }
}
