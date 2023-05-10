var target = Argument("Target", "Default");
var configuration =
    HasArgument("Configuration") ? Argument<string>("Configuration") :
    EnvironmentVariable("Configuration", "Release");
#if (Docker)
var tag =
    HasArgument("Tag") ? Argument<string>("Tag") :
    EnvironmentVariable("Tag", (string)null);
var platform =
    HasArgument("Platform") ? Argument<string>("Platform") :
    EnvironmentVariable("Platform", "linux/amd64,linux/arm64");
var push =
    HasArgument("Push") ? Argument<bool>("Push") :
    EnvironmentVariable("Push", false);
#endif

var artefactsDirectory = Directory("./Artefacts");

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
        DotNetRestore();
    });

Task("Build")
    .Description("Builds the solution.")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        DotNetBuild(
            ".",
            new DotNetBuildSettings()
            {
                Configuration = configuration,
                NoRestore = true,
            });
    });

Task("Test")
    .Description("Runs unit tests and outputs test results to the artefacts directory.")
    .DoesForEach(
        GetFiles("./Tests/**/*.csproj"),
        project =>
        {
            DotNetTest(
                project.ToString(),
                new DotNetTestSettings()
                {
                    Blame = true,
                    Collectors = new string[] { "Code Coverage", "XPlat Code Coverage" },
                    Configuration = configuration,
                    Loggers = new string[]
                    {
                        $"trx;LogFileName={project.GetFilenameWithoutExtension()}.trx",
                        $"html;LogFileName={project.GetFilenameWithoutExtension()}.html",
                    },
                    NoBuild = true,
                    NoRestore = true,
                    ResultsDirectory = artefactsDirectory,
                });
        });

Task("Publish")
    .Description("Publishes the solution.")
    .DoesForEach(
        GetFiles("./Source/**/*.csproj"),
        project =>
        {
            DotNetPublish(
                project.ToString(),
                new DotNetPublishSettings()
                {
                    Configuration = configuration,
                    NoBuild = true,
                    NoRestore = true,
                    OutputDirectory = artefactsDirectory + Directory("Publish"),
                });
        });

#if (Docker)
Task("DockerBuild")
    .Description("Builds a Docker image.")
    .DoesForEach(
        GetFiles("./**/Dockerfile").Where(x => !x.Segments.Contains(".devcontainer")),
        dockerfile =>
        {
            tag = tag ?? dockerfile.GetDirectory().GetDirectoryName().ToLower();
            var version = GetVersion();
            var gitCommitSha = GetGitCommitSha();

            // Docker buildx allows you to build Docker images for multiple platforms (including x64, x86 and ARM64) and
            // push them at the same time. To enable buildx, you may need to enable experimental support with these commands:
            // docker buildx create --name builder --driver docker-container --use
            // docker buildx inspect --bootstrap
            // To stop using buildx remove the buildx parameter and the --platform, --progress switches.
            // See https://github.com/docker/buildx
            var exitCode = StartProcess(
                "docker",
                new ProcessArgumentBuilder()
                    .Append("buildx")
                    .Append("build")
                    .AppendSwitchQuoted("--platform", platform)
                    .AppendSwitchQuoted("--progress", BuildSystem.IsLocalBuild ? "auto" : "plain")
                    .Append($"--push={push}")
                    .AppendSwitchQuoted("--tag", $"{tag}:{version}")
                    .AppendSwitchQuoted("--build-arg", $"Configuration={configuration}")
                    .AppendSwitchQuoted("--label", $"org.opencontainers.image.created={DateTimeOffset.UtcNow:o}")
                    .AppendSwitchQuoted("--label", $"org.opencontainers.image.revision={gitCommitSha}")
                    .AppendSwitchQuoted("--label", $"org.opencontainers.image.version={version}")
                    .AppendSwitchQuoted("--file", dockerfile.ToString())
                    .Append(".")
                    .RenderSafe());
            if (exitCode != 0)
            {
                throw new Exception($"Docker build failed with non zero exit code {exitCode}.");
            }

            // If you'd rather not use buildx, then you can uncomment these lines instead.
            // var exitCode = StartProcess(
            //     "docker",
            //     new ProcessArgumentBuilder()
            //         .Append("build")
            //         .AppendSwitchQuoted("--tag", $"{tag}:{version}")
            //         .AppendSwitchQuoted("--build-arg", $"Configuration={configuration}")
            //         .AppendSwitchQuoted("--label", $"org.opencontainers.image.created={DateTimeOffset.UtcNow:o}")
            //         .AppendSwitchQuoted("--label", $"org.opencontainers.image.revision={gitCommitSha}")
            //         .AppendSwitchQuoted("--label", $"org.opencontainers.image.version={version}")
            //         .AppendSwitchQuoted("--file", dockerfile.ToString())
            //         .Append(".")
            //         .RenderSafe());
            // if (exitCode != 0)
            // {
            //     throw new Exception($"Docker build failed with non zero exit code {exitCode}.");
            // }
            //
            // if (push)
            // {
            //     var pushExitCode = StartProcess(
            //         "docker",
            //         new ProcessArgumentBuilder()
            //             .AppendSwitchQuoted("push", $"{tag}:{version}")
            //             .RenderSafe());
            //     if (pushExitCode != 0)
            //     {
            //         throw new Exception($"Docker push failed with non zero exit code {pushExitCode}.");
            //     }
            // }

            string GetVersion()
            {
                var directoryBuildPropsFilePath = GetFiles("Directory.Build.props").Single().ToString();
                var directoryBuildPropsDocument = System.Xml.Linq.XDocument.Load(directoryBuildPropsFilePath);
                var preReleaseIdentifiers = directoryBuildPropsDocument.Descendants("MinVerDefaultPreReleaseIdentifiers").Single().Value;

                var exitCode = StartProcess(
                    "dotnet",
                    new ProcessSettings()
                        .WithArguments(x => x
                            .Append("minver")
                            .AppendSwitch("--default-pre-release-identifiers", preReleaseIdentifiers))
                        .SetRedirectStandardOutput(true),
                        out var versionLines);
                if (exitCode != 0)
                {
                    throw new Exception($"dotnet minver failed with non zero exit code {exitCode}.");
                }

                return versionLines.LastOrDefault();
            }

            string GetGitCommitSha()
            {
                StartProcess(
                    "git",
                    new ProcessSettings()
                        .WithArguments(x => x.Append("rev-parse HEAD"))
                        .SetRedirectStandardOutput(true),
                    out var shaLines);
                return shaLines?.LastOrDefault() ?? string.Empty;
            }
        });

Task("Default")
    .Description("Cleans, restores NuGet packages, builds the solution, runs unit tests and then builds a Docker image.")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("DockerBuild");
#else
Task("Default")
    .Description("Cleans, restores NuGet packages, builds the solution, runs unit tests and then publishes.")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("Publish");
#endif

RunTarget(target);
