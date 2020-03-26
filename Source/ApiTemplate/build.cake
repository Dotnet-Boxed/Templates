var target = Argument("Target", "Default");
var configuration =
    HasArgument("Configuration") ? Argument<string>("Configuration") :
    EnvironmentVariable("Configuration") is object ? EnvironmentVariable("Configuration") :
    "Release";
var versionTemplate =
    HasArgument("VersionTemplate") ? Argument<string>("VersionTemplate") :
    EnvironmentVariable("VersionTemplate") is object ? EnvironmentVariable("VersionTemplate") :
    "1.0.{0:D4}";
var buildNumber =
    HasArgument("BuildNumber") ? Argument<int>("BuildNumber") :
    BuildSystem.IsRunningOnAzurePipelinesHosted ? TFBuild.Environment.Build.Id :
    BuildSystem.IsRunningOnGitHubActions ? 1 : // GitHub Actions doesn't support build numbers
    BuildSystem.IsRunningOnAppVeyor ? AppVeyor.Environment.Build.Number :
    EnvironmentVariable("BuildNumber") is object ? int.Parse(EnvironmentVariable("BuildNumber")) :
    0;

var artefactsDirectory = Directory("./Artefacts");
var version = string.Format(versionTemplate, buildNumber);

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
                MSBuildSettings = new DotNetCoreMSBuildSettings().SetVersion(version),
                NoRestore = true,
            });
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
                Logger = $"trx;LogFileName={project.GetFilenameWithoutExtension()}.trx",
                NoBuild = true,
                NoRestore = true,
                ResultsDirectory = artefactsDirectory,
                ArgumentCustomization = x => x
                    .Append("--blame")
                    .AppendSwitch("--logger", $"html;LogFileName={project.GetFilenameWithoutExtension()}.html")
                    .Append("--collect:\"XPlat Code Coverage\""),
            });
    });

Task("Publish")
    .Description("Publishes the solution.")
    .Does(() =>
    {
        Information(artefactsDirectory.GetType().Name);
        DotNetCorePublish(
            ".",
            new DotNetCorePublishSettings()
            {
                Configuration = configuration,
                MSBuildSettings = new DotNetCoreMSBuildSettings().SetVersion(version),
                NoBuild = true,
                NoRestore = true,
                OutputDirectory = artefactsDirectory + Directory("Publish"),
            });
    });

//#if (Docker)
Task("DockerBuild")
    .Description("Builds a Docker image.")
    .DoesForEach(GetFiles("./**/Dockerfile"), dockerfile =>
    {
        // Uncomment the following lines if using docker buildx.
        StartProcess(
            "docker",
            new ProcessArgumentBuilder()
                //.Append("buildx")
                .Append("build")
                //.AppendSwitch("--progress", "plain")
                .AppendSwitchQuoted("--tag", $"{dockerfile.GetDirectory().GetDirectoryName().ToLower()}:{version}")
                .AppendSwitchQuoted("--build-arg", $"Configuration={configuration}")
                .AppendSwitchQuoted("--label", $"org.opencontainers.image.created={DateTimeOffset.Now:o}")
                .AppendSwitchQuoted("--label", $"org.opencontainers.image.version={version}")
                .AppendSwitchQuoted("--file", dockerfile.ToString())
                .Append(".")
                .RenderSafe());
    });

Task("Default")
    .Description("Cleans, restores NuGet packages, builds the solution, runs unit tests and then builds a Docker image.")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("DockerBuild");
////#else
//Task("Default")
//    .Description("Cleans, restores NuGet packages, builds the solution, runs unit tests and then publishes.")
//    .IsDependentOn("Build")
//    .IsDependentOn("Test")
//    .IsDependentOn("Publish");
//#endif

RunTarget(target);
