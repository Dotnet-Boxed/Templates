# TODO List

A list of improvements and new features to be added. Feel free to submit your own.

## General Improvements

Improvements that can be made to all project templates.

- Make use of Object Pooling code. Add note about using Object Pooling in ReadMe.html.
- Add [Google Structured Data](https://developers.google.com/structured-data/).

## ASP.NET 4.6 MVC 5

- Change Boilerplate.Web.Mvc5 to use an ASP.NET Core class library project to build the NuGet package.

## ASP.NET Core MVC 6

- Enable HTTPS in development environment by building a version of RequireHttpsAttribute which supports port numbers using [this](https://github.com/aspnet/Mvc/pull/4113).
- Add [Subresource Integrity](https://scotthelme.co.uk/subresource-integrity/) to scripts provided by Microsoft's CDN when they add the Access-Control-Allow-Origin HTTP header.
- Build a localization feature (See [docs](https://docs.asp.net/en/1.0.0-rc2/fundamentals/localization.html)).
- Add a CORS feature.
- Add an option to remove Font-Awesome.
- Add an option to choose the web-server you are using IIS, Nginx etc.
- Add an option to add a Docker file.
- If Bootstrap 4 includes LESS support, add @ChrisOMetz's [pull request](https://github.com/RehanSaeed/ASP.NET-MVC-Boilerplate/pulls).
- Add an example form controller.
- Use JIL for JSON serialization (See [issue](https://github.com/RehanSaeed/ASP.NET-MVC-Boilerplate/issues/72)).
- Improve social tag helpers to include child elments for images and other objects (See [this](https://channel9.msdn.com/Series/aspnetmonsters/Episode-19-Building-Advanced-Tag-Helpers?ocid=player)).

## ASP.NET Core MVC 6 API

- Update project template and release it.
- Use [HAL](https://github.com/mikekelly/hal_specification/blob/master/hal_specification.md) or [SIREN](https://github.com/kevinswiber/siren). See also [this](http://phlyrestfully.readthedocs.org/en/latest/halprimer.html) and [this](https://msdn.microsoft.com/en-us/magazine/jj883957.aspx) and [this](https://github.com/JakeGinnivan/WebApi.Hal).
- Add Produces attributes.
- Create a version of [UseDeveloperExceptionPage](https://github.com/aspnet/Diagnostics/blob/0a444088c9a7c5c6b4073c92104b48af734ef523/src/Microsoft.AspNetCore.Diagnostics/DeveloperExceptionPage/DeveloperExceptionPageExtensions.cs)

## Awaiting Third Parties

The following features require third parties to update their stuff.

### Microsoft

Wait for Microsoft to finish MVC 6 before adding these features.

- CacheProfile.VaryByParam in Startup.CacheProfiles.cs
- System.ServiceModel.SyndicationFeed does not exist on .NET Core. See [this](https://github.com/dotnet/wcf/issues/76#issuecomment-111420491) GitHub issue.
- Use the [RequireHttps middle-ware](https://github.com/aspnet/BasicMiddleware/issues/31) instead of a filter.
- Use the [HTTP Compression middle-ware](https://github.com/aspnet/BasicMiddleware/issues/34) instead of IIS compression.
