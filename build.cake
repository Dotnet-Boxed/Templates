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

var artifactsDirectory = Directory("./Artifacts");
var templatePackProject = Directory("./Source/*.csproj");
var versionSuffix = string.IsNullOrEmpty(preReleaseSuffix) ? null : preReleaseSuffix + "-" + buildNumber.ToString("D4");
var isRunningOnCI = TFBuild.IsRunningOnAzurePipelinesHosted || AppVeyor.IsRunningOnAppVeyor;
var isDotnetRunEnabled = !isRunningOnCI || (isRunningOnCI && IsRunningOnWindows());

Task("Clean")
    .Does(() =>
    {
        CleanDirectory(artifactsDirectory);
        DeleteDirectories(GetDirectories("**/bin"), new DeleteDirectorySettings() { Force = true, Recursive = true });
        DeleteDirectories(GetDirectories("**/obj"), new DeleteDirectorySettings() { Force = true, Recursive = true });
    });

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        DotNetCoreRestore();
    });

 Task("Build")
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
    .Does(() =>
    {
        if (isDotnetRunEnabled)
        {
            var certificateFilePath = System.IO.Path.ChangeExtension(System.IO.Path.GetTempFileName(), ".pfx");
            StartProcess(
                "dotnet",
                new ProcessArgumentBuilder()
                    .Append("dev-certs")
                    .Append("https")
                    .AppendSwitch("--export-path", certificateFilePath));
            Information($"Dotnet Developer Certificate saved");

            var certificate = new X509Certificate2(certificateFilePath);
            using (var store = new X509Store(StoreName.Root, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadWrite);
                store.Add(certificate);
            }
            Information($"Dotnet Developer Certificate installed to local machine");

            System.IO.File.Delete(certificateFilePath);
        }
        else
        {
            Information("This CI server does not support installing certificates");
        }
    });

Task("Test")
    .Does(() =>
    {
        foreach(var project in GetFiles("./Tests/**/*.csproj"))
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
                    ResultsDirectory = artifactsDirectory
                });
            Information($"Completed {project.GetFilenameWithoutExtension()} tests");
        }
    });

Task("Pack")
    .Does(() =>
    {
        DotNetCorePack(
            GetFiles(templatePackProject).Single().ToString(),
            new DotNetCorePackSettings()
            {
                Configuration = configuration,
                NoBuild = true,
                NoRestore = true,
                OutputDirectory = artifactsDirectory,
                VersionSuffix = versionSuffix,
            });
    });

Task("Default")
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
