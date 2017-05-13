# How To Build

To build this project you will require:

1. Visual Studio 2015 Update 2
2. Visual Studio 2015 SDK
3. Latest version of ASP.NET Core
4. [SideWaffle](https://visualstudiogallery.msdn.microsoft.com/a16c2d07-b2e1-4a25-87d9-194f04e7a698) Visual Studio Extension

# How To Debug

1. Set Boilerplate.Vsix as the start-up project.
2. Hit F5 to run. An experimental instance of Visual Studio with the ASP.NET Core Boilerplate extension will start and you can create a new project template using it.

Alternatively you can run the MVC 5 project itself to try it out. The MVC 6 projects cannot be run because of NPM's failure to implement comments in the packages.json file which means that the node modules will not be installed (See http://blog.getify.com/json-comments/ for why this is silly).