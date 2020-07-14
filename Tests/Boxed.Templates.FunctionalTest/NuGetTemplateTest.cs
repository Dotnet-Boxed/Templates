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

    [Trait("Template", "NuGet")]
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
        [Trait("IsUsingDocker", "false")]
        [Trait("IsUsingDotnetRun", "false")]
        [InlineData("NuGetDefaults")]
        [InlineData("NuGetStyleCop", "style-cop=true")]
        [InlineData("NuGetDotnetCoreAndFramework", "dotnet-framework=true")]
        [InlineData("NuGetDotnetFramework", "dotnet-core=false", "dotnet-framework=true")]
        public async Task RestoreBuildTest_NuGetDefaults_SuccessfulAsync(string name, params string[] arguments)
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

                if (!arguments.Contains("dotnet-framework=true") ||
                    (arguments.Contains("dotnet-framework=true") && Environment.OSVersion.Platform == PlatformID.Win32NT))
                {
                    // There seems to be a bug that stops xUnit working on Mono.
                    await project.DotnetTestAsync().ConfigureAwait(false);
                }
            }
        }

        [Theory]
        [Trait("IsUsingDocker", "false")]
        [Trait("IsUsingDotnetRun", "false")]
        [InlineData("NuGetDefaults")]
        public async Task Cake_NuGetDefaults_SuccessfulAsync(string name, params string[] arguments)
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var dictionary = arguments
                    .Select(x => x.Split('=', StringSplitOptions.RemoveEmptyEntries))
                    .ToDictionary(x => x.First(), x => x.Last());
                var project = await tempDirectory.DotnetNewAsync(TemplateName, name, dictionary).ConfigureAwait(false);
                await project.DotnetToolRestoreAsync().ConfigureAwait(false);
                await project.DotnetCakeAsync().ConfigureAwait(false);
            }
        }

        [Theory]
        [Trait("IsUsingDocker", "false")]
        [Trait("IsUsingDotnetRun", "false")]
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
        [Trait("IsUsingDocker", "false")]
        [Trait("IsUsingDotnetRun", "false")]
        public async Task RestoreBuildTest_SignFalse_SuccessfulAsync()
        {
            await InstallTemplateAsync().ConfigureAwait(false);
            using (var tempDirectory = TempDirectory.NewTempDirectory())
            {
                var project = await tempDirectory
                    .DotnetNewAsync(
                        TemplateName,
                        "NuGetSignFalse",
                        new Dictionary<string, string>()
                        {
                            { "sign", "false" },
                        })
                    .ConfigureAwait(false);
                await project.DotnetRestoreAsync().ConfigureAwait(false);
                await project.DotnetBuildAsync().ConfigureAwait(false);
                await project.DotnetTestAsync().ConfigureAwait(false);

                var files = new DirectoryInfo(project.DirectoryPath).GetFiles("*.*", SearchOption.AllDirectories);

                var csprojFile = files.Single(x => x.Name == "NuGetSignFalse.csproj");
                var csproj = File.ReadAllText(csprojFile.FullName);
                Assert.DoesNotContain("Sign", csproj, StringComparison.Ordinal);
            }
        }

        private static Task InstallTemplateAsync() => DotnetNew.InstallAsync<NuGetTemplateTest>(SolutionFileName);
    }
}
