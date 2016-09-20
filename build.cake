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
			.WithTarget("Build"));
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
        Information("Testing");
        var projects = GetFiles("./Tests/**/*.dll");
        foreach(var project in projects)
        {
            Information("Testing: " + project);
            XUnit2(
                project.GetDirectory().FullPath,
                new XUnit2Settings()
                {
                    OutputDirectory = artifactsDirectory
                });
        }
    });

Task("Upload-AppVeyor-Artifacts")
    .IsDependentOn("Test")
    .WithCriteria(() => AppVeyor.IsRunningOnAppVeyor)
    .Does(() =>
    {
        foreach(var file in GetFiles(artifactsDirectory.Path + "/*"))
        {
            AppVeyor.UploadArtifact(file);
        }
    });

Task("Default")
    .IsDependentOn("Upload-AppVeyor-Artifacts");

RunTarget(target);