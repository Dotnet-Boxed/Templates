#tool "nuget:?package=xunit.runner.console"
#r "System.Net.Http"

using System.Net.Http;

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var mygetApiKey = Argument<string>("MyGetApiKey");

var artifactsDirectory = Directory("./Artifacts");
var packagesDirectory = Directory("./packages");

Task("Clean")
    .Does(() =>
    {
        CleanDirectory(artifactsDirectory);
        CleanDirectories("./**/bin/");
        CleanDirectories("./**/obj/");
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

Task("Publish-MyGet")
    .WithCriteria(() =>
        HasArgument("MyGetApiKey") &&
        (!AppVeyor.IsRunningOnAppVeyor || AppVeyor.Environment.Repository.Branch == "master"))
    .IsDependentOn("Test")
    .Does(() =>
    {
        var vsixFilePath = GetFiles(artifactsDirectory.Path + "/*.vsix").First();
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("X-NuGet-ApiKey", mygetApiKey);
            var fileStream = new FileStream(vsixFilePath.ToString(), FileMode.Open, FileAccess.Read, FileShare.Read);
            var response = httpClient
                .PostAsync(
                    "https://www.myget.org/F/aspnet-mvc-boilerplate/vsix/upload",
                    new StreamContent(fileStream))
                .GetAwaiter()
                .GetResult();
            if (response.IsSuccessStatusCode)
            {
                Information("VSIX uploaded successfully.");
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                Error("VSIX upload failed with status code " + (int)response.StatusCode + " " + response.StatusCode);
            }
        }
    });

Task("Default")
    .IsDependentOn("Publish-MyGet");

RunTarget(target);