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

Task("Update-Version")
    .WithCriteria(() => buildNumber != 0)
    .IsDependentOn("Restore")
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

 Task("Build")
    .IsDependentOn("Update-Version")
    .Does(() =>
    {
        // Build Template Projects
        var projects = GetFiles("./**/Boilerplate.Templates/**/*.xproj");
        foreach(var project in projects)
        {
            DotNetCoreBuild(
                project.GetDirectory().FullPath,
                new DotNetCoreBuildSettings()
                {
                    Configuration = configuration
                });
        }

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

Task("Pack")
    .IsDependentOn("Test")
    .Does(() =>
    {
        string versionSuffix = null;
		if (!string.IsNullOrEmpty(preReleaseSuffix))
		{
			versionSuffix = preReleaseSuffix + "-" + buildNumber.ToString("D4");
		}

        var nuspecFile = GetFiles("./**/Templates.nuspec").First().ToString();
        var content = System.IO.File.ReadAllText(nuspecFile);
        var newContent = content.Replace("-*", "-" + versionSuffix);
        System.IO.File.WriteAllText(nuspecFile, newContent);

        NuGetPack(
            nuspecFile,
            new NuGetPackSettings()
            {
                OutputDirectory = artifactsDirectory
            });
        System.IO.File.WriteAllText(nuspecFile, content);
    });

Task("Publish-MyGet")
    .WithCriteria(() =>
        !string.IsNullOrEmpty(mygetApiKey) &&
        (!AppVeyor.IsRunningOnAppVeyor || AppVeyor.Environment.Repository.Branch == "master"))
    .IsDependentOn("Pack")
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