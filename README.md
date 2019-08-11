![.NET Boxed Banner](https://raw.githubusercontent.com/Dotnet-Boxed/Templates/master/Images/Banner.png)

 [![Boilerplate.Templates NuGet Package](https://img.shields.io/nuget/v/Boxed.Templates.svg)](https://www.nuget.org/packages/Boxed.Templates/) [![Boxed.Templates package in dotnet-boxed feed in Azure Artifacts](https://feeds.dev.azure.com/dotnet-boxed/_apis/public/Packaging/Feeds/03bd56a4-9269-43f7-9f75-d82037c56a46/Packages/d253caa8-4749-4cc9-892d-1342497a439e/Badge)](https://dev.azure.com/dotnet-boxed/Templates/_packaging?_a=package&feed=03bd56a4-9269-43f7-9f75-d82037c56a46&package=d253caa8-4749-4cc9-892d-1342497a439e&preferRelease=true) [![Twitter URL](https://img.shields.io/twitter/url/http/shields.io.svg?style=social)](https://twitter.com/RehanSaeedUK) [![Twitter Follow](https://img.shields.io/twitter/follow/rehansaeeduk.svg?style=social&label=Follow)](https://twitter.com/RehanSaeedUK)

Project templates with batteries included, providing the minimum amount of code required to get you going.

## Project Templates

### [ASP.NET Core API Boxed](https://github.com/Dotnet-Boxed/Templates/blob/master/Docs/API.md)
[![ASP.NET Core API Boxed Preview Image](https://raw.githubusercontent.com/Dotnet-Boxed/Templates/master/Images/API-Preview.png)](https://github.com/Dotnet-Boxed/Templates/blob/master/Docs/API.md)
### [ASP.NET Core GraphQL Boxed](https://github.com/Dotnet-Boxed/Templates/blob/master/Docs/GraphQL.md)
[![ASP.NET Core GraphQL Boxed Preview Image](https://raw.githubusercontent.com/Dotnet-Boxed/Templates/master/Images/GraphQL-Preview.png)](https://github.com/Dotnet-Boxed/Templates/blob/master/Docs/GraphQL.md)
### [Microsoft Orleans Boxed](https://github.com/Dotnet-Boxed/Templates/blob/master/Docs/Orleans.md)
[![Microsoft Orleans Boxed Preview Image](https://raw.githubusercontent.com/Dotnet-Boxed/Templates/master/Images/Orleans-Preview.png)](https://github.com/Dotnet-Boxed/Templates/blob/master/Docs/Orleans.md)

## How can I get it?

1. Install the latest [.NET Core SDK](https://dot.net).
2. Run `dotnet new --install Boxed.Templates` to install the project template.
3. Run `dotnet new api --help` to see how to select the feature of the project.
5. Run `dotnet new api --name "MyProject"` along with any other custom options to create a project from the template.

## Release Notes and To-Do List

You can find release notes for each version [here](https://github.com/Dotnet-Boxed/Templates/releases) and a To-Do list of new features and enhancements coming soon in the [projects](https://github.com/Dotnet-Boxed/Templates/projects) tab.

## Continuous Integration

| Name            | Operating System | Status | History |
| :---            | :---             | :---   | :---    |
| Azure Pipelines | Ubuntu           | [![Azure Pipelines Ubuntu Build Status](https://dev.azure.com/dotnet-boxed/Templates/_apis/build/status/Dotnet-Boxed.Templates?branchName=master&stageName=Build&jobName=Build&configuration=Build%20Linux)](https://dev.azure.com/dotnet-boxed/Templates/_build/latest?definitionId=2&branchName=master) | |
| Azure Pipelines | Mac              | [![Azure Pipelines Mac Build Status](https://dev.azure.com/dotnet-boxed/Templates/_apis/build/status/Dotnet-Boxed.Templates?branchName=master&stageName=Build&jobName=Build&configuration=Build%20Mac)](https://dev.azure.com/dotnet-boxed/Templates/_build/latest?definitionId=2&branchName=master) | |
| Azure Pipelines | Windows          | [![Azure Pipelines Windows Build Status](https://dev.azure.com/dotnet-boxed/Templates/_apis/build/status/Dotnet-Boxed.Templates?branchName=master&stageName=Build&jobName=Build&configuration=Build%20Windows)](https://dev.azure.com/dotnet-boxed/Templates/_build/latest?definitionId=2&branchName=master) | |
| Azure Pipelines | Overall          | [![Azure Pipelines Overall Build Status](https://dev.azure.com/dotnet-boxed/Templates/_apis/build/status/Dotnet-Boxed.Templates?branchName=master)](https://dev.azure.com/dotnet-boxed/Templates/_build/latest?definitionId=2&branchName=master) | |
| AppVeyor        | Ubuntu & Windows | [![AppVeyor Build status](https://ci.appveyor.com/api/projects/status/munmh9if4vfeqy62?svg=true)](https://ci.appveyor.com/project/RehanSaeed/templates) | [![AppVeyor Build history](https://buildstats.info/appveyor/chart/RehanSaeed/Templates?branch=master&includeBuildsFromPullRequest=false)](https://ci.appveyor.com/project/RehanSaeed/Templates) |

## Contributions and Thanks

Please view the [Contributing](CONTRIBUTING.md) guide for more information.

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
