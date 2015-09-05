To build this project you will require:

1. Visual Studio 2015
2. Visual Studio 2015 SDK
3. SideWaffle Visual Studio Extension

To debug this project:

1. Set Boilerplate.Vsix as the start-up project.
2. Open Boilerplate.Vsix Project Properties -> Debug Tab
3. Set 'Start external program' to:
   C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.exe
4. Set command line arguments to:
   /rootsuffix Exp
5. Delete any node_modules and bower_components folders under the MVC 5 and MVC 6 projects. This will stop errors complaining of long file paths.
6. Hit F5 to run. An experimental instance of Visual Studio will start and you can create a new project template from there.
