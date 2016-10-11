#tool "nuget:?package=xunit.runner.console"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var artifactsDirectory = Directory("./Artifacts");
var packagesDirectory = Directory("./packages");

Task("Clean")
    .Does(() =>
    {
        CleanDirectory(artifactsDirectory);
    });

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        DotNetCoreRestore();
        foreach (var project in GetFiles("./**/*.csproj"))
        {
            NuGetRestore(
                project,
                new NuGetRestoreSettings()
                {
                    PackagesDirectory = packagesDirectory
                });
        }
    });

 Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        // Build VSIX
		var vsixProject = GetFiles("./**/Boilerplate.Vsix.csproj").First();
        MSBuild(vsixProject, settings => settings
            .SetConfiguration(configuration)
            .SetPlatformTarget(PlatformTarget.MSIL)
            .SetMSBuildPlatform(MSBuildPlatform.x86)
            .WithTarget("Build")
            .WithProperty("DeployExtension", "false"));
        CopyFileToDirectory(GetFiles("./**/*.vsix").First(), artifactsDirectory);

        // Build Tests
        foreach (var project in GetFiles("./Tests/**/*.csproj"))
        {
            MSBuild(project, settings => settings.SetConfiguration(configuration));
        }
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var projects = GetFiles("./Tests/**/bin/" + configuration + "/*Test.dll");
        foreach(var project in projects)
        {
            XUnit2(
                project.FullPath,
                new XUnit2Settings()
                {
                    OutputDirectory = artifactsDirectory,
                    Parallelism = ParallelismOption.All,
                    XmlReport = true
                });
        }
    });

Task("Default")
    .IsDependentOn("Test");

RunTarget(target);