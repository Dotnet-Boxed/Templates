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
        var project = GetFiles("./**/Boilerplate.Vsix.csproj").First();
            if(IsRunningOnWindows())
            {
                // MSBuild(project, settings => settings.SetConfiguration(configuration));
                MSBuild(project, settings => settings
                    //.SetPlatformTarget(PlatformTarget.MSIL)
                    //.SetMSBuildPlatform(MSBuildPlatform.x86)
			        //.WithProperty("TreatWarningsAsErrors", "true")
			        //.SetVerbosity(Verbosity.Quiet)
			        .WithTarget("Build")
                    .SetConfiguration(configuration));
            }
            else
            {
                XBuild(project, settings => settings.SetConfiguration(configuration));
            }
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var projects = GetFiles("./Tests/**/*.xproj");
        foreach(var project in projects)
        {
            // if(IsRunningOnWindows())
            // {
                DotNetCoreTest(
                    project.GetDirectory().FullPath,
                    new DotNetCoreTestSettings()
                    {
                        ArgumentCustomization = args => args
                            .Append("-xml")
                            .Append(artifactsDirectory.Path.CombineWithFilePath(project.GetFilenameWithoutExtension()).FullPath + ".xml"),
                        Configuration = configuration,
                        NoBuild = true
                    });
            // }
            // else
            // {
            //     var name = project.GetFilenameWithoutExtension();
            //     var dirPath = project.GetDirectory().FullPath;

            //     foreach (var file in GetFiles(dirPath))
            //     {
            //         Information(file.FullPath);
            //     }

            //     var xunit = GetFiles(dirPath + "/bin/" + configuration + "/**/dotnet-test-xunit.exe").First().FullPath;
            //     Information("dotnet-test-xunit.exe File Path: " + xunit);
            //     var testfile = GetFiles(dirPath + "/bin/" + configuration + "/**/" + name + ".dll").First().FullPath;
            //     Information("Assembly File Path: " + xunit);

            //     using (var process = StartAndReturnProcess("mono", new ProcessSettings{ Arguments = xunit + " " + testfile }))
            //     {
            //         process.WaitForExit();
            //         if (process.GetExitCode() != 0)
            //         {
            //             throw new Exception("Mono tests failed!");
            //         }
            //     }
            // }
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