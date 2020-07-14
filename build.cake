using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

var target = Argument("Target", "Default");
var configuration =
    HasArgument("Configuration") ? Argument<string>("Configuration") :
    EnvironmentVariable("Configuration") is object ? EnvironmentVariable("Configuration") :
    "Release";
var template =
    HasArgument("Template") ? Argument<string>("Template") :
    EnvironmentVariable("Template") is object ? EnvironmentVariable("Template") :
    null;

var artefactsDirectory = Directory("./Artefacts");
var templatePackProject = Directory("./Source/*.csproj");
var isDotnetRunEnabled = BuildSystem.IsLocalBuild || (!BuildSystem.IsLocalBuild && IsRunningOnWindows());
var isDockerEnabled = BuildSystem.IsLocalBuild;

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
        var filters = new List<string>();
        if (!isDotnetRunEnabled)
        {
            filters.Add("IsUsingDotnetRun=false");
        }

        if (!isDockerEnabled)
        {
            filters.Add("IsUsingDocker=false");
        }

        if (template != null)
        {
            filters.Add($"Template={template}");
        }

        DotNetCoreTest(
            project.ToString(),
            new DotNetCoreTestSettings()
            {
                Configuration = configuration,
                Filter = string.Join("&", filters),
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
    if (exitCode != 0 && !AzurePipelines.IsRunningOnAzurePipelinesHosted)
    {
        throw new Exception($"'{command}' failed with exit code {exitCode}.");
    }
}
