using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

var target = Argument("Target", "Default");
var configuration =
    HasArgument("Configuration") ? Argument<string>("Configuration") :
    EnvironmentVariable("Configuration") != null ? EnvironmentVariable("Configuration") :
    "Release";
var preReleaseSuffix =
    HasArgument("PreReleaseSuffix") ? Argument<string>("PreReleaseSuffix") :
    (TFBuild.IsRunningOnAzurePipelinesHosted && TFBuild.Environment.Repository.SourceBranch.StartsWith("refs/tags/")) ? null :
    (AppVeyor.IsRunningOnAppVeyor && AppVeyor.Environment.Repository.Tag.IsTag) ? null :
    EnvironmentVariable("PreReleaseSuffix") != null ? EnvironmentVariable("PreReleaseSuffix") :
    "beta";
var buildNumber =
    HasArgument("BuildNumber") ? Argument<int>("BuildNumber") :
    TFBuild.IsRunningOnAzurePipelinesHosted ? TFBuild.Environment.Build.Id :
    AppVeyor.IsRunningOnAppVeyor ? AppVeyor.Environment.Build.Number :
    EnvironmentVariable("BuildNumber") != null ? int.Parse(EnvironmentVariable("BuildNumber")) :
    0;

var artefactsDirectory = Directory("./Artefacts");
var templatePackProject = Directory("./Source/*.csproj");
var versionSuffix = string.IsNullOrEmpty(preReleaseSuffix) ? null : preReleaseSuffix + "-" + buildNumber.ToString("D4");
var isRunningOnCI = TFBuild.IsRunningOnAzurePipelinesHosted ||
    AppVeyor.IsRunningOnAppVeyor ||
    Environment.GetEnvironmentVariable("GITHUB_ACTIONS") != null;
var isDotnetRunEnabled = !isRunningOnCI || (isRunningOnCI && IsRunningOnWindows());

Task("Clean")
    .Description("Cleans the artefacts, bin and obj directories.")
    .Does(() =>
    {
        CleanDirectory(artefactsDirectory);
        DeleteDirectories(GetDirectories("**/bin"), new DeleteDirectorySettings() { Force = true, Recursive = true });
        DeleteDirectories(GetDirectories("**/obj"), new DeleteDirectorySettings() { Force = true, Recursive = true });
    });

Task("Restore")
    .Description("Restores NuGet packages.")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        DotNetCoreRestore();
    });

 Task("Build")
    .Description("Builds the solution.")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        DotNetCoreBuild(
            ".",
            new DotNetCoreBuildSettings()
            {
                Configuration = configuration,
                NoRestore = true
            });
    });

Task("InstallDeveloperCertificate")
    .Description("Installs a developer certificate using the dotnet dev-certs tool.")
    .Does(() =>
    {
        if (isDotnetRunEnabled)
        {
            var certificateFilePath = System.IO.Path.ChangeExtension(System.IO.Path.GetTempFileName(), ".pfx");
            try
            {
                StartProcess(
                    "dotnet",
                    new ProcessArgumentBuilder()
                        .Append("dev-certs")
                        .Append("https")
                        .AppendSwitch("--export-path", certificateFilePath));

                var certificate = new X509Certificate2(certificateFilePath);
                using (var store = new X509Store(StoreName.Root, StoreLocation.LocalMachine))
                {
                    store.Open(OpenFlags.ReadWrite);
                    store.Add(certificate);
                }
                Information($"Dotnet developer certificate installed to local machine's root certificates.");
            }
            finally
            {
                if (System.IO.File.Exists(certificateFilePath))
                {
                    System.IO.File.Delete(certificateFilePath);
                }
            }
        }
        else
        {
            Information("This CI server does not support installing certificates");
        }
    });

Task("Test")
    .Description("Runs unit tests and outputs test results to the artefacts directory.")
    .DoesForEach(GetFiles("./Tests/**/*.csproj"), project =>
    {
        DotNetCoreTest(
            project.ToString(),
            new DotNetCoreTestSettings()
            {
                Configuration = configuration,
                Filter = isDotnetRunEnabled ? null : "IsUsingDotnetRun=false",
                Logger = $"trx;LogFileName={project.GetFilenameWithoutExtension()}.trx",
                NoBuild = true,
                NoRestore = true,
                ResultsDirectory = artefactsDirectory,
                ArgumentCustomization = x => x.Append($"--logger html;LogFileName={project.GetFilenameWithoutExtension()}.html"),
            });
    });

Task("Pack")
    .Description("Creates NuGet packages and outputs them to the artefacts directory.")
    .Does(() =>
    {
        DotNetCorePack(
            GetFiles(templatePackProject).Single().ToString(),
            new DotNetCorePackSettings()
            {
                Configuration = configuration,
                NoBuild = true,
                NoRestore = true,
                OutputDirectory = artefactsDirectory,
                VersionSuffix = versionSuffix,
            });
    });

Task("Default")
    .Description("Cleans, restores NuGet packages, builds the solution, runs unit tests and then creates NuGet packages.")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("Pack");

RunTarget(target);

public void StartProcess(string processName, ProcessArgumentBuilder builder)
{
    var command = $"{processName} {builder.RenderSafe()}";
    Information($"Executing: {command}");
    var exitCode = StartProcess(
        processName,
        new ProcessSettings()
        {
            Arguments = builder
        });
    if (exitCode != 0 && !TFBuild.IsRunningOnAzurePipelinesHosted)
    {
        throw new Exception($"'{command}' failed with exit code {exitCode}.");
    }
}
