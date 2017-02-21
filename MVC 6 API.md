# ![ASP.NET MVC Boilerplate Logo](https://raw.githubusercontent.com/RehanSaeed/ASP.NET-MVC-Boilerplate/master/Images/Nuget Icon.png) [ASP.NET Core MVC 6 API Boilerplate](https://github.com/ASP-NET-MVC-Boilerplate/Templates)

![ASP.NET Core MVC 6 API Boilerplate Preview Image](https://raw.githubusercontent.com/ASP-NET-MVC-Boilerplate/Templates/master/Images/MVC%206%20API%20Preview%20Image.png)

## Technology Map

The ASP.NET Core MVC 6 API project template contains the following features:

![ASP.NET Core MVC 6 API Boilerplate Technology Map](https://raw.githubusercontent.com/ASP-NET-MVC-Boilerplate/Templates/master/Images/MVC%206%20API%20Technology%20Map.png)

## Feature Selection

The ASP.NET Core MVC 6 API project template uses `dotnet new` to enable you to turn features of the project template on or off. Literally everything can be turned on or off with the click of a button for a truly personalized project. Find out more about `dotnet new` [here](http://rehansaeed.com/custom-project-templates-using-dotnet-new/).

- **Title** - The name of the project which determines the assembly product name. If the Swagger feature is enabled, shows the title on the Swagger UI.
- **Description** - A description of the project which determines the assembly description. If the Swagger feature is enabled, shows the description on the Swagger UI.
- **Author** - The name of the author of the project which determines the assembly author and copyright information.
- **Swagger** (Default=On) - Swagger is a format for describing the endpoints in your API. Swashbuckle is used to generate a Swagger document and to generate beautiful API documentation, including a UI to explore and test operations, directly from your routes, controllers and models.
- **TargetFramework** - Decide which version of the .NET Framework to target.
  - **.NET Core** - Run cross platform (on Windows, Mac and Linux). The framework is made up of NuGet packages which can be shipped with the application so it is fully stand-alone.
  - **.NET Framework** - Gives you access to the full breadth of libraries available in .NET instead of the subset available in .NET Core but requires it to be pre-installed.
  - **Both** (Default) - Target both .NET Core and .NET Framework.
- **PrimaryWebServer** - The primary web server you want to use to host the site.
  - **Kestrel** (Default) - A web server for ASP.NET Core that is not intended to be internet facing as it has not been security tested. IIS or NGINX should be placed in front as reverse proxy web servers.
  - **WebListener** - A Windows only web server. It gives you the option to take advantage of Windows specific features, like Windows authentication, port sharing, HTTPS with SNI, HTTP/2 over TLS (Windows 10), direct file transmission, and response caching WebSockets (Windows 8).
- **ReverseProxyWebServer** - The internet facing reverse proxy web server you want to use in front of the primary web server to host the site.
  - **Internet Information Services (IIS)** - A flexible, secure and manageable Web server for hosting anything on the Web using Windows Server. Select this option if you are deploying your site to Azure web apps. IIS is preconfigured to set request limits for security.
  - **NGINX** - A free, open-source, cross-platform high-performance HTTP server and reverse proxy, as well as an IMAP/POP3 proxy server. It does have a Windows version but it's not very fast and IIS is better on that platform. If the HTTPS Everywhere feature is enabled, NGINX is pre-configured to enable the most secure TLS protocols and ciphers for security and to enable HTTP 2.0 and SSL stapling for performance.
  - **Both** (Default) - Support both reverse proxy web servers.
- **CloudProvider** - Select which cloud provider you are using if any, to add cloud specific features.
  - **Azure** - The Microsoft Azure cloud. Adds logging features that let you see logs in the Azure portal.
  - **None** (Default) - No cloud provider is being used.
- **Analytics** - Monitor internal information about how your application is running, as well as external user information.
  - **Application Insights** - Monitor internal information about how your application is running, as well as external user information using the Microsoft Azure cloud.
  - **None** (Default) - Not using any analytics.
- **ApplicationInsightsInstrumentationKey** - Your Application Insights instrumentation key e.g. 11111111-2222-3333-4444-555555555555.
- **HttpsEverywhere** (Default=On) - Use the HTTPS scheme and TLS security across the entire site, redirects HTTP to HTTPS and adds a Strict Transport Security (HSTS) HTTP header with preloading enabled. Configures the primary and reverse proxy web servers for best security and adds a development certificate file for use in your development environment.
- **PublicKeyPinning** (Default=Off) - Adds the Public-Key-Pins (HPKP) HTTP header to responses. It stops man-in-the-middle attacks by telling browsers exactly which TLS certificate you expect. You must have two TLS certificates for this to work, if you get this wrong you will have performed a denial of service attack on yourself.
- **CORS** (Default=On) - Browser security prevents a web page from making AJAX requests to another domain. This restriction is called the same-origin policy, and prevents a malicious site from reading sensitive data from another site. CORS is a W3C standard that allows a server to relax the same-origin policy. Using CORS, a server can explicitly allow some cross-origin requests while rejecting others.
- **XmlFormatter** - Choose whether to use the XML input/output formatter and which serializer to use.
  - **DataContractSerializer** - The default XML serializer you should use. Requires the use of [DataContract] and [DataMember] attributes.
  - **XmlSerializer** - The alternative XML serializer which is slower but gives more control. Uses the [XmlRoot], [XmlElement] and [XmlAttribute] attributes.
  - **None** (Default) - No XML formatter.
- **StatusController** (Default=On) - An endpoint that returns the status of this API and it's dependencies, giving an indication of it's health. This endpoint can be called by site monitoring tools which ping the site or by load balancers which can remove an instance of this API if it is not functioning correctly.
- **RequestId** (Default=Off) - Require that all requests send the X-Request-ID HTTP header containing a GUID. This is useful where you have access to the client and server logs and want to correlate a request and response between the two.
- **User-Agent** (Default=Off) - Require that all requests send the User-Agent HTTP header containing the application name and version of the caller.
- **robots.txt** (Default=On) - Adds a robots.txt file to tell search engines not to index this site.
- **humans.txt** (Default=On) - Adds a humans.txt file where you can tell the world who wrote the application. This file is a good place to thank your developers.

## Always On Features

- **GZip Compression** - Turned on by default for static and dynamic files making them smaller and quicker to download. 
- **Caching** - Both in-memory and distributed cache are configured. You do need to specify where the distributed cache stores it's data.
- **Response Caching** - Response caching is pre-configured, so you can use the [ResponseCache] attribute. Cache settings (cache profiles) are stored in the config file and referred to by name.
- **User Secrets** - This feature is turned on when the site is in development mode to allow storing of secrets on you development machine.
- **Format Filter** - The FormatFilterAttribute is turned on globally in development mode, so you can pass `?format=application/json` while debugging to set the Accept MIME type.
- **Validate Model State** - The ValidateModelStateAttribute is turned on globally so all models passed to an action method are automatically validated and a 400 Bad Request is returned if the model is invalid.
- **Automatically Return Not Acceptable** - Returns a 406 Not Acceptable if the MIME type in the Accept HTTP header is not valid.
- **AddMvcCore** - Uses only the features and packages from ASP.NET MVC required for an API. Uses ControllerBase instead of Controller.
- **Remove Server HTTP Header** - Removes the Server HTTP header for security and performance.
- **Browser Link** - Enables Browser Link in development mode which lets you hit Ctrl+Alt+Enter to refresh the browser.
- **Developer Exception Page** - Turns on the developer exception page in development mode to give you nice exceptions.
- **Named Routes** - Uses named attribute routes for best performance and maintainability. All route names are specified as constants.

## How can I get it?
That's easy, just choose one of the following options:

1. You must have the latest version of the dotnet tooling. This comes with Visual Studio 2017 or from [dot.net](https://dot.net).
2. Run `dotnet new --install Boilerplate.Templates::*` to install the project template.
3. Run `dotnet new bapi --help` to see how to select the feature of the project.
4. Run `dotnew new bapi --name "MyTemplate"` along with any other custom options to create a project from the template.

## Release Notes and To-Do List
You can find release notes for each version [here](https://github.com/ASP-NET-MVC-Boilerplate/Templates/blob/master/RELEASE%20NOTES.md) and a To-Do list of new features and enhancements coming soon in the [projects](https://github.com/ASP-NET-MVC-Boilerplate/Templates/projects) tab.

## Bugs and Issues

Please report any bugs or issues on the GitHub issues page [here](https://github.com/ASP-NET-MVC-Boilerplate/Templates/issues).
