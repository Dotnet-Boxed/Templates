<div id="main" class="container body-content" role="main">[![.NET Boxed](https://raw.githubusercontent.com/Dotnet-Boxed/Templates/master/Images/Banner.png)](https://github.com/Dotnet-Boxed/Templates) 

<section>

## Task Check List

This is a check-list of tasks you must perform to get your site up and running faster. Open this file in the browser of your choice and it will remember what you have checked, just don't clear your cache.

<article>

### <span aria-hidden="true" class="fa fa-list"></span>Pre-Requisites

*   <input name="PreRequisites-UpdateVisualStudio" type="checkbox"> **Update Visual Studio / Visual Studio Code** - Update your version of Visual Studio or Visual Studio Code with all patches and updates.
*   <input name="PreRequisites-UpdateNuGet" type="checkbox"> **Update NuGet** - Update the NuGet Visual Studio extension from the Tools -> Extensions and Updates menu.
*   <input name="PreRequisites-UpdateDNVM" type="checkbox"> **Update Visual Studio ASP.NET Core Tools** - Update the Visual Studio ASP.NET Core tools. Find out more at [dot.net](https://dot.net).
*   <input name="PreRequisites-IISNETCoreWindowsServerHosting" type="checkbox"> **IIS .NET Core Windows Server Hosting Bundle** - If you are using IIS and configuring it yourself, install the [.NET Core Windows Server Hosting](https://docs.asp.net/en/latest/publishing/iis.html) bundle from [here](http://go.microsoft.com/fwlink/?LinkId=798480).

</article>

<article>

### <span aria-hidden="true" class="fa fa-lock"></span>API Guidelines

*   <input name="HTTPS-ConfigureHSTSPreload" type="checkbox"> **Zalando REST'ful API Guidelines** - Read the [Zalando REST'ful API Guidelines](https://opensource.zalando.com/restful-api-guidelines/).

</article>

<article>

### <span aria-hidden="true" class="fa fa-lock"></span>Security

#### TLS over HTTPS

*   <input name="HTTPS-ConfigureSitewideHTTPS" type="checkbox"> **Configure Site-Wide HTTPS** - Please note that [SSL](http://en.wikipedia.org/wiki/SSL) has been superseded by [TLS](http://en.wikipedia.org/wiki/Transport_Layer_Security). SSL is vulnerable to the [POODLE](http://en.wikipedia.org/wiki/POODLE) security vulnerability and should not be used. These steps outline how to secure your site so that all requests and responses are made over [HTTPS](http://en.wikipedia.org/wiki/HTTPS) using TLS, you should consider using it across your whole site for best security, rather than having a mix of HTTP and HTTPS pages. TLS certificates can be obtained for free at:
    *   [LetsEncrypt.org](https://letsencrypt.org/)
    *   [StartSSL.com](https://www.startssl.com/) (You can follow [this](http://www.troyhunt.com/2013/09/the-complete-guide-to-loading-free-ssl.html) guide to setting up TLS using StartSSL.)
    *   [Digicert](https://www.digicert.com/friends/mvp.php) (Digicert is only for Microsoft employees or MVP's)
*   <input name="HTTPS-ConfigureHSTSPreload" type="checkbox"> **Configure Strict Transport Security (HSTS) Preloading** - If using Strict-Transport-Security, submit your domain to the [HSTS Preload](https://hstspreload.appspot.com/) site so that your domain can be preloaded using HTTPS rather than HTTP See [this](https://developer.mozilla.org/en-US/docs/Web/Security/HTTP_strict_transport_security#Preloading_Strict_Transport_Security) for more information about preloading.
*   <input name="HTTPS-ConfigureHPKP" type="checkbox"> **Configure Public Key Pinning (HPKP)** - Open the _ApplicationBuilderExtensions.cs_ file and uncomment the `application.UseHpkp(...);` line to turn on [Public Key Pinning (HPKP)](https://developer.mozilla.org/en-US/docs/Web/Security/Public_Key_Pinning). Please note that there is some risk to setting up Public Key Pinning (HPKP), if you get it wrong people who have visited your site will get an error in their browser upon visiting your site so be careful.
*   <input name="HTTPS-TestHttps" type="checkbox"> **Test Your HTTPS Implementation** - Use the [SSLLabs.com](https://www.ssllabs.com/ssltest) site to check that you have implemented TLS over HTTPS correctly.
*   <input name="HTTPS-CertificateTransparency" type="checkbox"> **Certificate Transparency** - [Certificate Transparency (CT)](https://scotthelme.co.uk/revocation-is-broken/) requires that all Certificate Authorities (CA's) publically log all certificates they create. If a certificate is mistakenly issued for your site you can be notified by the following services:
    *   [Facebook Certificate Transparency Monitoring](https://developers.facebook.com/tools/ct/)
    *   [sslmate CertSpotter](https://sslmate.com/certspotter/)
    *   [crt.sh](https://crt.sh/?q=example.com)

#### Understand Web Security

*   <input name="OtherSecurity-UnderstandOWASPTopTen" type="checkbox"> **Understand OWASP Top 10** - Understand the top ten security vulnerabilities and how you can protect yourself from them at [OWASP Top 10](https://www.owasp.org/index.php/Category:OWASP_Top_Ten_Project). Also take a look at the [OWASP Top Ten Cheat Sheet](https://www.owasp.org/index.php/Cheat_Sheets).
*   <input name="OtherSecurity-UnderstandCrossSiteScripting" type="checkbox"> **Understand Cross Site Scripting (XSS)** - Understand Cross Site Scripting (XSS) security vulnerabilities and how you can protect yourself from them using the [OWASP XSS Cheat Sheet](https://www.owasp.org/index.php/XSS_%28Cross_Site_Scripting%29_Prevention_Cheat_Sheet) and [OWASP DOM Based XSS Cheat Sheet](https://www.owasp.org/index.php/DOM_based_XSS_Prevention_Cheat_Sheet).
*   <input name="OtherSecurity-UnderstandCrossSiteRequestForgery" type="checkbox"> **Understand Cross Site Request Forgery (CSRF)** - Understand Cross Site Request Forgery security vulnerabilities and how you can protect yourself from them using the [OWASP CSRF Cheat Sheet](https://www.owasp.org/index.php/Cross-Site_Request_Forgery_%28CSRF%29_Prevention_Cheat_Sheet).

#### Other Security

*   <input name="OtherSecurity-SecretStore" type="checkbox"> **Use Secret Store** - If you want to use connection strings or other secrets without checking them in to your source control repository, use the secret store. See [this](http://go.microsoft.com/fwlink/?LinkID=532709) and [this](http://docs.asp.net/en/latest/security/app-secrets.html) for more information.
*   <input name="OtherSecurity-RequestLimits" type="checkbox"> **Adjust Request Limits** - There are settings in the _Web.config_ file under the `requestLimits` element that limit maximum size of HTTP requests clients can make to your site. You can limit the maximum content size, maximum URL length and maximum query string length. You should lower these as much as possible, while still having a working site.
*   <input name="OtherSecurity-KeepNuGetUpToDate" type="checkbox"> **Keep NuGet Up To Date** - Keep your NuGet packages up to date to patch security vulnerabilities. See the section about updating below.
*   <input name="OtherSecurity-AzureSQLThreatDetection" type="checkbox"> **Turn on Azure SQL Database Threat Detection** - If you are using SQL Database on Azure, then turn on [Threat Detection](https://azure.microsoft.com/en-us/documentation/articles/sql-database-threat-detection-get-started/).

</article>

<article>

### <span aria-hidden="true" class="fa fa-tachometer"></span>Performance

#### Content Delivery Networks (CDN)

*   <input name="ContentDeliveryNetworks-UploadStaticFilesToCDN" type="checkbox"> **Upload Static Files to CDN** - Upload the static files to a [CDN](http://en.wikipedia.org/wiki/Content_delivery_network) for vastly better performance. Examples of static content include your images, minified CSS bundle content, minified JavaScript bundle content, etc.
    *   [Cloudflare CDN](http://www.cloudflare.com/CDN)
    *   [Azure CDN](http://azure.microsoft.com/en-us/documentation/articles/cdn-serve-content-from-cdn-in-your-web-application/)

#### Caching

*   <input name="Caching-ConfigureCaching" type="checkbox"> **Configure Caching** - Some resources are generated programmatically and then cached for a certain time period. If these resources won't change much on your site, change the length of time they are cached. Open the _appsettings.json_ file and take a look at the cache profiles section.
*   <input name="Caching-AddMoreCaching" type="checkbox"> **Add More Caching** - Use the `[ResponseCache]` attribute in conjunction with the cache profiles section in the appsettings.json file to cache pages that don't change often and do not contain sensitive information. See [this](http://www.asp.net/mvc/overview/older-versions-1/controllers-and-routing/improving-performance-with-output-caching-cs) for more information.

#### Benchmarking

*   <input name="OtherPerformance-RunGooglePageSpeed" type="checkbox"> **Run Google Page Speed** - Use [Google Page Speed](https://developers.google.com/speed/pagespeed/) to benchmark your sites performance and to get suggestions on how to further improve performance. Note that this template is already very quick to begin with.
*   <input name="OtherPerformance-RunYahooYSlow" type="checkbox"> **Run Yahoo's YSlow** - Use [Yahoo's YSlow](http://yslow.org/) to benchmark your sites performance and to get suggestions on how to further improve performance.

#### Other Performance

*   <input name="OtherPerformance-Prefetcher" type="checkbox"> **Enable Windows Server Pre-fetcher** - This is only relevant if your site is hosted on Windows Server. You can enable the [pre-fetcher](http://en.wikipedia.org/wiki/Prefetcher) to get a performance and reduce the disk-read cost of application start-up. Click [here](http://www.asp.net/aspnet/overview/aspnet-and-visual-studio-2012/whats-new#_Toc_perf_6) for more information on how to do this.

</article>

<article>

### <span aria-hidden="true" class="fa fa-puzzle-piece"></span>Compatibility

*   <input name="Compatibility-OldIIS" type="checkbox"> **IIS 7.5/8** - Some _web.config_ settings do not exist in older versions of IIS (7.5 and 8). If you are using an older version, edit the _web.config_ file and remove the `dynamicIpSecurity` settings.

</article>

<article>

### <span aria-hidden="true" class="fa fa-user"></span>Authentication & Authorization

*   <input name="Authentication-Provider" type="checkbox"> **Choose Authentication Provider** - If you want no authentication, then you don't need to do anything. **Do not** build your own authentication provider unless you are a security expert, it is much harder than it looks and even professionals get it wrong. Choose from one of the options below:
    1.  IdentityServer4 - This is another site that handles all authentication with OAuth and Open ID Connect.
    2.  ASP.NET Identity - This is basically forms authentication with OAuth and Open ID Connect. This is what you get when you select 'Individual Account' from Microsoft's '[Change Authentication](http://i2.asp.net/media/47397/ConfigureAuthDialog.png?cdn_id=2015-05-22-001)' dialogue, when you create a new Web Project. Follow [this](http://www.asp.net/identity/overview/getting-started) tutorial. Don't forget to add the `NoLowercaseQueryStringAttribute` to the `AccountController` to enable the `RedirectToCanonicalUrlAttribute` to work (See comments on these classes for more information about what they do).
    3.  Windows Authentication - Follow [this](http://www.asp.net/aspnet/overview/owin-and-katana/enabling-windows-authentication-in-katana) tutorial.

</article>

<article>

### <span aria-hidden="true" class="fa fa-exclamation-triangle"></span>Resilience and Error Handling

*   <input name="ResilienceAndErrorHandling-DosProtection" type="checkbox"> **Denial of Service (DoS) Protection** - There are a number of ways to protect your site from [Denial of Service (DoS)](http://en.wikipedia.org/wiki/Denial-of-service_attack) attacks:
    *   [Cloudflare](https://www.cloudflare.com/) - You can use the Cloudflare service as a proxy to your site. Cloudflare has sophisticated techniques to block attackers from accessing your site and is used by a large proportion of websites on the internet.
    *   [Dynamic IP Security](http://www.iis.net/configreference/system.webserver/security/dynamicipsecurity) is a feature of IIS that can be configured using the `dynamicIpSecurity` section of the _web.config_ file. Dynamic IP Security is set to logging only mode in this site, so IIS will log malicious requests from clients without actually rejecting them. If you decide to use this feature, run your site in this mode for some time and slowly raise the limits until you get no more 403.501 or 403.502 errors. Normal legitimate requests that can look like a DoS attack are:
        1.  Google and Bing often make large numbers of requests that can look like a DoS attack and you do not want to block them by accident.
        2.  Some organizations put themselves behind a proxy and their requests can look like they are from a single IP address.
        3.  Pages on your site that cause a large number of requests to be made your site may trigger the Dynamic IP Security limits to be triggered and the page loading will be treated like a Denial of Service (DoS) attack.You can test this feature by recording a request to your site using [Fiddler](http://www.telerik.com/fiddler) and then replaying it many times.
*   <input name="ResilienceAndErrorHandling-ApplicationInsights" type="checkbox"> **Application Insights** - Application Insights helps monitor your site and know about it when it goes down. Login to [Azure](http://azure.microsoft.com), create a new Application Insights instance, retrieve the Instrumentation Key and add it to your _appsettings.json_ file. For more information see the [Getting Started](http://docs.asp.net/en/latest/fundamentals/application-insights.html) guide.
*   <input name="ResilienceAndErrorHandling-Logging" type="checkbox"> **Add Logging** - This template comes with basic logging enabled in development mode. Consider using [Serilog](http://serilog.net/) or another logging framework in production. Logging can be configured in _Startup.cs_.

</article>

<article>

### <span aria-hidden="true" class="fa fa-users"></span>Thank Developers

*   <input name="ThankDevelopers-ConfigureHumansTxt" type="checkbox"> **Configure Humans.txt** - Edit the _humans.txt_ file at the root of the site. This is totally optional, you can delete this file if you want. Be careful what you put into this file, you could give away clues about potential attack vectors on your site. Even giving contact details can be an attack vector (Most hackers manipulate people to get access).
*   <input name="ThankDevelopers-Review" type="checkbox"> **Thank Developers** - A lot of work was put into this project template. Show your appreciation by giving the project a star on [GitHub](https://github.com/Dotnet-Boxed/Templates).

</article>

<article>

### <span aria-hidden="true" class="fa fa-refresh"></span>Keeping Your Site Up To Date

*   <input name="KeepingYourSiteUpToDate-KeepNuGetPackagesUpToDate" type="checkbox"> **Keep NuGet Packages Up To Date** - Most updates can be easily carried out by simply updating [NuGet](https://www.nuget.org/) packages. See [this](https://docs.nuget.org/consume/package-manager-dialog) link for more information. However, there are a few things to watch out for before updating a package:
    1.  It's worth reading the NuGet package release notes to see what has changed and base the urgency of upgrading on this information. Blindly updating, can sometimes cause things to break. Sometimes packages are incompatible with other packages or there are special upgrade steps that need to be carried out.
    2.  When updating JavaScript or CSS framework Bower Packages, be careful that the particular version you are updating to is also available on the [CDN](http://en.wikipedia.org/wiki/Content_delivery_network). Some CDN's update more often than others. This project template uses the popular Google and Microsoft CDN's as it is likely that their scripts are already preloaded in a browsers cache. However, their CDN service also does not update too often.
    3.  You need to ensure that you update your JavaScript and CSS framework Bower packages when there are new versions to obtain security patches but also because [CDN](http://en.wikipedia.org/wiki/Content_delivery_network)'s sometimes retire very old scripts. This template provides fallback scripts, so if a CDN does retire a script or goes down, it will use the scripts in this template instead. This will result in a slight performance degradation.
*   <input name="KeepingYourSiteUpToDate-KeepTemplateUpToDate" type="checkbox"> **Keep Template Up To Date** - Keep an eye on the [release notes](https://github.com/Dotnet-Boxed/Templates/releases) and update your site accordingly.

</article>

</section>

</div>
