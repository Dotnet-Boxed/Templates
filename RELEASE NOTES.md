Project templates come in two delivery methods, each contains different project templates:

- [dotnet new Boilerplate.Templates NuGet Package](#user-content-dotnet-new-boilerplatetemplates-nuget-package)
  - [ASP.NET Core API Boilerplate](https://github.com/ASP-NET-Core-Boilerplate/Templates/blob/master/MVC%206%20API.md)
- [Visual Studio Extension (VSIX)](#user-content-visual-studio-extension-vsix)
  - [ASP.NET MVC 5 Boilerplate](https://github.com/ASP-NET-Core-Boilerplate/Templates/blob/master/MVC%205.md)
  - [ASP.NET Core MVC Boilerplate](https://github.com/ASP-NET-Core-Boilerplate/Templates/blob/master/MVC%206.md)

# dotnet new Boilerplate.Templates NuGet Package

### 1.6.2
ASP.NET Core API Boilerplate
- Move ILoggerFactory configuration in Startup to constructor.
- Use C# 7 expression body members.
- Use IStartup interface on the Startup class.
- Added note in ReadMe about registering for [Certificate Transparency](https://scotthelme.co.uk/revocation-is-broken/) notifications.
ASP.NET Core Static Files Boilerplate
- Initial version.

### 1.6.1
ASP.NET Core API Boilerplate
- Use AppendCommaSeparatedValues and HeaderNames constants.

### 1.6.0
ASP.NET Core API Boilerplate
- Added Prefix.io profiler option.
- Upgraded NWebSec from 1.0.0 to 1.1.0.
- Run health checks in parallel in StatusController.
- Add Last-Modified and If-Modified-Since HTTP headers to return 304 Not Modified if a car has not changed.
- Response caching is now an optional feature.
- Upgraded all NuGet packages.

### 1.5.0
ASP.NET Core API Boilerplate
- Added API Versioning support for Swagger.

### 1.4.0
ASP.NET Core API Boilerplate
- Added OPTIONS endpoint.
- Added HEAD endpoints to all GET endpoints.
- Update Swashbuckle.AspNetCore to 1.0.0-rc3
- Use AddMvcOptions instead the delegate in AddMvcCore.

### 1.3.0
ASP.NET Core API Boilerplate
- Added Versioning feature.
- Add Link HTTP Header to GET /cars to show next, previous, first, last page URL.

### 1.2.0
ASP.NET Core API Boilerplate
- Upgraded to VS 2017 csproj format.
- NWebSec upgraded to 1.0.0.
- Disable the Application Request Routing (AAR) in web.config if using Azure.
- Move to new `AddUserSecrets<T>()` syntax in Startup.cs.

### 1.1.2
ASP.NET Core API Boilerplate
- Fixed the CORS feature which had duplicate code. 
- Refactoring ConfigureServices to use extension methods for everything.
- Added more comments.

### 1.1.1
ASP.NET Core API Boilerplate
- Fixed Swagger feature.
- Removed conditional compilation symbols from created project.

### 1.0.0
ASP.NET Core API Boilerplate
- Initial version.



# Visual Studio Extension (VSIX)

# 6.2.0
ASP.NET Core MVC Boilerplate
- Move ILoggerFactory configuration in Startup to constructor.
- Use C# 7 expression body members.
- Use IStartup interface on the Startup class.
- Added note in ReadMe about registering for [Certificate Transparency](https://scotthelme.co.uk/revocation-is-broken/) notifications.

# 6.1.0
ASP.NET Core MVC Boilerplate
- Use IOptionsSnapshot instead of IOptions
- Removed #region's.
- Upgraded NWebSec from 1.0.0 to 1.1.0.
- Upgrade packages.

# 6.0.0
ASP.NET Core MVC Boilerplate
- Upgraded to VS 2017 csproj format.
- NWebSec upgraded to 1.0.0.
- Disable the Application Request Routing (AAR) in web.config if using Azure.
- Move to new `AddUserSecrets<T>()` syntax in Startup.cs.
- 
# 5.1.1
ASP.NET Core MVC Boilerplate
- Update NuGet packages.

# 5.1.0
ASP.NET Core MVC Boilerplate
- Upgrade ASP.NET Core to 1.1.1.
- Upgrade Application Insights to 2.0.0.
- Set GZIP response compression to optimal in Startup.cs.
- Use self-signed pfx certificate in Development environment.
- Upgrade jquery-validation to 1.16.0 and use Subresource Integrity (SRI) for it.

# 5.0.0
ASP.NET Core MVC Boilerplate
- Upgraded to ASP.NET Core 1.1.
- Upgraded all NuGet packages.
- Removed experimental .cshtml minification feature.
- Added primary and reverse proxy web server features. Added Web Listener web server.
- Added response caching middleware.
- Added response compression middleware.
- Added cloud provider feature which adds logging integration into the Azure portal.
- Added current project as tag helper source to _ViewImports.cshtml so that View Components can be used as tag helpers.

# 4.7.0
ASP.NET Core MVC Boilerplate
- Add the Cache-Control HTTP header to the static files middleware and make it config driven.
- Removed bower.json, using package.json instead.
- Upgraded applicationinsights-js from 0.22.9 to 1.0.4.
- Upgraded Bootstrap from 3.3.6 to 3.3.7.
- Upgraded Font Awesome from 4.6.3 to 4.7.0.
- Upgraded jQuery from 2.2.3 to 3.1.1.
- Upgraded jquery-validation from 1.15.0 to 1.15.1.
- Upgraded mocha from 2.4.5 to 3.1.2.
- Upgraded sinon from 1.17.4 to 1.17.6.
- Updated several Gulp packages to the latest versions.
- Added the Typescript NPM package if TypeScript feature is used.
- Removed Subresource Integrity (SRI) from jquery-validation and jquery-validation-unobtrusive. The NPM package does
  not include a minified copy.
- Removed Modernizr.
- Use the new portable .pdb file format in project.json buildOptions.

# 4.6.0
ASP.NET MVC 5 Boilerplate & ASP.NET Core MVC Boilerplate
- Added item to ReadMe.html to use the Google CSP Evaluator to validate the sites CSP policy.
ASP.NET Core MVC Boilerplate
- Added a CORS feature.
- project.json publishOptions updated to use globs to copy cshtml files.
- launchSettings.json updated to add profiles to run the app using the Staging and Production environments.
- Add crossorigin="anonymous" attribute to external scripts supporting Access-Control-Allow-Origin: *.

# 4.5.0
ASP.NET Core MVC Boilerplate
- Added 'dotnet watch' launch profile to launchSettings.json, allowing you to edit code and refresh the browser to see your changes while
  the app is running.

# 4.4.0
ASP.NET Core MVC Boilerplate
- Added Web Server feature that lets you select which web server you want to target.
- nginx.conf added to configure the Nginx web server.
- UseRandomizedStringHashAlgorithm setting Moved from web.config to app.config.
- HttpExceptionMiddleware and InternalServerErrorOnExceptionMiddleware logs information messages.

# 4.3.0
ASP.NET Core MVC Boilerplate
- Get the environment in gulpfile.js by reading the launchSettings.json file if an environment variable does not exist.
- Google Page Speed is now an optional feature under performance.
- Set AddServerHeader to false for the Kestrel Web Server in Program.cs.

# 4.2.1
ASP.NET Core MVC Boilerplate
- Renamed the template in the File -> New Project window.

# 4.2.0
ASP.NET Core MVC Boilerplate
- Updated NWebSec to the latest ASP.NET Core 1.0 version.
- AddApplicationInsights in Startup.ConfigureServices now added fluently as it now returns IServiceCollection.

# 4.1.0
ASP.NET Core MVC Boilerplate
- Fixed https://github.com/ASP-NET-Core-Boilerplate/Templates/issues/108

# 4.0.0
ASP.NET Core MVC Boilerplate
- Updated to ASP.NET Core 1.0 RTM.

# 3.2.0
ASP.NET MVC 5 Boilerplate & 6
- Removed the X-UA-Compatible HTTP header from web.config.
ASP.NET Core MVC Boilerplate
- Development mode now uses HTTPS if HTTPS everywhere is enabled. In development mode a SSL port is used and needs to
  be set in the MVC options and in the Content-Security-Policy settings.
- Major refactoring of Startup.cs making all method calls fluent.
- CSP now configured using NWebSec middleware instead of NWebSec filters.
- Anti-forgery tokens use the X-XSRF-TOKEN HTTP header name.
- EnvironmentName.cs constant removed as the built in RC2 one with the same name now has a Staging constant in it which
  was missing.
- ReadMe.html updated to change comment about installing the HttpPlatformHandler and installing the IIS .NET Core
  Windows Server Hosting Bundle instead.
- Removed comments about IgnoreRoute missing from ASP.NET Core MVC Boilerplate.
- Added the autostrip-json-comments NPM package to package.json and require statement to gulpfile.js to allow comments
  in config.json and hosting.json.

# 3.1.1
ASP.NET Core MVC Boilerplate
- Fixed https://github.com/ASP-NET-Core-Boilerplate/Templates/issues/104

# 3.1.0
ASP.NET Core MVC Boilerplate
- Enable UseRandomizedStringHashAlgorithm for .NET Framework 4.6.1 in web.config.
- Dont need the 404 middleware in RC2, so it has been removed from the end of the pipeline in Startup.cs.

# 3.0.0
ASP.NET MVC 5 Boilerplate
- Took my name out of the AssemblyCopyright attribute in AssemblyInfo.cs and left it blank for users to fill in.
ASP.NET Core MVC Boilerplate
- Upgraded to ASP.NET Core RC2!!! Too many changes to list here.
- NuGet packages renamed:
    - Boilerplate.Web.Mvc NuGet package renamed to Boilerplate.AspNetCore.
    - Boilerplate.Web.Mvc.Razor NuGet package renamed to Boilerplate.AspNetCore.Razor.
    - Boilerplate.Web.Mvc.TagHelpers NuGet package renamed to Boilerplate.AspNetCore.TagHelpers.
- The AssemblyCopyright attribute in AssemblyInfo.cs is now filled in automatically with the current user and year.
- Switched from gulp-scsss-lint to gulp-sass-lint because it does not require Ruby to be installed.
- Added a Target Framework feature selector, so that you can choose from .NET Core, .NET Framework or both.
- Build Gulp tasks no longer clean as a pre-requisite.
- The name property in bower.json and package.json is now automatically populated based on the project name.
- BSON formatter removed until Microsoft adds support.
- The Glimpse feature has been hidden and turned off by default until it supports RC2.
- Pre-compiled views have been removed because Microsoft removed support in RC2.

# 2.18.1
ASP.NET Core MVC Boilerplate
- Removed some commented out code relating to Elmah and trace.axd which is not relevant in ASP.NET Core MVC Boilerplate.
- Added a '~' at the beginning of all URL's to local resources.

# 2.18.0
- Fixed SitemapGenerator bug https://github.com/ASP-NET-Core-Boilerplate/Templates/issues/96.
- Boilerplate.Web.Mvc NuGet packages updated for the above change.
ASP.NET MVC 5 Boilerplate
- jQuery updated NuGet package updated from 2.2.0 to 2.2.3. ContentDeliveryNetwork.cs and packages.config updated.
- jQuery.validate NuGet package updated from 1.14.0 to 1.15.0. ContentDeliveryNetwork.cs and packages.config updated.
- Newtonsoft.Json NuGet package updated from 8.0.2 to 8.0.3.
- Font Awesome NuGet package updated from 4.2.0 to 4.5.0.
ASP.NET Core MVC Boilerplate
- jQuery updated NuGet Bower updated from 2.2.2 to 2.2.3. ContentDeliveryNetwork.cs and bower.json updated.
- Font Awesome NuGet Bower updated from 4.6.1 to 4.6.3. ContentDeliveryNetwork.cs and bower.json updated.
- jQuery.validate.unobtrusive Bower package updated from 3.2.5 to 3.2.6. ContentDeliveryNetwork.cs and bower.json
updated.
- sinon Bower package updated from 1.17.3 to 1.17.4. ContentDeliveryNetwork.cs and bower.json updated.
- Updated several NPM packages in package.json.
- applicationinsights-js added to bower.json and application-insights.js removed from scripts. gulpfile.js changed to
use the source of the application insights js file.

# 2.17.0
- Extension now installs for a single user, rather than all users. This enables it to auto-update.
ASP.NET Core MVC Boilerplate
- RC2 ASPNET_ENVIRONMENT is now renamed to ASPNETCORE_ENVIRONMENT in gulpfile.js and Startup.cs.
- AddUserSecrets for configuration only done in Development for security in Startup.Configuration.cs.
- Changed comments for how to add/remove environment variables in Startup.Configuration.cs.

# 2.16.0
ASP.NET Core MVC Boilerplate
- Added HttpRequestExtensions IsLocalRequest extension method.
- Modified launchSettings.json so that the site starts on the same port when started from Kestrel or IIS Express.

# 2.15.0
ASP.NET Core MVC Boilerplate
- Boilerplate.Web.Mvc6 Updated:
    - Performance improvements.
    - Added ICommand and IAsyncCommand interfaces to help implement controller actions as separate classes.
    - Refactored the DistributedCacheExtensions.
    - Added ValidateModelStateAttribute to automatically validate ModelState.
- Boilerplate.Web.Mvc6.TagHelpers Updated:
    - Performance improvements.
- Fixed Missing jshint and jslint (See https://github.com/ASP-NET-Core-Boilerplate/Templates/issues/89).
- Updated JSON.NET reference.
- Updated font-awesome to 4.6.1.
- Updated jQuery to 2.2.2.
- Updated jQuery validate to 1.15.0.
- Updated jQuery validate npm package to 3.2.5.
- Updated gulp to 2.9.1.
- Updated gulp-csslint to 0.3.0.
- Updated gulp-cssnano to 2.1.1.
- Updated gulp-plumber to 1.1.0.
- Updated gulp-size to 2.1.0.
- Updated gulp-typescript to 2.13.0.
- Updated gulp-tslint to 4.3.5.
- Updated gulp-uglify to 1.5.3.
- Updated psi to 2.0.3.
- Updated rimraf to 2.5.2.
- Deleted tsconfig.html and moved comments to tsconfig.json as it now supports comments.

# 2.14.0
ASP.NET Core MVC Boilerplate
- Change error controller route to start at 400 to 599.

# 2.13.0
ASP.NET Core MVC Boilerplate
- Merged [Fixed problem with broken error handling on ReExecute](https://github.com/ASP-NET-Core-Boilerplate/Templates/pull/85).
- Merged [Fixes problem with returning BadGateway for StatusCodes below 200](https://github.com/ASP-NET-Core-Boilerplate/Templates/pull/87).

# 2.12.0
ASP.NET MVC 5 Boilerplate
- Add a note when debugging, always starts the site at the home page (See [this](https://github.com/ASP-NET-Core-Boilerplate/Templates/issues/84) issue).

# 2.11.0
ASP.NET MVC 5 Boilerplate
- Target Framework now respects the selected version in the new project dialogue.

# 2.10.0
ASP.NET MVC 5 Boilerplate
- Upgraded to .NET 4.5.2

# 2.9.9
ASP.NET Core MVC Boilerplate
- Removed trailing white space.
- Improved the Subresource Integrity tag helper (SRI) to generate the SRi from a local file.
- Changed gulpfile.js to only copy third party files instead of running them through the pipeline.
- Upgraded the tag helpers NuGet package.

# 2.9.8
ASP.NET MVC 5 Boilerplate
- Upgraded to TemplateBuilder NuGet package version 1.1.4.9-beta to fix a bug where using a '.'
in the name of a project causes the NuGet package hint paths to break.
ASP.NET Core MVC Boilerplate
- Packages file path in the .xproj is now corrected automatically with the update to the latest version of
TemplateBuilder and the new Wizard I added to it.

# 2.9.7
ASP.NET Core MVC Boilerplate
- All NuGet packages updated.
- Dropped TagHelpers NuGet package dependency on the base NuGet package.
- Refactored AtomActionResult to use IHostingEnvironment.
- Added RemoveServerHttpHeader extension method for IApplicationBuilder to the main NuGet package.
- Used the RemoveServerHttpHeader method in the Configure method in Startup.cs.
- Subresource Integrity tag helper added to TagHelpers NuGet package and _Layout.cshtml.
- URL's in ContentDeliveryNetwork updated to include the https: scheme to use with Subresource Integrity.
- Renamed ApplicationBuilderExtensions.UseBoilerplate to ApplicationBuilderExtensions.UseUrlHelperExtensions.
- Renamed ApplicationBuilderExtensions.RemoveServerHttpHeader to ApplicationBuilderExtensions.RunNotFound.
- Added UseHttpException, which allows the use of <see cref="HttpException"/> as an alternative method of returning an
error result.
- Added UseInternalServerErrorOnException, which returns a 500 Internal Server Error response when an unhandled
exception occurs.
- Renamed everything from ASP.NET 5 to ASP.NET Core.

# 2.9.6
ASP.NET MVC 5 Boilerplate
- Removed AntiXss package as it was causing a warning dialogue box to show up. It was not used and people can add it
themselves if they need it.
ASP.NET Core MVC Boilerplate
- Automatically run Mocha JavaScript tests using gulp-mocha-phantomjs.
- Added gulp-mocha-phantomjs to package.json.
- Added a test task to gulpfile.js.
- Renamed Test folder to Tests.

# 2.9.5
ASP.NET MVC 5 Boilerplate & 6
- Updated Newtonsoft.Json to 8.0.2.
- Updated jQuery to 2.2.0.
- Updated Bootstrap 3.3.6.
ASP.NET MVC 5 Boilerplate
- Switched from Twitter.Bootstrap.Less NuGet package to Bootstrap.Less.
ASP.NET Core MVC Boilerplate
- Use an absolute expiration instead of a sliding one for the sitemap, just in case people are dynamically generating
the sitemap.
- Updated Font Awesome to 4.5.0.
- Updated JavaScript Mocha, Chai and Sinon.
- Updated Gulp packages.
- Switched from deprecated gulp-minify-css to gulp-cssnano.
- Changed gulpfile.js to add a lintSources variable with defaults to skip the application-insights.js file.
- Add the new ASPNET_ENVIRONMENT variable in gulpfile.js. Leave the old ASPNET_ENV until RC2 upgrade.

# 2.9.4
ASP.NET Core MVC Boilerplate
- Fix issue where removing application insights caused a compile error in Startup.ContentSecurityPolicy.cs.

# 2.9.3
ASP.NET Core MVC Boilerplate
- Added new feature selection icons.
- Updated the ASP.NET Core MVC Boilerplate Technology Map image.
- Updated the ReadMe to uncomment the Glimpse TODO and make the social TODO's conditional.

# 2.9.2
ASP.NET MVC 5 Boilerplate & 6
- Added NoLowercaseQueryStringAttribute filter to allow URL's containing query strings with upper-case characters when
using the RedirectToCanonicalUrlAttribute. Added a note to use this attribute in the AccountController when using
ASP.NET Identity.
- Only redirect to the canonical URL for GET requests when using the RedirectToCanonicalUrlAttribute.
- Updated the Boilerplate NuGet packages to the latest versions.
- Upgraded to Newtonsoft.Json 8.0.1.
ASP.NET Core MVC Boilerplate
- Added the Microsoft.AspNet.Razor.Runtime.Precompilation NuGet package for Razor pre-compilation. Added the using
statement to RazorPreCompilation.cs.
- Added a note to use https://report-uri.io/ to report CSP violation reports.

# 2.9.1
ASP.NET Core MVC Boilerplate
- Removed Hammer.js.
- Fix Application Insights not registering exceptions in Startup.cs.

# 2.9.0
ASP.NET Core MVC Boilerplate
- Upgraded NWebSec to latest version and made feature visible but experimental.
- Fixed the manifest.json link having a trailing slash in _Layout.cshtml.

# 2.8.2
ASP.NET Core MVC Boilerplate
- Added NWebSec security through HTTP Headers to Startup.Filters and project.json. Turned off and hidden for now.
- Fixed the browserconfig.xml link having a trailing slash in _Layout.cshtml.
- Fixed the Bootstrap CSS not pointing to the correct folder for the fonts in site.scss.

# 2.8.1
ASP.NET Core MVC Boilerplate
- webroot setting moved from project.json to hosting.json.

# 2.8.0
ASP.NET MVC 5 Boilerplate & 6
- Added a note to ReadMe.html to turn on Azure SQL Database Threat Detection.
- Updated notes in ReadMe.html about where to get free SSL certificates.
ASP.NET Core MVC Boilerplate
- Upgraded to ASP.NET Core RC1 Update1.
- Added Glimpse feature, enabled by default.
- NuGet packages split into three, Boilerplate.Web.Mvc6, Boilerplate.Web.Mvc6.TagHelpers, Boilerplate.Web.Mvc6.Razor.
- _ViewImports.cshtml updated to pick up the Tag Helpers from the new location.
- Boilerplate.Web.Mvc6.dll is now signed.
- NPM and Gulp packages updated in package.json.
- Logging configuration added to config.json and Startup.Logging.cs.
- Added "use strict"; in gulpfile.js.
- Added main method to Startup.cs with a setting in project.json to use it.
- Removed Bootstrap Touch Carousel from _Layout.cshtml, Index.cshtml, fallback\scripts.js, gulpfile.js,
  ContentDeliveryNetwork.cs, bower.json.
- Updated Readme.html with links to RC1 instead of Beta 8.
- NuGet assemblies are now strong named like the Microsoft ones.
- gulpfile.js JSLint Fixes
- webroot setting removed from project.json (https://github.com/aspnet/Announcements/issues/94).
- ApplicationBasePath no longer needs to be set in Startup.Configuration.cs (https://github.com/aspnet/Announcements/issues/88).
- Move Autoprefixer before sass in gulpfile.js (https://github.com/ASP-NET-Core-Boilerplate/Templates/issues/44).

# 2.7.0
ASP.NET MVC 5 Boilerplate
- Added a note to ReadMe.html to enable the CSP upgrade-insecure-requests directive in Startup.cs.
ASP.NET Core MVC Boilerplate
- Added Application Insights Feature.
- SitemapService and SitemapPingerService track custom Application Insights events.
- SitemapPingerService now stores it's pinging locations in config.json using the SitemapSettings.
- SitemapPingerService now uses the hosting environment and only works in the production environment.

# 2.6.0
ASP.NET MVC 5 Boilerplate
- Added a commented out line for adding the Content-Security-Policy:upgrade-insecure-requests directive in Startup.cs.
- CSP Policy changed to add wss://localhost:* to the allowed connect-src list in debug mode to fix browser link.
ASP.NET Core MVC Boilerplate
- NPM packages updated to latest versions.
- JS-Hint feature added, making JS-Hint optional.
- NuGet package updated to add IDistributedCache extension methods.
- SitemapService changed to use the IDistributedCache instead of IMemoryCache.
- Added the RouteConvention to the NuGet package to enable global route prefixes. See http://www.strathweb.com/2015/10/global-route-prefixes-with-attribute-routing-in-asp-net-5-and-mvc-6/.

# 2.5.0
ASP.NET MVC 5 Boilerplate
- CacheService is now SingleInstance.
- Fixed missing image in Readme.html.
ASP.NET Core MVC Boilerplate
- Added a note to ReadMe.html to install the HttpPlatformHandler.
- Added a note for NPM file paths too long.
- Added UseCookiePolicy middleware to set better more secure cookie defaults.

# 2.4.0
ASP.NET MVC 5 Boilerplate
- Added a more prominent comment about removing settings from Web.config when using older versions of IIS.
ASP.NET Core MVC Boilerplate
- Updated to Beta 8.
- HTML Helper constructors removed, so that they can be used as TagHelper's too.
- Added a JavaScript Code Style (JSCS) feature to turn add/remove JSCS.
- Added Several features to add/remove favicons, manifest.json, browserconfig.xml and web app capability.
- Added a feature to add/remove search.
- Added a feature to add/remove Atom feed.
- Added a feature to add/remove Sitemap.
- Added a feature to add/remove Robots.txt.
- Fixed feature to add/remove Humans.txt which was not removing markup from Index.cshtml.
- The OpenSearchService now uses the AppSettings.SiteTitle for the description.
- SitemapService and SitemapPingerService changed to use ILogger<T>, rather than ILoggerFactory.
- Startup.Debugging.cs modified to only add the console logger if running in the console using the WebListener server.
- Razor pre-compilation updated for Beta 8 in RazorPreCompilation.cs and Startup.cs.

# 2.3.0
ASP.NET MVC 5 Boilerplate & 6
- Fixed a bug with the OpenGraphMedia type which was not handling HTTPS schemes correctly.
- Fixed a bug with the RedirectToCanonicalUrlAttribute which was not working if you turned off AppendTrailingSlash.
- Updated SitemapNode to check for null URL's and priorities out of range.
- SitemapService updated to enter the correct priorities for the sitemap nodes.
- Updated to use the latest version of the ASP.NET Core Boilerplate Framework NuGet package with the above changes.
- Numerous bug fixes and improvements to the Twitter and Open Graph HTML helpers.
- strict mode added to Fallback scripts.js and styles.js as well as other jshint fixes.
- Numerous bug fixes and improvements to the Twitter and Open Graph HTML helpers.
- ReadMe.html improved with icons and dismiss-able alerts.
ASP.NET MVC 5 Boilerplate
- CSP Filters changed in FilterConfig.cs so that Browser Link now works in debug mode and is not blocked by CSP.
ASP.NET Core MVC Boilerplate
- Added gulp-autoprefixer, gulp-jscs and gulp-plumber NPM packages.
- Implemented CSS autoprefixing in Gulpfile.js.
- Used gulp-plumber to handle errors in Gulpfile.js.
- Implemented JavaScript Code Style linting (JSCS) in Gulpfile.js.
- Added JSHint, TSHint and JSCS settings files.
- Switched from gulp-tsc to gulp-typescript.
- Added a TypeScript feature.
- The Twitter and Open Graph HTML helpers are now also tag helpers and can be used as such.
- The Twitter and Open Graph HTML helpers have been moved to the Boilerplate.Web.Mvc.TagHelpers namespace.
- RedirectToCanonicalUrlAttribute and NoTrailingSlashAttribute updated to use the new ASP.NET Core API's.
- The Twitter and Open Graph HTML helpers are now also tag helpers and can be used as such.
- The Twitter and Open Graph HTML helpers have been moved to the Boilerplate.Web.Mvc.TagHelpers namespace.

# 2.2.0
ASP.NET Core MVC Boilerplate
- Added the Feature Selection Wizard.
- Added Mocha JavaScript tests.
ASP.NET Core API Boilerplate
- Added the Feature Selection Wizard.
Feature Selection Wizard
- Added HTTPS Everywhere feature.
- Added Remove Development Server Port feature.
- Modified Formatter features.
- Features can now be invisible to the UI.
- Lots more improvements.

# 2.1.4
ASP.NET MVC 5 Boilerplate & 6
- site.js updated to use the revealing prototype pattern instead of the revealing module pattern.
- site.js updated with comments about this vs self.
- Updated the jQuery Validation NuGet/Bower package to 1.14.0.
- Updated the jQuery Validation CDN link in ContentDeliveryNetwork.cs to 1.14.0.
- Updated the Bootstrap NuGet/Bower package to 3.3.5.
- Updated the Bootstrap CDN link in ContentDeliveryNetwork.cs to 3.3.5.
ASP.NET MVC 5 Boilerplate
- Added the CSharpRazorViewEngine to Boilerplate.Web.Mvc5 NuGet package.
- Used the CSharpRazorViewEngine in Global.asax.cs instead of RazorViewEngine.
ASP.NET Core MVC Boilerplate
- Upgraded Boilerplate.Web.Mvc6 to Beta 7 with version 1.2.0.
- upgraded project template to Beta 7 and Boilerplate.Web.Mvc6 1.2.0.
- Updated ReadMe.html with information about how to upgrade to Beta 7.
- Added the Microsoft.Framework.Logging.Debug package and loggerFactory.AddDebug(); in the Development environment.
- Updated gulpfile.js to require project.json instead of using eval.
ASP.NET Core API Boilerplate (Coming Soon)
- Upgraded Boilerplate.Web.Mvc6 to Beta 7 with version 1.2.0.
- Upgraded project template to Beta 7 and Boilerplate.Web.Mvc6 1.2.0.
- Added BSON formatter.
Feature Selection Wizard (Coming Soon)
- More improvements

# 2.1.3
ASP.NET MVC 5 Boilerplate & 6
- The calculator in site.js now throws Error instead of a string.
ASP.NET Core MVC Boilerplate
- Updated the Boilerplate.Web.Mvc6 NuGet package to add support for BSON formatters.
- SitemapService cache settings moved to use the CacheProfileSettings and config.json. CacheSetting constant was
  deleted and a new constant was added to CacheProfileName.
Feature Selection Wizard (Coming Soon)
- Support for ASP.NET Core API Boilerplate added by including a new wizard and feature sets.
- Added a link to RehanSaeed.com
- Added a 'Give me the carefully chosen defaults' button label.
ASP.NET Core API Boilerplate (Coming Soon)
- First check-in of a Web API project. Lots more work to do.
- Copied some code from the ASP.NET Core MVC Boilerplate project.
- Added Swagger.
- Configured SideWaffle files.
- Added to VSIX but commented out.

# 2.1.2
ASP.NET MVC 5 Boilerplate & 6
- Updated site.js with better comments, fixed undefined bug, added a namespace and calculator example using the
  revealing module pattern with full comments.
- Fixed a missing var keyword in styles.js.
- using window.jQuery rather than $ in scripts.js.
Feature Selection Wizard (Coming Soon)
- More major enhancements and bug fixes.
- New unit test project with ProjectService tests.

# 2.1.1
ASP.NET Core MVC Boilerplate
- Move UseStaticFiles in Startup.cs so it is the first thing to execute in the pipeline for better performance.
- Startup.Options.cs Fixed comment.
- Startup.Routing.cs Cleaned up IgnoreRoute comments.
- First check-in of Feature Selection Wizard, not used yet but will allow you to select template features.

# 2.1.0
ASP.NET MVC 5 Boilerplate & 6
- browserconfig.xml is now generated dynamically, you no longer have to edit it and put in the Atom feed URL. You can
  also customize it with your own tile XML. See BrowserConfigService, HomeController and constants for changes.
- manifest.json is now generated dynamically, you no longer have to edit it and put in the site title. See
  ManifestService, HomeController and constants for changes.
- Updated ReadMe.html to remove a step to edit the browserconfig.xml file.
- Updated ReadMe.html to remove a step to edit the manifest.json file.
ASP.NET Core MVC Boilerplate
- Settings classes have been moved from Properties folder to Settings and placed under the Settings namespace.
- Site.less now imports bootstrap and bootstrap-touch-carousel.
- Removed gulp-recess as it does not support the latest version of LESS. Added gulp-csslint instead.

# 2.0.0
ASP.NET MVC 5 Boilerplate & 6
- Template now has a Preview Image.
- Default project name changed to WebApplication.
ASP.NET Core MVC Boilerplate
- Cache Profile settings are now stored in a CacheProfileSettings configuration section in config.json.
- Released!!!

# 1.2.9
ASP.NET MVC 5 Boilerplate
- Web.config Set <httpErrors existingResponse="PassThrough"> for the Debug version.
- Web.config Set <httpErrors existingResponse="Replace"> for the Release version.
- Added link to http://rehansaeed.com/dynamically-generating-robots-txt-using-asp-net-mvc/

# 1.2.8
ASP.NET MVC 5 Boilerplate
- SideWaffle fix: New GUID now generated for each project.
- Added a note to upgrade to .NET 4.6 in ReadMe.html.
- Added a link to webaim.org to check accessibility in ReadMe.html.
- Fix Site.less linting errors. Fixed order.
- Fix Site.js linting errors. Added missing semi-colons.
- Added CancellationToken to RSS Feed to allow cancellation of requests. See http://www.davepaquette.com/archive/2015/07/19/cancelling-long-running-queries-in-asp-net-mvc-and-web-api.aspx
ASP.NET Core MVC Boilerplate
- SideWaffle fix: New GUID now generated for each project.
- SideWaffle fix: Target VSIX project template at .NET 4.5.1.
- gulp-tsc (TypeScript) and gulp-if added to package.json.
- userSecretsId set in project.json.
- SideWaffle _preprocess.xml updated so a new userSecretsId GUID is generated for each project.
- gulpfile.js updated so that you can now start with CSS (.css) or LESS (.less) and compile to CSS.
- gulpfile.js updated so that you can now start with TypeScript (.ts) or JavaScript (.js) and compile to JavaScript.
- gulpfile.js updated with CSS, LESS, JavaScript and TypeScript linting tasks.
- gulpfile.js updated with a Google Page Speed performance test tasks.
- Fix Site.less linting errors. Fixed order.
- Fix Site.js linting errors. Added missing semi-colons.
- AddUserSecrets for all environments in Startup.Configuration.cs.
- UseAspNetMvcBoilerplate in ApplicationBuilderExtensions.cs renamed to UseBoilerplate.
- Added Startup.Caching.cs to configure IDistributedCache and IMemoryCache.
- Added Startup.Debugging.cs to configure browser link, console logging and other debugging tools.
- Added Startup.ErrorPages.cs to configure error pages.
- Added a note to upgrade to .NET 4.6 in ReadMe.html.
- Added a link to webaim.org to check accessibility in ReadMe.html.
- Added UseErrorPageTests to Boilerplate.Web.Mvc6 NuGet package to help test error pages.
- Added ASP.NET Core, npm, Gulp and Bower links in Index.cshtml.
- web.config added to wwwroot folder.
- IHostingEnvironment.IsDevelopment and IsProduction extension methods used where available.
- bower.html added to comment bower.json file. Comments are not allowed in the JSON file.
- package.html added to comment package.json file. Comments are not allowed in the JSON file.
- gen command removed from project.json.
- web command updated in project.json and split out into hosting.ini file.
- Added CancellationToken to RSS Feed to allow cancellation of requests. See http://www.davepaquette.com/archive/2015/07/19/cancelling-long-running-queries-in-asp-net-mvc-and-web-api.aspx

# 1.2.7
ASP.NET MVC 5 Boilerplate
- NoTrailingSlashAttribute updated to handle URL's with query strings.
- Boilerplate.Web.Mvc5 NuGet package updated to 1.0.17 containing the above change.
ASP.NET Core MVC Boilerplate
- NoTrailingSlashAttribute updated to handle URL's with query strings.
- Boilerplate.Web.Mvc6 NuGet package updated to 1.0.4 containing the above change.
- project.json keyFile path fixed.
- SideWaffle template settings updated in _project.cstemplate.xml.

# 1.2.6
ASP.NET MVC 5 Boilerplate
- Added trailing slashes to the URL's under httpErrors.
- RedirectToCanonicalUrlAttribute fixed to handle URL's with query strings and removed the ignore controllers parameter
  due to the fix above and this one it is no longer needed.
- Ignored controllers for the above fix removed from RedirectToCanonicalUrlAttribute in FilterConfig.cs.
- Upgraded the Boilerplate.Web.Mvc5 NuGet package to 1.0.15 which includes the above changes.
- Elmah constant removed from ControllerName.cs as it was no longer needed.
- ReadMe.html updated with a comment about Web.config settings not existing when using older versions of IIS 7.5 and 8.
ASP.NET Core MVC Boilerplate
- Turn off Razor View Pre-Compilation when compiling using the DEBUG solution configuration.
- NoCacheAttribute added to Boilerplate.Web.Mvc6 NuGet package.
- RedirectToCanonicalUrlAttribute fixed to handle URL's with query strings and removed the ignore controllers parameter
  because it is not needed.
- Ignored controllers for the above fix removed from RedirectToCanonicalUrlAttribute in FilterConfig.cs.
- Upgraded the Boilerplate.Web.Mvc6 NuGet package to 1.0.15 which includes the above changes.
- Elmah constant removed from ControllerName.cs as it was no longer needed.
- ReadMe.html updated with a comment about Web.config settings not existing when using older versions of IIS 7.5 and 8.

# 1.2.5
- No changes. The release messed up somehow.

# 1.2.4
- No changes. The release messed up somehow.

# 1.2.3
ASP.NET MVC 5 Boilerplate
- favicon.ico moved to root of site.
- Icons rearranged in _Layout.cshtml to reflect RealFaviconGenerator.com and favicon.ico meta tag removed.
- Added an empty IIFE and DOM ready event handler to site.js with comments about good practice.
- Added manifest.json and browserconfig.xml links to Home.cshtml.
ASP.NET Core MVC Boilerplate
- favicon.ico moved to root of site.
- browserconfig.xml moved to root of site.
- Icons rearranged in _Layout.cshtml to reflect RealFaviconGenerator.com. favicon.ico and browserconfig.xml meta tags
  removed.
- Updated Site.less with basic styles for the site.
- Added an empty IIFE and DOM ready event handler to site.js with comments about good practice.
- Added manifest.json and browserconfig.xml links to Home.cshtml.

# 1.2.2
ASP.NET MVC 5 Boilerplate
- maxcdn.bootstrapcdn.com added to font-src CSP policy directive to enable font awesome to work.
- fallback/font-awesome.js made generic so it can handle multiple fallback stylesheets.
    - font-awesome.js renamed to styles.js
    - BundleConfig.cs updated to reflect the above change.
    - styles.js can now handle multiple fallbacks but only contains font-awesome for now.
- Added comments to fallback/scripts.js and renamed a few variables. Nothing major.
ASP.NET Core MVC Boilerplate
- Fallback stuff copied here also.
- Environments addedto gulpfile.js.
- Watches fixed in gulpfile.js.

# 1.2.1
ASP.NET MVC 5 Boilerplate
- Deleted all failover scripts and added Scripts\fallback\font-awesome.js and Scripts\fallback\scripts.js which handle
  failover much better.
- Added a blank site.js script.
- Changed the 'failover' bundle in BundleConfig.cs to 'site' and included the new fallback scripts and the new site.js
  script.
- Changed FilterConfig.cs because using MaxCDN was causing a CSP error. MaxCDN needed to be added to the styles
  directive instead of the scripts directive.
- Updated _Layout.cshtml with the new site bundle and with a new meta tag to help with the font awesome fallback.
- Updated _Layout.cshtml with a new site title icon in the toolbar.

# 1.2.0
ASP.NET MVC 5 Boilerplate
- Renamed ContentDeliveryNetwork.MaxCdn.FontAwesome to add Url at the end.
- Removed 'Learn More' buttons from Index.cshtml.
- Added 'This App Contains' section to Index.cshtml.
- HSTS maxage changed to 18 weeks and subdomains included to fulfil preload criteria.
- HKPG maxage changed to match HSTS.
- Changed SitemapService to return null if the index is out of range instead of throwing an exception.
- Changed HomeController to return a BadRequest response if the sitemap XML is null.
- Update Boilerplate.Web.Mvc5 to 1.0.14 which is just a name change to the NuGet package to 'ASP.NET MVC 5 Boilerplate' instead of just 'MVC'.
- Changed Atom Feed links in FeedService.cs and HomeController.cs to rehansaeed.com.
- Updated FeedService with commented out code explaining how to add Atom feed paging.
ASP.NET Core MVC Boilerplate
- Gulp, Bower packages and build.
- About, Contact, Index, _Layout and _GlobalImports modified.
- FeedService added but only if you use full .NET and not DotNetCore.
- Changed SitemapService to return null if the index is out of range instead of throwing an exception.
- Changed HomeController to return a BadRequest response if the sitemap XML is null.
- Lots more...

# 1.1.9
ASP.NET MVC 5 Boilerplate
- Dropped support for IE 8 by removing the Respond.js library. IE 8 is used by 2.19% of global users, mostly in less
  developed countries and governments/businesses too lazy to upgrade from Windows XP.
    - Removed Respond.js NuGet package.
    - Removed Respond.js script bundle.
    - Removed Respond.js fail-over script.
    - Removed Respond.js from _Layout.cshtml.
- Added Font Awesome CDN from MaxCDN.com.

# 1.1.8
ASP.NET MVC 5 Boilerplate
- Updated NuGet packages:
    - Boilerplate.Web.Mvc5 updated to 1.0.12 - Added referrer meta tag.
- Added referrer meta tag to _Layout.cshtml to control privacy and security of the HTTP referrer header.
- Set the referrer mode and author in Index.cshtml, About.cshtml and Contact.cshtml.
- Added further comments and links about HPKP in Startup.cs to help set it up.
- HPKP no longer includes subdomains by default in Startup.cs.
- HSTS no longer includes subdomains by default in Startup.cs.
- Sample Copyright text fixed in FeedService.cs.

# 1.1.7
ASP.NET MVC 5 Boilerplate
- Updated NuGet packages:
    - Boilerplate.Web.Mvc5 updated to 1.0.10 - Assembly is now signed.
    - Newtonsoft.Json updated to 7.0.1
    - NWebsec updated to 4.1.1
    - NWebsec.Core updated to 1.4.1
    - NWebsec.Mvc updated to 4.1.1 - Web.config updated with new assembly version number.
    - NWebsec.Owin updated to 2.1.1
- Added comment about the NWebSec CspPluginTypesAttribute in FilterConfig.cs.
- Added comment about implementing HPKP (Public Key Pinning) to Startup.cs and ReadMe.html.
ASP.NET Core MVC Boilerplate
- Various Updates

# 1.1.6
- Minor formatting, line length and spelling fixes to _Layout.cshtml.

# 1.1.5
- Thanks to Kevin P. Rice for discovering that the 192x192 icon has to come before the 96x96, 32x32 and 16x16 icons due
  to a FireFox bug.

# 1.1.4
- RehanSaeed.co.uk moved to RehanSaeed.com. All comments and links updated to reflect this.

# 1.1.3
ASP.NET MVC 5 Boilerplate
- FeedService updated to use SyndicationContent.CreatePlaintextContent static method rather than creating a new
  TextSyndicationContent.
- Added comments and links to anti-forgery token configuration in Global.asax.cs.
- Fixed spelling mistakes in Web.config.
ASP.NET Core MVC Boilerplate
- Boilerplate.Web.Mvc6 NuGet package updated
    - Filters ported from ASP.NET MVC 5 Boilerplate. RedirectToHttpsAttribute not required anymore. Nothing tested yet.
    - HttpRequestExtensions added for the IsAjaxRequest extension method.
    - UrlHelperExtensions ported.
- Razor pre-compilation turned on in RazorPreCompilation.cs. Need to find a way to turn it off during development.
- Constants ported from ASP.NET MVC 5 Boilerplate.
- EnvironmentName constants added for Development, Staging and Production environments in ASP.NET Core MVC Boilerplate.
- ErrorController added. Broken still.
- HomeController modified to use attribute routing.
- Namespaces added to _GlobalImports.cshtml.
- SiteTitle set in config.json.
- Microsoft.AspNet.Mvc.Xml and System.Runtime Nuget packages added. Boilerplate.Web.Mvc6 added as a project reference
  until the NuGet package is published.
- Comments added to gulpfile.cs.
- Startup configured
    - Anti-forgery tokens configured.
    - All commented.
    - Configure MVC
    - Configure View Engines
    - Configure Routing and lower-case URL's.
    - Add UseRuntimeInfoPage.

# 1.1.2
- Implemented PubSubHubbub protocol to publish changes in the Atom feed to subscribers. See IFeedServices.cs and FeedService.cs.
- Added a note to ReadMe.html to call IFeedServices.PublishUpdate to publish PubSubHubbub updates to subscribers.
- ErrorModel.cs deleted. Although it was being passed to the error views, we did not use it.
- Lines now wrap at 120 characters.
- Ran spell-check and fixed spelling errors in several files.

# 1.1.1
- Updated ReadMe.html
    - New SSL/TLS instructions. Explaining that SSL is vulnerable to the POODLE attack and should not be used.
    - New Authentication section explaining why ASP.NET Core does not provide authentication out of the box and how to add it.
    - New step to update the browserconfig.xml file with the URL to the sites RSS feed.
- Updated _Layout.cshtml with comment, telling you to keep title's less than 70 characters.
- Updated Index.cshtml with comments about Title, Description and Author.
- Updated browserconfig.xml with details about the site RSS feed.
- Updated Boilerplate.Web.Mvc NuGet package to 1.0.9.
    - Added ValidateHeaderAntiForgeryTokenAttribute which can validate anti-forgery tokens for Ajax posts by checking the HTTP headers instead of the form inputs.
    - Updated comment in RedirectToCanonicalUrlAttribute with link from Bing, explaining why 301 redirects are better than canonical URL's.

# 1.1.0
- Comment about Anti-Forgery Tokens and the name of the corresponding form input.
- Set MvcBuildViews to true in the .csproj project file when in release mode. This builds the
  .cshtml ASP.NET Core views, so we get compile time errors instead of runtime errors.
  We do this only in release mode because this stops edit and refresh from working.
- Added new render section to _Layout.cshtml for meta tags in the head of the HTML.
- Removed all classes under the Framework namespace and moved them to a separate NuGet package called Boilerplate.Web.Mvc5
  this should make it easy for people to upgrade their projects to the latest framework and we can include bug fixes and new
  features easily. Tp achieve this a few changes had to be made:
    - RedirectToCanonicalUrlAttribute now has an extra constructor parameter, set in FilterConfig.cs which tells it which controller actions to ignore.
    - ContentType constant also moved to Framework.
    - SitemapService now inherits from SitemapGenerator which exists in the Framework. SitemapService is now much more simplified.
    - Boilerplate.Web.Mvc5 NuGet package added.
- jQuery NuGet package updated to 2.1.4.
- jQuery Google CDN URL updated to 2.1.4.
- Twitter Card and Open Graph and Facebook meta tag helpers added to the Framework (Now exists in the Boilerplate.Web.Mvc5 NuGet package).
    - Twitter card and Open Graph meta tags added to _Layout.cshtml file allowing us to add them using the ViewBag.
    - Twitter card and Open Graph meta tags added to the Home.cshtml, About.cshtml and Contact.cshtml files using the new OpenGraph helper.
    - open-graph-1200x630.png file added to be used in the Open Graph meta tags and displayed when someone shares the site on Facebook.

# 1.0.45
- Added Elmah Controller Name to ControllerName.cs
- Ignore requests for the ErrorController in the RedirectToCanonicalUrlAttribute.cs.
- Updated Modernizr bundle to use CDN in BundleConfig.cs and updated comments.
- Lots of files changed for StyleCop compliance, mostly fixing spelling mistakes and comments.

# 1.0.44
- Elmah.Mvc NuGet package updated from 2.1.1 to 2.1.2.
- Autofac.Mvc5 NuGet package updated from 3.3.3 to 3.3.4.
- New Elmah.Mvc app setting added to Web.config file called elmah.mvc.UserAuthCaseSensitive.
- New CacheService.cs and ICacheService.cs added, which is a wrapper around the System.Runtime.Caching.MemoryCache.
- ICacheService registered with Autofac in Startup.Container.cs.
- SitemapService now supports sitemap index files, which are used when you have a large number of nodes in your sitemap. See SitemapService.cs for more details.
    - ISitemapService.cs updated with new index parameter and comments.
    - SitemapService.cs totally updated and improved to support sitemap indexes.
    - SitemapService now uses ICacheService to cache sitemap nodes internally rather than relying on OutputCache.
    - HomeController updated with new index parameter which it passes to the SiteMapService. New comments and removed OutputCache.
    - Removed sitemap caching settings from Web.config.
    - Removed sitemap caching profile name from CacheProfileName.cs
- Added XDocumentExtensions.cs to convert XDocument to String but with the ability to specify any Encoding.
- Refactored OpenSearchService and SitemapService to use XDocumentExtensions instead of containing duplicated code.
- SitemapPingerService updated to remove async keyword in Debug mode, to get around a compiler warning about using the async keyword without awaiting anything.
- FilterConfig.cs changes courtesy of Omid:
    - Changed example.com to *.example.com.
    - Used a string.Join rather than string.Format to create the custom sources for CDN's.

# 1.0.43
- Glimpse path moved back to /glimpse.axd as the path was no longer working due to a Elmah bug workaround.

# 1.0.42
- Added the FriendlyUrlHelper to help create SEO and human friendly URL's.
- Added a note to ReadMe.html to use the FriendlyUrlHelper to create SEO and human friendly URL's.
- Removed IgnoreRoute statements for Elmah and Glimpse. It turns out you don't need these.

# 1.0.41
- RedirectToHttpsAttribute set to Compile, rather than Content.

# 1.0.40
- Corrected thumbnail size in FeedService.cs.

# 1.0.39
- Fixed the 'Learn More' link to ASP.NET Core Boilerplate in Index.cshtml.
- Added a note about session state in Web.config file.

# 1.0.38
- Added /content/icons/atom-icon-48x48.png and /content/icons/atom-icon-96x48.png for Atom 1.0 feed images.
- Changed the images referred to in FeedService.cs to point to the above images.
- Added in a default MapRoute in RouteConfig to work around a Elmah/Elmah.MVC bug.
- enablePrefetchOptimization set to true in Web.config for better startup performance of the site.
- Note added to ReadMe.html about enabling the prefetcher in Windows Server for the enablePrefetchOptimization setting to work.
- Added comments in Web.config for the <compilation/> element.
- Added <requestLimits maxAllowedContentLength="1048576" maxQueryString="2048" maxUrl="4096" /> to the Web.config as well as comments.
- Added similar settings to the <httpRuntime element in the Web.config with comments.
- Added a note to ReadMe.html to edit the requestLimits and httpRuntime settings for better security.

# 1.0.37
- Added a note to ReadMe.html to review the ASP.NET Core Boilerplate project template.
- Ignore the Elmah pages in RedirectToCanonicalUrlAttribute.

# 1.0.36
- Fixed the RedirectToHttpsAttribute class not being recognized in the project, thanks to @dls314159 for highlighting this issue.
- Added two more IgnoreRoute statements to support Elmah, copied from Elmah.MVC (This should be part of Elmah.MVC already, have submitted a pull request to Elmah.MVC. If they accept it, we can remove this code altogether).
- Added SitemapPingerService.cs and ISitemapPingerService.cs which sends the URL for the sitemap.xml file to Google, Microsoft and Yahoo.
- Registered the ISitemapPingerService with Autofac in Startup.Container.cs.
- Added a note in ReadMe.html to call the SitemapPingerService's PingSearchEngines method when your sitemap changes.

# 1.0.35
- Atom feed now uses absolute URL's instead of the BaseUri. This is because Firefox has a bug and cannot handle relative URL's!!! Updated FeedService.cs.
- Moved some code around in FeedService.cs to make it easier to read. Uncommented the media enclosure link.
- Added self URL link to Atom feed in FeedService.cs.
- Added Yahoo Media Atom/RSS extensions to SyndicationFeedExtensions.cs.
- Added Yahoo Media thumbnails to Atom feed entries in FeedService.cs.
- Added a few content types to ContentType.cs and added some comments.

# 1.0.34
- Added a AbsoluteContent extension method to UrlHelperExtensions.cs. This gives us an absolute URL to static content.
- Used AbsoluteContent in OpenSearchService.cs to link to icons.
- ReadMe.html now has check boxes next to each step which remembers if it was checked or not.
- Added lots of new information into ReadMe.html from http://webdevchecklist.com/.
- Added Atom 1.0 feed.
    - IFeedService.cs added.
    - FeedService.cs added.
    - AtomActionResult.cs added to return Atom feeds.
    - SyndicationFeedExtensions added to add icon element to System.ServiceModel.SyndicationFeed.
    - FeedService registered with Autofac in Startup.Container.cs.
    - Feed action added to HomeController.cs.
    - Feed action is cached. Cache settings added to web.config and CacheProfileName.cs.
    - Atom feed link tag added to _Layout.cshtml.
    - RSS/Atom section added to ReadMe.html
- Atom, Png, and Jpg ContentType's added to ContentType.cs.

# 1.0.33
- Added RequirePermanentHttpsAttribute filter which is different from System.Web.Mvc.RequireHttpsAttribute because it does a 301 Permament redirect instead of a 302 temporary redirect.
- Changed commented out code in FilterConfig.cs to use RequirePermanentHttpsAttribute instead of RequireHttpsAttribute.
- Added RedirectToCanonicalUrlAttribute filter which redirects requests without lowercase characters or trailing slashes to a valid one for better SEO.
- Added NoTrailingSlashAttribute to disable the RedirectToCanonicalUrlAttribute for routes like /robots.txt.
- Added RedirectToCanonicalUrlAttribute to the global filters in FilterConfig.cs.
- Added NoTrailingSlashAttribute to the opensearch.xml, robots.txt and sitemap.xml actions in HomeController.cs.
- Added a note to ReadMe.html to edit the RobotsService.cs file to modify the dynamically generated robots.txt file.
- Added a note to ReadMe.html to use a domain names 301 redirect service to redirect multiple domain names to a single one for better SEO.
- Added a note in ReadMe.html to use Microsoft the SEO Toolkit to test your site.
- Added RedirectToHttpsAttribute to enable 301 Permanent redirect to HTTPS pages rather than 302 temporary redirects that the System.Web.MVC.RequireHttpsAttribute attribute does.
- Replaced the commented out RequireHttpsAttribute with RedirectToHttpsAttribute(true) in FilterConfig.cs.
- Expanded the comment in _Layout.cshtml about the canonical URL.
- Expanded the comment in RouteConfig.cs about trailing slashes and how Google treats them.
- Added new 405 Method Not Allowed error page, used by RequireHttpsAttribute.

# 1.0.32
- Added a comment in RobotsService.cs not to disallow scripts or images.
- Added a disallow in RobotsService.cs so that the robots.txt stops robots from indexing the error pages.
- Added About and Contact pages to sitemap.xml in SitemapService.cs
- Added a note to the Forbidden action in ErrorController.cs that "Unlike a 401 Unauthorized response, authenticating will make no difference.".

# 1.0.31
- Created IOpenSearchService.cs and OpenSearchService.cs and moved code in HomeController.cs there.
- Created IRobotsTextService.cs and RobotsTextService.cs and moved code in HomeController.cs there.
- Added .TrimEnd('/') to site map URL in robots.txt. See RobotsTextService.cs.
- Registered the new services above in Startup.Container.cs and injected them into HomeController.cs.
- Updated the comments for the ISitemapService.cs, SitemapService.cs and HomeController actions.
- Added Pre-requisites section to ReadMe.html telling you to update Visual Studio, NuGet extension and install the Web Essentials extension.
- Added Forbidden error page to ErrorController and Forbidden.cshtml view. Cache settings added to web.config.
- Added 403 Forbidden error setting to httpErrors section in web.config.
- Added commented out information about Crawl-delay to RobotsService.cs.
- Modified the .csproj file so that the IISUrl element is empty. This allows a random port to be chosen each time a project is generated from the template.

# 1.0.30
- Set elmah.mvc.disableHandleErrorFilter app setting in web.config to true.
- 500 errors now route to error/internalservererror because there is now a error folder which conflicts with the /error route.
- Added link to my blog post about CSP at http://rehansaeed.com/content-security-policy-for-asp-net-mvc/ to ReadMe.html and FilterConfig.cs.
- Added link to my blog post about HTTP Headers at http://rehansaeed.com/nwebsec-asp-net-mvc-security-through-http-headers/ to FilterConfig.cs.
- Added link to my blog post about internet favicons at http://rehansaeed.com/internet-favicon-madness/ to _Layout.cshtml.

# 1.0.29
- Enabled Dynamic IP Security in log only mode in the web.config file. It provides a dynamic means of blocking malicious Web requests such as a Denial of Service (DoS) attack.
- Added a note to the ReadMe.txt file to adjust the Dynamic IP Security settings.

# 1.0.28
- Add new HTTP error codes to httpErrors in web.config and new static error pages under the Error folder:
    - 403.502 Forbidden errors using a static HTML file. This occurs due to a Denial of Service (Dos) attack.
    - 500.13 Internal Server Error errors using a static HTML file. This occurs because the server is too busy.
    - 503 Service Unavailable errors using a static HTML file.
    - 504 Gateway Timeout errors using a static HTML file.
- Add new ignore routes to RouteConfig.cs to ignore the new static HTML files.
- Added new note in Readme.html to update the static HTML error files.

# 1.0.27
- Microsoft CDN Bootstrap URL updated to 3.3.4.
- Twitter.Bootstrap.Less package updated to 3.3.4.

# 1.0.26
- Fixed skip to main content link for screen readers in _Layout.cshtml.
- Added aria-label and maxlength to search text box in _Layout.cshtml.

# 1.0.25
- Added a search action to HomeController that redirects to Google and with instructions on how to setup your own search.
- Added opensearch.xml file to site (See http://www.hanselman.com/blog/CommentView.aspx?guid=50cc95b1-c043-451f-9bc2-696dc564766d#commentstart and http://www.opensearch.org for more information.
    - HomeController.cs modified.
    - _Layout.cshtml modified.
    - Caching settings added to Web.config.
    - MVC routing settings added to Web.config.
- Added a note about adding search to your site and the Open Search protocol to ReadMe.html.
- Fixed sitemap.xml to return XML containing the version and correct encoding of UTF-8.

# 1.0.24
- Added accessibility section to ReadMe.html.
- aria-hidden="true" added to all font awesome icons for screen readers and better accessibility.
- Use 'span' instead of 'i' for all font awesome icons.
- Added aside and section HTML5 elements to Contact view to show an example of using an aside.
- Added aria role="article" to all HTML5 article elements.
- Added aria role="complementary" to all HTML5 aside elements.
- Added aria role="region" to all HTML5 section elements.

# 1.0.23
- Enable tracing by default (Only in Debug mode).
- Added debug menu item showing Elmah, Glimpse and Tracing links (Only visible in Debug mode).

# 1.0.22
- Added 400 Bad Request to ErrorController, Views, Cache config and http.
- Added 500 Internal Server Error to ErrorController, Views, Cache config and http.
- Added HttpContext.Current to Elmah logging in the LoggingService. Custom logging should log extra information about the current request if available.
- Added a security note to ReadMe.html explaining the need to keep NuGet packages up to date.
- Added a security note to ReadMe.html about enabling retail mode.

# 1.0.21
- WOFF MIME type changed to application/font-woff as per specification. See http://stackoverflow.com/questions/3594823/mime-type-for-woff-fonts.
- Added a note to encrypt the machine key in web.config and ReadMe.html.
- Added notes on how to update NuGet packages and files from this template to ReadMe.html.
- Changes courtesy of @surfsflo on GitHub
    - Add mimeMap to fix "404 not found" error when serving "ghyphicon-halflings-regular.woff2".
    - Fixed bootstrap glyphicon "404 not found" error. Path missing "/" on bootstrap "site.less".

# 1.0.20
- Updated ReadMe.html with information about using the https://www.ssllabs.com/ssltest site to check that you have implemented SSL/TLS over HTTPS correctly.

# 1.0.19
- Microsoft CDN jQuery Validation URL updated to 1.13.1.
- Microsoft CDN jQuery Validation Unobtrusive URL updated to 5.2.3.
- Microsoft CDN Modernizr URL updated to 2.8.3.
- Microsoft CDN Bootstrap URL updated to 3.3.2.
- Microsoft CDN Respond URL updated to 1.4.2.

# 1.0.18
- Updated Startup.cs to add Preload directive to Strict-Transport-Security. See https://developer.mozilla.org/en-US/docs/Web/Security/HTTP_strict_transport_security#Preloading_Strict_Transport_Security.
- SSL/TLS section in ReadMe.html split up and expanded with more information and links.
- ReadMe.html split up into sub-sections.

# 1.0.17
- Stop IIS returning a 403.12 Forbidden response when navigating to a folder e.g. /Content
  by rewriting to 404 not found using Web.config. Also set defaultDocument enabled=false to achieve the same thing.
  See http://www.troyhunt.com/2014/09/solving-tyranny-of-http-403-responses.html.
- Add a note to ReadMe.html to save images for the web and use Visual Studio Image Optimizer in conjunction
  with PNG Gauntlet to losslessly compress your images.
- Add a note to ReadMe.html saying that default document handling has been disabled and detailing how you can turn it on if you need to.

# 1.0.16
- Added Android/Chrome version M39+ favicon/theming support. Layout.cshtml updated. Files added include:
    \Content\icons\android-chrome-144x144.png
    \Content\icons\android-chrome-192x192.png
    \Content\icons\android-chrome-36x36.png
    \Content\icons\android-chrome-48x48.png
    \Content\icons\android-chrome-72x72.png
    \Content\icons\android-chrome-96x96.png
    \Content\icons\manifest.json
- \Content\icons\favicon-160x160.png removed - Opera can use other icon sizes to fill the gap.
- Compressed PNG images further using the excellent and free PNG Gauntlet.
- Use HTML5 self closing tags http://stackoverflow.com/questions/3558119/are-self-closing-tags-valid-in-html5
  and http://stackoverflow.com/questions/1946426/html-5-is-it-br-br-or-br.

# 1.0.15
- Strict-Transport-Security HTTP header configured for use using the NWebSec.Owin NuGet package in Startup.cs.
  Commented out by default. ReadMe.html also updated with this information.
- NWebSec.Owin package added.
- Added comments for the NWebSec SetNoCacheHttpHeadersAttribute attribute.
- Removed the commented out NWebSec XXssProtectionAttribute.

# 1.0.14
- Default port for site changed from 81 to 8080. Visual Studio needs admin privileges to run on a port less than 1024.
  (For more information see http://www.iis.net/learn/extensions/using-iis-express/running-iis-express-without-administrative-privileges).

# 1.0.13
- Added NWebSec CSP 2.0 filters to FilterConfig.cs for default settings and comments.
- Turn off tracing in Release mode, just in case it is turned on.
- Remove tracing HTTP handlers in Release Mode, so navigating to /trace.axd gives us a 404 Not Found,
  rather than a 500 Internal Server Error for added security and performance.
- Added Web.config comments encouraging encryption of connection strings.
- Added ReadMe.html comments encouraging encryption of connection strings.
- Added ReadMe.html comments encouraging users to scan their site for security vulnerabilities at asafaweb.com.
- Added message to CspViolationException explaining that CSP can be set to report-only mode.
- Added comments to LoggingService explaining where we are logging.

# 1.0.12
- Microsoft.Owin package updated to 3.0.1.
- Microsoft.Owin.Host.SystemWeb package updated to 3.0.1.
- NWebSec updated to 4.0.0.
    - Web.config changes to point to new DLL.
    - HttpHeaderSecurityModuleConfig.xsd updated.
    - FilterConfig changed to fix NWebSec breaking changes. Enabled/Disabled enum changed to bool and namespace removed.
    - Index.cshtml updated with new GitHub home page for NWebSec.
- NWebSec package updated to 4.0.0.
- NWebSec.Mvc package updated to 4.0.0.
- NWebSec.Core package updated to 1.3.0.

# 1.0.11
- Fixed Issue #1 - Intellisense broken in cshtml

# 1.0.10
- Microsoft.AspNet.Cors package updated to 5.2.3.
- Microsoft.AspNet.Mvc package updated to 5.2.3.
- Microsoft.AspNet.Razor package updated to 3.2.3.
- Microsoft.AspNet.WebPages package updated to 3.2.3.
- Microsoft.jQuery.Unobtrusive.Validation package updated to 3.2.3.
- Twitter.Bootstrap.Less package updated to 3.3.2 (Microsoft CDN URL remains on 3.3.1 as 3.3.2 is not available. Changes are minor).

# 1.0.8
- jQuery NuGet package updated to 2.1.3.
- Google CDN jQuery URL updated to 2.1.3.
- Newtonsoft.Json NuGet package updated to 6.0.8.

# 1.0.7
- Updated Visual Studio Gallery icons and New Project dialogue icons for template.

# 1.0.6
- jQuery.Validation NuGet package updated to 1.13.0.
- Twitter.Bootstrap.Less NuGet package updated to 3.3.1.
- Link to RehanSaeed.com containing details of this project added to Home.cshtml.
- Newtonsoft.Json NuGet package updated to 6.0.7

# 1.0.0
Initial Release