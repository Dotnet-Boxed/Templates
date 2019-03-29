![.NET Boxed Banner](https://raw.githubusercontent.com/Dotnet-Boxed/Templates/master/Images/Banner.png)

![Microsoft Orleans Boxed Preview Image](https://raw.githubusercontent.com/Dotnet-Boxed/Templates/master/Images/Orleans-Preview.png)

## Optional Feature Selection

The Microsoft Orleans project template uses `dotnet new` to enable you to turn features of the project template on or off. Literally everything can be turned on or off with the click of a button for a truly personalized project. Find out more about `dotnet new` [here](http://rehansaeed.com/custom-project-templates-using-dotnet-new/).

#### Project

- **Title** - The name of the project which determines the assembly product name.
- **Description** - A description of the project which determines the assembly description.
- **Author** - The name of the author of the project which determines the assembly author and copyright information.
- **TreatWarningsAsErrors** - Treat warnings as errors.

#### Analytics

- **Analytics** - Monitor internal information about how your application is running, as well as external user information.
  - **Application Insights** - Monitor internal information about how your application is running, as well as external user information using the Microsoft Azure cloud.
  - **None** (Default) - Not using any analytics.
- **ApplicationInsightsKey** - Your Application Insights instrumentation key e.g. 11111111-2222-3333-4444-555555555555.

## Always On Features

#### Logging

- **Serilog** - Has [Serilog](https://serilog.net/) built in for an excellent structured logging experience.

#### Dashboard

- **Dashboard** - A dashboard showing Grain statistics.

## How can I get it?

1. Install the latest [.NET Core SDK](https://dot.net).
2. Run `dotnet new --install "Boxed.Templates::*"` to install the project template.
3. Run `dotnet new orleans --help` to see how to select the feature of the project.
5. Run `dotnet new orleans --name "MyProject"` along with any other custom options to create a project from the template.

## Release Notes and To-Do List
You can find release notes for each version [here](https://github.com/Dotnet-Boxed/Templates/releases) and a To-Do list of new features and enhancements coming soon in the [projects](https://github.com/Dotnet-Boxed/Templates/projects) tab.

## Contributing

Please read the [guide](https://github.com/Dotnet-Boxed/Templates/blob/master/CONTRIBUTING.md) to learn how you can make a contribution.
