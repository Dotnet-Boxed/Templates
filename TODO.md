# TODO List

A list of improvements and new features to be added. Feel free to submit your own.

## General Improvements

Improvements that can be made to all project templates.

- Make use of Object Pooling code. Add note about using Object Pooling in ReadMe.html.
- Add [Google Structured Data](https://developers.google.com/structured-data/).

## ASP.NET 4.6 MVC 5

- Change Boilerplate.Web.Mvc5 to use an ASP.NET 5 class library project to build the NuGet package.

## ASP.NET Core MVC 6

- Create a VS Extensions IWizard to fix the file path pointing to the artifacts folder in the .xproj file if it is incorrect. This can happen when someone creates a project that is not two folders away from the solution.
- Enable HTTPS in development environment by building a version of RequireHttpsAttribute which supports port numbers.
- Develop and add [Subresource Integrity](https://scotthelme.co.uk/subresource-integrity/).
- Automatically run Mocha JavaScript tests.
- Build a HttpException for MVC 6. See [this](http://stackoverflow.com/questions/31054012/asp-net-5-mvc-6-equivalent-of-httpexception) StackOverflow question.
- Build a localization feature.
- Add a CORS feature.
- Add an option to choose the web-server you are using IIS, Nginx etc.
- If Bootstrap 4 includes LESS support, add @ChrisOMetz's [pull request](https://github.com/RehanSaeed/ASP.NET-MVC-Boilerplate/pulls).

## ASP.NET Core MVC 6 API

- Update project template and release it.
- Use [HAL](https://github.com/mikekelly/hal_specification/blob/master/hal_specification.md) or [SIREN](https://github.com/kevinswiber/siren). See also [this](http://phlyrestfully.readthedocs.org/en/latest/halprimer.html) and [this](https://msdn.microsoft.com/en-us/magazine/jj883957.aspx) and [this](https://github.com/JakeGinnivan/WebApi.Hal).
- Add Produces attributes.

## Awaiting Third Parties

The following features require third parties to update their stuff.

### Microsoft

Wait for Microsoft to finish MVC 6 before adding these features.

- CacheProfile.VaryByParam in Startup.CacheProfiles.cs
- System.ServiceModel.SyndicationFeed does not exist on .NET Core. See [this](https://github.com/dotnet/wcf/issues/76#issuecomment-111420491) GitHub issue.
