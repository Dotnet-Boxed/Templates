namespace Boxed.Templates.FunctionalTest
{
    using System;
    using System.Collections.Generic;
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
        [InlineData("NuGetNoDotnetFramework", "dotnet-framework=true")]
        [InlineData("NuGetNoDotnetCore", "dotnet-core=false", "dotnet-framework=true")]
        public async Task RestoreBuildTestCake_NuGetDefaults_SuccessfulAsync(string name, params string[] arguments)
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

                // There is currently an issue with enabling .NET Framework and PublicSign.
                // https://github.com/dotnet/sdk/issues/10597
                // https://stackoverflow.com/questions/60058571
                if (!arguments.Contains("dotnet-framework=true"))
                {
                    await project.DotnetTestAsync().ConfigureAwait(false);
                    await project.DotnetToolRestoreAsync().ConfigureAwait(false);
                    await project.DotnetCakeAsync().ConfigureAwait(false);
                }
            }
        }

        [Theory(Skip = "#if pre-processor statements don't work in .cake files. See https://github.com/dotnet/templating/issues/2243")]
        [InlineData("NuGetNoAppVeyor", "appveyor.yml", "AppVeyor", "appveyor=false")]
        [InlineData("NuGetNoAzurePipelines", "azure-pipelines.yml", "AzurePipelines", "azure-pipelines=false")]
        [InlineData("NuGetNoGitHubActions", @"/.github/workflows/build.yml", "GitHubActions", "github-actions=false")]
        public async Task RestoreBuildTestCake_NoContinuousIntegration_SuccessfulAsync(
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
                await project.DotnetTestAsync().ConfigureAwait(false);
                await project.DotnetToolRestoreAsync().ConfigureAwait(false);
                await project.DotnetCakeAsync().ConfigureAwait(false);

                Assert.False(File.Exists(Path.Combine(project.DirectoryPath, relativeFilePath)));
                var cake = await File.ReadAllTextAsync(Path.Combine(project.DirectoryPath, "build.cake")).ConfigureAwait(false);
                Assert.DoesNotContain(cakeContent, cake, StringComparison.Ordinal);
            }
        }

        [Fact]
        [Trait("IsUsingDotnetRun", "false")]
        public async Task RestoreBuildTestCake_PublicSignFalse_SuccessfulAsync()
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory
                    .DotnetNewAsync(
                        TemplateName,
                        "NuGetPublicSignFalse",
                        new Dictionary<string, string>()
                        {
                            { "public-sign", "false" },
                        })
                    .ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project.DotnetTestAsync().ConfigureAwait(false);
                await project.DotnetToolRestoreAsync().ConfigureAwait(false);
                await project.DotnetCakeAsync().ConfigureAwait(false);

                var files = new DirectoryInfo(project.DirectoryPath).GetFiles("*.*", SearchOption.AllDirectories);

                var csprojFile = files.Single(x => x.Name == "NuGetPublicSignFalse.csproj");
                var csproj = File.ReadAllText(csprojFile.FullName);
                Assert.DoesNotContain("PublicSign", csproj, StringComparison.Ordinal);
            }
        }

        private static Task InstallTemplateAsync() => DotnetNew.InstallAsync<NuGetTemplateTest>(SolutionFileName);
    }
}
