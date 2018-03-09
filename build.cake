using System.Xml.Linq;

var target = Argument("Target", "Default");
var configuration =
    HasArgument("Configuration") ? Argument<string>("Configuration") :
    EnvironmentVariable("Configuration") != null ? EnvironmentVariable("Configuration") :
    "Release";
var mygetApiKey =
    HasArgument("MyGetApiKey") ? Argument<string>("MyGetApiKey") :
    EnvironmentVariable("MyGetApiKey") != null ? EnvironmentVariable("MyGetApiKey") :
    null;
var preReleaseSuffix =
    HasArgument("PreReleaseSuffix") ? Argument<string>("PreReleaseSuffix") :
    (AppVeyor.IsRunningOnAppVeyor && AppVeyor.Environment.Repository.Tag.IsTag) ? null :
    EnvironmentVariable("PreReleaseSuffix") != null ? EnvironmentVariable("PreReleaseSuffix") :
    "beta";
var buildNumber = HasArgument("BuildNumber") ?
    Argument<int>("BuildNumber") :
    AppVeyor.IsRunningOnAppVeyor ? AppVeyor.Environment.Build.Number :
    EnvironmentVariable("BuildNumber") != null ? int.Parse(EnvironmentVariable("BuildNumber")) :
    0;

var artifactsDirectory = Directory("./Artifacts");
var nuspecFile = GetFiles("./**/*.nuspec").First().ToString();
var nuspecContent = string.Empty;
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
                NoRestore = true,
                VersionSuffix = versionSuffix
            });
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        foreach(var project in GetFiles("./Tests/**/*.csproj"))
        {
            var outputFilePath = MakeAbsolute(artifactsDirectory.Path)
                .CombineWithFilePath(project.GetFilenameWithoutExtension());
            DotNetCoreTool(
                project,
                "xunit",
                new ProcessArgumentBuilder()
                    .AppendSwitch("-configuration", configuration)
                    .AppendSwitchQuoted("-xml", outputFilePath.AppendExtension(".xml").ToString())
                    .AppendSwitchQuoted("-html", outputFilePath.AppendExtension(".html").ToString()));
        }
    });

Task("Version")
    .IsDependentOn("Test")
    .Does(() =>
    {
        nuspecContent = System.IO.File.ReadAllText(nuspecFile);
        var nuspecDocument = XDocument.Parse(nuspecContent);
        var ns = XNamespace.Get("http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd");
        var versionElement = nuspecDocument.Element(ns + "package").Element(ns + "metadata").Element(ns + "version");
        versionElement.Value = versionElement.Value.Replace("*", versionSuffix);
        System.IO.File.WriteAllText(nuspecFile, nuspecDocument.ToString());
        Information($"VersionSuffix set to {versionSuffix}");
    });

Task("Pack")
    .IsDependentOn("Version")
    .Does(() =>
    {
        NuGetPack(
            nuspecFile,
            new NuGetPackSettings()
            {
                OutputDirectory = artifactsDirectory
            });
        System.IO.File.WriteAllText(nuspecFile, nuspecContent);
    });

Task("Default")
    .IsDependentOn("Pack");

RunTarget(target);