# How To Build

To build this project you will require:

1. Visual Studio 2015 Update 2
2. Visual Studio 2015 SDK
3. Latest version of ASP.NET Core
4. [SideWaffle](https://visualstudiogallery.msdn.microsoft.com/a16c2d07-b2e1-4a25-87d9-194f04e7a698) Visual Studio Extension

# How To Debug

1. Set Boilerplate.Vsix as the start-up project.
2. Open Boilerplate.Vsix Project Properties -> Debug Tab
3. Set 'Start external program' to:
    > C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.exe
4. Set command line arguments to:
    > /rootsuffix Exp
5. Delete any node_modules and bower_components folders under the MVC 6 projects. This will stop errors complaining of long file paths.
6. Hit F5 to run. An experimental instance of Visual Studio will start and you can create a new project template from there.

Alternatively you can run the MVC 5 project itself to try it out. The MVC 6 projects cannot be run because of Microsoft's failure to implement comments in the various JSON config files which are required for the feature selection wizard (See http://blog.getify.com/json-comments/ for why this is silly).
