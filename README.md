![ASP.NET Core Boilerplate Banner](https://raw.githubusercontent.com/ASP-NET-Core-Boilerplate/Templates/master/Images/Banner.png)

![ASP.NET-Core-Boilerplate in the Visual Studio 'New Project' dialogue](https://raw.githubusercontent.com/ASP-NET-Core-Boilerplate/Templates/master/Images/New%20Project.png)

[![AppVeyor Build status](https://ci.appveyor.com/api/projects/status/munmh9if4vfeqy62?svg=true)](https://ci.appveyor.com/project/RehanSaeed/templates) [![Gitter](https://img.shields.io/gitter/room/nwjs/nw.js.svg?maxAge=2592000)](https://gitter.im/ASP-NET-MVC-Boilerplate/Lobby?utm_source=share-link&utm_medium=link&utm_campaign=share-link)

A professional ASP.NET Core template for building secure, fast, robust and adaptable web applications or sites. It provides the minimum amount of code required on top of the default ASP.NET Core template provided by Microsoft. Find out more at [RehanSaeed.com](http://rehansaeed.com/ASP-NET-Core-Boilerplate/), the [Visual Studio Gallery](https://visualstudiogallery.msdn.microsoft.com/6cf50a48-fc1e-4eaf-9e82-0b2a6705ca7d) or at [The Open Web Security Project (OWASP)](https://www.owasp.org/index.php/OWASP_ASP.NET_MVC_Boilerplate_Project). You can also follow me on Twitter at [@RehanSaeedUK](https://twitter.com/rehansaeeduk).</p>

## Why Do I Need It?

The default ASP.NET Core template that Visual Studio gives you does not make best use of the tools available. It's insecure, slow, and really has a very basic feature list (That's the point of it). ASP.NET Core Boilerplate provides you with a few more pieces of the puzzle to get you started quicker. It makes liberal use of comments and even gives you a checklist of tasks which you need to perform to make it even better. The main benefits of using this template are:

- Security
- Performance
- Search Engine Optimization (SEO)
- Accessibility
- Browser Compatibility
- Resilience and Error Handling
- Easier Debugging and Performance Testing Tools
- Patterns and Practices
- Atom Feed
- Search
- Social Media Support

## Project Templates

Both ASP.NET Core and ASP.NET 4.6 MVC 5 are supported with their own project templates.

### [ASP.NET Core MVC](https://github.com/ASP-NET-Core-Boilerplate/Templates/blob/master/MVC%206.md)

#### Preview Image

![ASP.NET Core Boilerplate Preview Image](https://raw.githubusercontent.com/ASP-NET-Core-Boilerplate/Templates/master/Images/MVC%206%20Preview%20Image.png)

#### Technology Map

The ASP.NET Core MVC project template contains the following features:

![ASP.NET Core Boilerplate Technology Map](https://raw.githubusercontent.com/ASP-NET-Core-Boilerplate/Templates/master/Images/MVC%206%20Technology%20Map.png)

#### Feature Selection Wizard

The ASP.NET Core MVC project template comes with a feature selection wizard where literally everything can be turned on
or off with the click of a button for a truly personalized project.

![ASP.NET Core Boilerplate Technology Map](https://raw.githubusercontent.com/ASP-NET-Core-Boilerplate/Templates/master/Images/ASP.NET%20MVC%20Boilerplate%20Feature%20Selection%20Wizard%201.png)










### [ASP.NET Core API](https://github.com/ASP-NET-Core-Boilerplate/Templates/blob/master/MVC%206%20API.md)

#### Preview Image

![ASP.NET Core API Boilerplate Preview Image](https://raw.githubusercontent.com/ASP-NET-Core-Boilerplate/Templates/master/Images/MVC%206%20API%20Preview%20Image.png)

#### Technology Map

The ASP.NET Core API project template contains the following features:

![ASP.NET Core API Boilerplate Technology Map](https://raw.githubusercontent.com/ASP-NET-Core-Boilerplate/Templates/master/Images/MVC%206%20API%20Technology%20Map.png)

#### Dotnet New

This template is not available from Visual Studio (yet!) but from the new `dotnet new` command line tool. Find out more about `dotnet new` [here](http://rehansaeed.com/custom-project-templates-using-dotnet-new/).











### [ASP.NET 4.6 MVC 5](https://github.com/ASP-NET-Core-Boilerplate/Templates/blob/master/MVC%205.md)

#### Preview Image

![ASP.NET MVC 5 Boilerplate Preview Image](https://raw.githubusercontent.com/ASP-NET-Core-Boilerplate/Templates/master/Images/MVC%205%20Preview%20Image.png)

#### Technology Map

The ASP.NET 4.6 MVC 5 project template contains the following features:

![ASP.NET MVC 5 Boilerplate Technology Map](https://raw.githubusercontent.com/ASP-NET-Core-Boilerplate/Templates/master/Images/MVC%205%20Technology%20Map.png)

## How Can I Get It?

| Name                           | Information | Download |
| :---                           | :---        | :---     |
| dotnet new NuGet package (For API template only)       | 1. You must have the latest version of the dotnet tooling. This comes with Visual Studio 2017 or from [dot.net](https://dot.net). <br> 2. If you have a newer .NET Core Preview SDK installed then use a [global.json](https://www.hanselman.com/blog/ManagingDotnetCore20AndDotnetCore1xVersionedSDKsOnTheSameMachine.aspx) to default to the current stable SDK. <br> 3. Run `dotnet new --install "Boilerplate.Templates::*"` to install the project template. <br> 4. Run `dotnet new api --help` to see how to select the feature of the project. <br> 5. Run `dotnet new bapi --name "MyTemplate"` along with any other custom options to create a project from the template. | [![Boilerplate.Templates NuGet Package](https://img.shields.io/nuget/v/Boilerplate.Templates.svg)](https://www.nuget.org/packages/Boilerplate.Templates/) |
| Visual Studio Extension (VSIX) | Install extension, then [create project](https://raw.githubusercontent.com/RehanSaeed/ASP.NET-Core-Boilerplate/master/Images/New%20Project.png). | [![Visual Studio Gallery](https://img.shields.io/badge/Visual%20Studio%20Gallery-Download-blue.svg)](https://visualstudiogallery.msdn.microsoft.com/6cf50a48-fc1e-4eaf-9e82-0b2a6705ca7d/file/148517/112/ASP.NET%20MVC%20Boilerplate.vsix) |
| Visual Studio Extension (VSIX) | Follow [instructions](http://docs.myget.org/docs/walkthrough/getting-started-with-vsix) to add feed to Visual Studio. | [![My-Get Development Feed](https://img.shields.io/badge/My--Get%20Feed-Feed-blue.svg?link=https://visualstudiogallery.msdn.microsoft.com/6cf50a48-fc1e-4eaf-9e82-0b2a6705ca7d/file/148517/112/ASP.NET%20MVC%20Boilerplate.vsix)](https://www.myget.org/F/aspnet-mvc-boilerplate/vsix/) |
| Git Clone                      | Clone the git repository. | `git clone https://github.com/ASP-NET-Core-Boilerplate/Templates` |

## Release Notes & To-Do List
You can find release notes for each version [here](https://github.com/ASP-NET-Core-Boilerplate/Templates/blob/master/RELEASE%20NOTES.md) and a To-Do list of new features and enhancements coming soon in the [projects](https://github.com/ASP-NET-Core-Boilerplate/Templates/projects) tab.

## Sites Built Using The Template

Add your site here, just raise an issue.

- [adddemo.com](http://www.adddemo.com/postakodu/) (MVC 5) by [ademsenel](https://github.com/ademsenel).

## Contributions and Thanks

- [sayedihashimi](https://github.com/sayedihashimi) - Fixed dotnet new command in ReadMe.
- [bobinush](https://github.com/bobinush) - Fixed typo.
- [chrisrichards](https://github.com/chrisrichards) - Fixed paging in the API template.
- [VeXHarbinger](https://github.com/VeXHarbinger) - Updated Twitter cards.
- [michalstanko](https://github.com/michalstanko) - Added Czech and Slovak characters to the FriendlyUrlHelper.
- [miroslavpopovic](https://github.com/miroslavpopovic) - Small fix to SiteMapGenerator.
- [Matthew-Bonner](https://github.com/Matthew-Bonner) - Fix removal of trailing slash for manifest.json.
- [Abuson](https://github.com/abuson) - Two ASP.NET Core MVC fixes to do with error pages.
- [Shiney](https://github.com/Shiney) - Fixed typo.
- [ChrisOMetz](https://github.com/ChrisOMetz) - ASP.NET Core LESS feature.
- [mcliment](https://github.com/mcliment) - Updated template to ASP.NET Core RC 1.
- [surfsflo](https://github.com/surfsflo) - Added woff2 support for Font Awesome.
