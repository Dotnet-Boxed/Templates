var target = Argument("Target", "Default");
var configuration =
    HasArgument("Configuration") ? Argument<string>("Configuration") :
    EnvironmentVariable("Configuration") != null ? EnvironmentVariable("Configuration") :
    "Release";
var preReleaseSuffix =
    HasArgument("PreReleaseSuffix") ? Argument<string>("PreReleaseSuffix") :
    (TFBuild.IsRunningOnAzurePipelinesHosted && Environment.GetEnvironmentVariable("BUILD_SOURCEBRANCH").StartsWith("refs/tags/")) ? null :
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
var versionSuffix = string.IsNullOrEmpty(preReleaseSuffix) ? null : preReleaseSuffix + "-" + buildNumber.ToString("D4");

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
                    Logger = $"trx;LogFileName={project.GetFilenameWithoutExtension()}.trx",
                    NoBuild = true,
                    NoRestore = true,
                    ResultsDirectory = artifactsDirectory
                });
        }
    });

Task("Pack")
    .Does(() =>
    {
        DotNetCorePack(
            GetFiles("**/Boxed.Templates.csproj").First().ToString(),
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

Teardown(context =>
{
    // Appveyor is failing to exit the cake script.
    if (AppVeyor.IsRunningOnAppVeyor)
    {
        foreach (var process in Process.GetProcessesByName("dotnet"))
        {
            process.Kill();
        }
    }
});