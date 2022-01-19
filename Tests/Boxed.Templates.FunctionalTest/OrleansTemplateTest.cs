namespace Boxed.Templates.FunctionalTest;

using System;
using System.Threading.Tasks;
using Boxed.DotnetNewTest;
using Xunit;
using Xunit.Abstractions;

[Trait("Template", "Orleans")]
public class OrleansTemplateTest
{
    private const string TemplateName = "orleans";
    private const string SolutionFileName = "OrleansTemplate.sln";
    private static readonly string[] DefaultArguments = new string[]
    {
        "no-install-azurite-emulator=true",
        "no-start-azurite-emulator=true",
        "http-port={HTTP_PORT}",
    };

    public OrleansTemplateTest(ITestOutputHelper testOutputHelper)
    {
        ArgumentNullException.ThrowIfNull(testOutputHelper);

        TestLogger.WriteMessage = testOutputHelper.WriteLine;
    }

    [Theory]
    [Trait("IsUsingDocker", "false")]
    [Trait("IsUsingDotnetRun", "false")]
    [InlineData("OrleansNoSerilog", "logging=None")]
    [InlineData("OrleansOpenTelemetry", "open-telemetry=true")]
    [InlineData("OrleansGitHubContainerRegistry", "docker-registry=GitHubContainerRegistry")]
    [InlineData("OrleansDockerHub", "docker-registry=DockerHub")]
    public async Task RestoreBuild_OrleansDefaults_SuccessfulAsync(string name, params string[] arguments)
    {
        await InstallTemplateAsync().ConfigureAwait(false);
        await using (var tempDirectory = TempDirectory.NewTempDirectory())
        {
            var project = await tempDirectory
                .DotnetNewAsync(TemplateName, name, DefaultArguments.ToArguments(arguments))
                .ConfigureAwait(false);
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
        await using (var tempDirectory = TempDirectory.NewTempDirectory())
        {
            var project = await tempDirectory
                .DotnetNewAsync(TemplateName, name, DefaultArguments.ToArguments(arguments))
                .ConfigureAwait(false);
            await project.DotnetToolRestoreAsync().ConfigureAwait(false);
            await project.DotnetCakeAsync(timeout: TimeSpan.FromMinutes(10)).ConfigureAwait(false);
        }
    }

    private static Task InstallTemplateAsync() => DotnetNew.InstallAsync<OrleansTemplateTest>(SolutionFileName);
}
