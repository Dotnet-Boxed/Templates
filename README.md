![.NET Boxed Banner](https://github.com/Dotnet-Boxed/Templates/blob/main/Images/Banner.png)

 [![Boilerplate.Templates NuGet Package](https://img.shields.io/nuget/v/Boxed.Templates.svg)](https://www.nuget.org/packages/Boxed.Templates/) [![Boxed.Templates package in dotnet-boxed feed in Azure Artifacts](https://feeds.dev.azure.com/dotnet-boxed/_apis/public/Packaging/Feeds/03bd56a4-9269-43f7-9f75-d82037c56a46/Packages/d253caa8-4749-4cc9-892d-1342497a439e/Badge)](https://dev.azure.com/dotnet-boxed/Templates/_packaging?_a=package&feed=03bd56a4-9269-43f7-9f75-d82037c56a46&package=d253caa8-4749-4cc9-892d-1342497a439e&preferRelease=true) [![Boilerplate.Templates NuGet Package Downloads](https://img.shields.io/nuget/dt/Boilerplate.Templates)](https://www.nuget.org/packages/Boilerplate.Templates) [![Twitter URL](https://img.shields.io/twitter/url/http/shields.io.svg?style=social)](https://twitter.com/RehanSaeedUK) [![Twitter Follow](https://img.shields.io/twitter/follow/rehansaeeduk.svg?style=social&label=Follow)](https://twitter.com/RehanSaeedUK)

Project templates with batteries included, providing the minimum amount of code required to get you going.

## Project Templates

### [ASP.NET Core API Boxed](https://github.com/Dotnet-Boxed/Templates/blob/main/Docs/API.md)
[![ASP.NET Core API Boxed Preview Image](https://github.com/Dotnet-Boxed/Templates/blob/main/Images/API-Preview.png)](https://github.com/Dotnet-Boxed/Templates/blob/main/Docs/API.md)

### [ASP.NET Core GraphQL Boxed](https://github.com/Dotnet-Boxed/Templates/blob/main/Docs/GraphQL.md)
[![ASP.NET Core GraphQL Boxed Preview Image](https://github.com/Dotnet-Boxed/Templates/blob/main/Images/GraphQL-Preview.png)](https://github.com/Dotnet-Boxed/Templates/blob/main/Docs/GraphQL.md)

### [Microsoft Orleans Boxed](https://github.com/Dotnet-Boxed/Templates/blob/main/Docs/Orleans.md)
[![Microsoft Orleans Boxed Preview Image](https://github.com/Dotnet-Boxed/Templates/blob/main/Images/Orleans-Preview.png)](https://github.com/Dotnet-Boxed/Templates/blob/main/Docs/Orleans.md)

### [NuGet Package Boxed](https://github.com/Dotnet-Boxed/Templates/blob/main/Docs/NuGet.md)
[![NuGet Package Boxed Preview Image](https://github.com/Dotnet-Boxed/Templates/blob/main/Images/NuGet-Preview.png)](https://github.com/Dotnet-Boxed/Templates/blob/main/Docs/NuGet.md)

## Item Templates

- `.editorconfig` - A very generic [.editorconfig](https://github.com/RehanSaeed/EditorConfig) file supporting .NET, C#, VB and web technologies.
- `.gitattributes` - A [.gitattributes](https://rehansaeed.com/gitattributes-best-practices/) file supporting normalized line endings and Git Large File System (LFS).

## How can I install it?

1. Install the latest [.NET Core SDK](https://dot.net).
2. Run `dotnet new --install Boxed.Templates` to install the project templates.

## How can I use it?

#### Using Visual Studio:
1. Select .NET Boxed from the project type drop down.
2. Select the .NET Boxed template you want to install and follow the instructions.
![Visual Studio New Project Dialogue](https://github.com/Dotnet-Boxed/Templates/blob/main/Images/VisualStudio-NewProject.png)

#### Using the CLI:
1. Choose a project template i.e. `api`, `graphql`, `nuget`, `orleans`.
2. Run `dotnet new api --help` to see how to select the feature of the project.
3. Run `dotnet new api --name "MyProject"` along with any other custom options to create a project from the template.

## Release Notes and To-Do List

You can find release notes for each version [here](https://github.com/Dotnet-Boxed/Templates/releases) and a To-Do list of new features and enhancements coming soon in the [projects](https://github.com/Dotnet-Boxed/Templates/projects) tab.

## Continuous Integration

| Name            | Operating System      | Status | History |
| :---            | :---                  | :---   | :---    |
| Azure Pipelines | Ubuntu                | [![Azure Pipelines Ubuntu Build Status](https://dev.azure.com/dotnet-boxed/Templates/_apis/build/status/Dotnet-Boxed.Templates?branchName=main&stageName=Build&jobName=Build&configuration=Build%20Linux)](https://dev.azure.com/dotnet-boxed/Templates/_build/latest?definitionId=2&branchName=main) | |
| Azure Pipelines | Mac                   | [![Azure Pipelines Mac Build Status](https://dev.azure.com/dotnet-boxed/Templates/_apis/build/status/Dotnet-Boxed.Templates?branchName=main&stageName=Build&jobName=Build&configuration=Build%20Mac)](https://dev.azure.com/dotnet-boxed/Templates/_build/latest?definitionId=2&branchName=main) | |
| Azure Pipelines | Windows               | [![Azure Pipelines Windows Build Status](https://dev.azure.com/dotnet-boxed/Templates/_apis/build/status/Dotnet-Boxed.Templates?branchName=main&stageName=Build&jobName=Build&configuration=Build%20Windows)](https://dev.azure.com/dotnet-boxed/Templates/_build/latest?definitionId=2&branchName=main) | |
| Azure Pipelines | Overall               | [![Azure Pipelines Overall Build Status](https://dev.azure.com/dotnet-boxed/Templates/_apis/build/status/Dotnet-Boxed.Templates?branchName=main)](https://dev.azure.com/dotnet-boxed/Templates/_build/latest?definitionId=2&branchName=main) | [![Azure Pipelines Build History](https://buildstats.info/azurepipelines/chart/dotnet-boxed/Templates/2?branch=main&includeBuildsFromPullRequest=false)](https://dev.azure.com/dotnet-boxed/Templates/_build/latest?definitionId=2&branchName=main) |
| GitHub Actions  | Ubuntu, Mac & Windows | [![GitHub Actions Status](https://github.com/Dotnet-Boxed/Templates/workflows/Build/badge.svg?branch=main)](https://github.com/Dotnet-Boxed/Templates/actions) | [![GitHub Actions Build History](https://buildstats.info/github/chart/Dotnet-Boxed/Templates?branch=main&includeBuildsFromPullRequest=false)](https://github.com/Dotnet-Boxed/Templates/actions) |
| AppVeyor        | Ubuntu, Mac & Windows | [![AppVeyor Build Status](https://ci.appveyor.com/api/projects/status/munmh9if4vfeqy62/branch/main?svg=true)](https://ci.appveyor.com/project/RehanSaeed/templates/branch/main) | [![AppVeyor Build History](https://buildstats.info/appveyor/chart/RehanSaeed/Templates?branch=main&includeBuildsFromPullRequest=false)](https://ci.appveyor.com/project/RehanSaeed/Templates) |

## Contributions and Thanks

Please view the [Contributing](/.github/CONTRIBUTING.md) guide for more information.

- [tomecho](https://github.com/tomecho) - Fixing GraphQL Apollo Tracing.
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
- [rarrarrarr](https://github.com/rarrarrarr) - Fixed grammar mistakes.
