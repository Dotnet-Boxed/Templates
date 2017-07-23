#tool "nuget:?package=xunit.runner.console"
#r "System.Net.Http"
#r "System.Xml.Linq"

using System.Net.Http;
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
var packagesDirectory = Directory("./packages");
var nuspecFile = GetFiles("./**/*.nuspec").First().ToString();
var nuspecContent = string.Empty;

Task("Clean")
    .Does(() =>
    {
        CleanDirectory(artifactsDirectory);
        DeleteDirectories(GetDirectories("**/bin"), true);
        DeleteDirectories(GetDirectories("**/obj"), true);
    });

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        // Not building templates until issue https://github.com/dotnet/templating/issues/442 is fixed and released.
        // var projects = GetFiles("./**/Boilerplate.Templates/**/*.csproj");
        // foreach(var project in projects)
        // {
        //     DotNetCoreRestore(project.FullPath);
        // }
    });

 Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        // Not building templates until issue https://github.com/dotnet/templating/issues/442 is fixed and released.
        // var projects = GetFiles("./**/Boilerplate.Templates/**/*.csproj");
        // foreach(var project in projects)
        // {
        //      DotNetCoreBuild(
        //          project.GetDirectory().FullPath,
        //          new DotNetCoreBuildSettings()
        //          {
        //              Configuration = configuration
        //          });
        // }
    });

Task("Version")
    .IsDependentOn("Build")
    .Does(() =>
    {
        string versionSuffix = string.Empty;
        if (!string.IsNullOrEmpty(preReleaseSuffix))
        {
            versionSuffix = "-" + preReleaseSuffix + "-" + buildNumber.ToString("D4");
        }

        nuspecContent = System.IO.File.ReadAllText(nuspecFile);
        System.IO.File.WriteAllText(nuspecFile, nuspecContent.Replace("-*", versionSuffix));
        Information("VersionSuffix set to " + versionSuffix);
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

Task("Restore-VSIX")
    .IsDependentOn("Pack")
    .Does(() =>
    {
        var projects = new string[]
        {
            "Boilerplate.Web.Mvc5.Sample.csproj",
            "Boilerplate.FeatureSelection.csproj",
            "Boilerplate.FeatureSelection.Client.csproj",
            "Boilerplate.Vsix.csproj",
            "Boilerplate.Wizard.csproj",
            "Boilerplate.FeatureSelection.FunctionalTest.csproj",
            "Boilerplate.FeatureSelection.Test.csproj"
        };
        foreach (var project in GetFiles("./**/*.csproj")
            .Where(x => projects.Contains(x.GetFilename().ToString())))
        {
            Information("nuget restore " + project.ToString());
            NuGetRestore(
                project,
                new NuGetRestoreSettings()
                {
                    PackagesDirectory = packagesDirectory
                });
        }
    });

Task("Version-VSIX")
    .WithCriteria(() => buildNumber != 0)
    .IsDependentOn("Restore-VSIX")
    .Does(() =>
    {
        var vsixManifest = GetFiles("./**/source.extension.vsixmanifest").First();

        var document = XDocument.Parse(System.IO.File.ReadAllText(vsixManifest.ToString()));
        var ns = XNamespace.Get("http://schemas.microsoft.com/developer/vsx-schema/2011");
        var versionAttribute = document.Descendants(ns + "Identity").First().Attribute("Version");

        var version = new Version(versionAttribute.Value);
        versionAttribute.Value = version.Major + "." + version.Minor + "." + version.Build + "." + buildNumber.ToString("0000");
        document.Save(vsixManifest.ToString());

        Information("Version updated from " + version + " to " + versionAttribute.Value);
    });

 Task("Build-VSIX")
    .IsDependentOn("Version-VSIX")
    .Does(() =>
    {
        // Build VSIX
        var vsixProject = GetFiles("./**/Boilerplate.Vsix.csproj").First();
        MSBuild(vsixProject, settings => settings
            .UseToolVersion(MSBuildToolVersion.VS2017)
            .SetConfiguration(configuration)
            .WithProperty("DeployExtension", "false")
            .WithProperty("VisualStudioVersion", "15.0"));
        CopyFileToDirectory(GetFiles("./**/*.vsix").First(), artifactsDirectory);

        // Build Tests
        foreach (var project in GetFiles("./Tests/**/Boilerplate.FeatureSelection.*Test.csproj"))
        {
            MSBuild(project, settings => settings.SetConfiguration(configuration));
        }
    });

Task("Test-VSIX")
    .IsDependentOn("Build-VSIX")
    .Does(() =>
    {
        var projects = GetFiles("./Tests/**/bin/" + configuration + "/Boilerplate.FeatureSelection.*Test.dll");
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

Task("Publish-VSIX")
    .WithCriteria(() =>
        !string.IsNullOrEmpty(mygetApiKey) &&
        (!AppVeyor.IsRunningOnAppVeyor || AppVeyor.Environment.Repository.Branch == "master"))
    .IsDependentOn("Test-VSIX")
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
    .IsDependentOn("Publish-VSIX");

RunTarget(target);