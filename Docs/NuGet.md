![.NET Boxed Banner](../Images/Banner.png)

![NuGet Package Boxed Preview Image](../Images/NuGet-Preview.png)

## Optional Feature Selection

The NuGet package project template uses `dotnet new` to enable you to turn features of the project template on or off. Find out more about `dotnet new` [here](http://rehansaeed.com/custom-project-templates-using-dotnet-new/).

#### Project

- **Title** - The name of the project which determines the assembly product name.
- **Description** - A description of the project which determines the assembly description.
- **Author** - The name of the author of the project which determines the assembly author and copyright information.
- **Tags** - A semi-colon `;` delimited list of tags for the NuGet package.
- **Contact** - The contact details to use if someone wants to contact you about a security vulnerability or code of conduct issues.
- **DotnetCore** - Sets .NET Core as one of the target frameworks.
- **DotnetFramework** - Sets .NET Framework as one of the target frameworks.
- **Sign** - Signs the NuGet package.
- **ReadMe** - Add a README.md markdown file describing the project.
- **EditorConfig** - Add a .editorconfig file to set a fixed code style.
- **License** - The legal license applied to the source code in this project.
  - **MIT** - The MIT license.
  - **None** - No license, the source code cannot be legally shared.
- **TreatWarningsAsErrors** - Treat warnings as errors.
- **StyleCop** - Adds and enforces StyleCop analysers.
- **Tests** - Adds a unit test project.

#### Source Control

- **SourceControl** - Select which source control provider you are using if any, to add provider specific features.
  - **AzureDevOpsServer** - Adds source link.
  - **AzureRepos** - Adds source link.
  - **Bitbucket** - Adds source link.
  - **GitHub** - Adds source link, .github directory containing a code of conduct, contributing guide, pull request template and issue templates.
  - **GitLab** - Adds source link.
  - **None** - No source control provider is being used.
- **GitHubUsername** - Your GitHub username or organisation name that the project lives under.
- **GitHubProject** - The name of your GitHub project.

#### Continuous Integration (CI)

- **AppVeyor** - Adds AppVeyor continuation integration build file appveyor.yml.
- **AzurePipelines** - Adds Azure Pipelines continuation integration build file azure-pipelines.yml.
- **GitHubActions** - Adds GitHub Actions continuation integration build file .github/workflow/build.yml.

## Always On Features

- **Signing** - The package is signed. However, you should change the .pfx file.
- **SourceLink** - During debugging, you can step into code from your NuGet package using [Source Link](https://docs.microsoft.com/en-us/dotnet/standard/library-guidance/sourcelink).

## How can I get it?

1. Install the latest [.NET Core SDK](https://dot.net).
2. Run `dotnet new --install Boxed.Templates` to install the project template.
3. Run `dotnet new nuget --help` to see how to select the feature of the project.
5. Run `dotnet new nuget --name "MyProject"` along with any other custom options to create a project from the template.

## Release Notes and To-Do List
You can find release notes for each version [here](https://github.com/Dotnet-Boxed/Templates/releases) and a To-Do list of new features and enhancements coming soon in the [projects](https://github.com/Dotnet-Boxed/Templates/projects) tab.

## Contributing

Please view the [Contributing](/.github/CONTRIBUTING.md) guide for more information.
