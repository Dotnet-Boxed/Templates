using System.IO;
using System.Security.Cryptography.X509Certificates;
using Nuke.Common;
using Nuke.Common.CI.AppVeyor;
using Nuke.Common.CI.AzurePipelines;
using Nuke.Common.Execution;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Logger;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter] readonly Configuration Configuration = Configuration.Release;

    [Parameter]
    readonly int BuildNumber = (int?)AzurePipelines.Instance?.BuildId ??
                               AppVeyor.Instance?.BuildNumber ??
                               0;

    [Parameter] readonly string PreReleaseSuffix = "beta";

    [Parameter] string VersionSuffix => $"{PreReleaseSuffix}-{BuildNumber}";

    [Solution] readonly Solution Solution;

    string ArtifactsDirectory => RootDirectory / "Artifacts";
    Project TemplatePackProject => Solution.GetProject(nameOrFullPath: "Boxed.Templates");


    Target Clean => _ => _
        .Description(description: "Cleans the artefacts, bin and obj directories.")
        .Before(Restore)
        .Executes(() =>
        {
            EnsureCleanDirectory(ArtifactsDirectory);
            RootDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
        });

    Target Restore => _ => _
        .Description(description: "Restores NuGet packages.")
        .Executes(() =>
        {
            DotNetRestore(_ => _
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .Description(description: "Builds the solution.")
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(_ => _
                .SetConfiguration(Configuration)
                .SetNoRestore(ExecutingTargets.Contains(Restore)));
        });

    Target InstallDeveloperCertificate => _ => _
        .Description(description: "Installs a developer certificate using the dotnet dev-certs tool.")
        .OnlyWhenStatic(() => !IsLocalBuild || IsWin)
        .Executes(() =>
        {
            var certificateFilePath = Path.ChangeExtension(Path.GetTempFileName(), extension: ".pfx");
            DotNet($"dev-certs https --export-path {certificateFilePath}");
            Info("Dotnet Developer Certificate saved");

            var certificate = new X509Certificate2(certificateFilePath);
            using (var store = new X509Store(StoreName.Root, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadWrite);
                store.Add(certificate);
            }

            Info("Dotnet Developer Certificate installed to local machine");

            File.Delete(certificateFilePath);
        });

    Target Test => _ => _
        .Description("Runs unit tests and outputs test results to the artefacts directory.")
        .Executes(() =>
        {
            DotNetTest(_ => _
                .SetConfiguration(Configuration)
                .SetNoBuild(ExecutingTargets.Contains(Compile))
                .SetResultsDirectory(ArtifactsDirectory)
                .When(!IsLocalBuild || IsWin, _ => _
                    .SetFilter("IsUsingDotnetRun=false"))
                .CombineWith(Solution.GetProjects("*Test"), (_, v) => _
                    .SetProjectFile(v)
                    .SetLogger($"trx;LogFileName={v.Name}.trx")));
        });

    Target Pack => _ => _
        .Description("Creates NuGet packages and outputs them to the artefacts directory.")
        .Executes(() =>
        {
            DotNetPack(_ => _
                .SetProject(TemplatePackProject)
                .SetConfiguration(Configuration)
                .SetNoBuild(ExecutingTargets.Contains(Compile))
                .SetOutputDirectory(ArtifactsDirectory)
                .SetVersionSuffix(VersionSuffix));
        });
}
