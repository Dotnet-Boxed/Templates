# ![ASP.NET Core Boilerplate Logo](https://raw.githubusercontent.com/ASP-NET-Core-Boilerplate/Templates/master/Images/Nuget%20Icon.png) [ASP.NET Core MVC Boilerplate](https://github.com/ASP-NET-Core-Boilerplate/Templates)

![ASP.NET Core MVC Boilerplate Preview Image](https://raw.githubusercontent.com/ASP-NET-Core-Boilerplate/Templates/master/Images/MVC%206%20Preview%20Image.png)

## Technology Map

The ASP.NET Core MVC project template contains the following features:

![ASP.NET Core MVC Boilerplate Technology Map](https://raw.githubusercontent.com/ASP-NET-Core-Boilerplate/Templates/master/Images/MVC%206%20Technology%20Map.png)

## Feature Selection Wizard

The ASP.NET Core MVC project template comes with a feature selection wizard where literally everything can be turned on
or off with the click of a button for a truly personalized project.


![ASP.NET Core MVC Boilerplate Feature Selection Wizard Screenshot](https://raw.githubusercontent.com/ASP-NET-Core-Boilerplate/Templates/master/Images/ASP.NET%20MVC%20Boilerplate%20Feature%20Selection%20Wizard%201.png)

![ASP.NET Core MVC Boilerplate Feature Selection Wizard Screenshot](https://raw.githubusercontent.com/ASP-NET-Core-Boilerplate/Templates/master/Images/ASP.NET%20MVC%20Boilerplate%20Feature%20Selection%20Wizard%202.png)

![ASP.NET Core MVC Boilerplate Feature Selection Wizard Screenshot](https://raw.githubusercontent.com/ASP-NET-Core-Boilerplate/Templates/master/Images/ASP.NET%20MVC%20Boilerplate%20Feature%20Selection%20Wizard%203.png)

![ASP.NET Core MVC Boilerplate Feature Selection Wizard Screenshot](https://raw.githubusercontent.com/ASP-NET-Core-Boilerplate/Templates/master/Images/ASP.NET%20MVC%20Boilerplate%20Feature%20Selection%20Wizard%204.png)

![ASP.NET Core MVC Boilerplate Feature Selection Wizard Screenshot](https://raw.githubusercontent.com/ASP-NET-Core-Boilerplate/Templates/master/Images/ASP.NET%20MVC%20Boilerplate%20Feature%20Selection%20Wizard%205.png)

![ASP.NET Core MVC Boilerplate Feature Selection Wizard Screenshot](https://raw.githubusercontent.com/ASP-NET-Core-Boilerplate/Templates/master/Images/ASP.NET%20MVC%20Boilerplate%20Feature%20Selection%20Wizard%206.png)

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

### Secure By Default

The default ASP.NET Core template is not as secure as it could be. There are various settings which are insecure by default. For example, it leaks information about which version of IIS you are using and allows external scripts to access cookies by default!

Setting up [SSL](http://en.wikipedia.org/wiki/SSL)/[TLS](http://en.wikipedia.org/wiki/Transport_Layer_Security), so that your site runs over HTTPS is made easy by turning on a single check box and easy step by step instructions and links.

ASP.NET Core Boilerplate makes everything secure by default but goes further and uses various HTTP headers which are sent to the browser to restrict things further. It makes use of the [Content Security Policy (CSP)](https://developer.mozilla.org/en-US/docs/Web/Security/CSP/Introducing_Content_Security_Policy) HTTP Header using the [NWebSec](https://github.com/NWebsec/NWebsec) NuGet packages. It also adds the [Strict Transport Security (HSTS)](https://developer.mozilla.org/en-US/docs/Web/Security/HTTP_strict_transport_security) HTTP header and also makes it very easy to use the [Public Key Pinning (HPKP)](https://developer.mozilla.org/en-US/docs/Web/Security/Public_Key_Pinning) HTTP header. Finally, it also adds the [X-Content-Type-Options](http://rehansaeed.com/nwebsec-asp-net-mvc-security-through-http-headers/), [X-Download-Options](http://rehansaeed.com/nwebsec-asp-net-mvc-security-through-http-headers/) and [X-Frame-Options](https://developer.mozilla.org/en-US/docs/Web/HTTP/X-Frame-Options?redirectlocale=en-US&redirectslug=The_X-FRAME-OPTIONS_response_header) HTTP headers by default.

Do you trust the people on the internet? Well, the default ASP.NET Core template stores the contents of all NPM and Bower packages in the wwwroot folder. These contain executables, scripts and all kinds of potential nasties that your site is serving up. ASP.NET Core Boilerplate moves these packages out and only adds things to the wwwroot folder that are required.

Finally, ASP.NET Core Boilerplate takes security to the bleeding edge with [Subresource Integrity (SRI)](http://rehansaeed.com/subresource-integrity-taghelper-using-asp-net-core/) implemented by default. SRI ensures that third party resources from CDN's have not been compromised.

### Fast By Default

The default ASP.NET Core template does a pretty poor job in the performance department. Probably because they don't make any assumptions about which web server you are using. Most of the world and dog that are writing ASP.NET Core sites use IIS and there are settings in the web.config file under the system.webServer section which can make a big difference when it comes to performance.

ASP.NET Core Boilerplate makes no such assumptions. It turns on GZip compression for static and dynamic files being sent to the browsers making them smaller and quicker to download. It also uses Content Delivery Networks (CDN) by default to make common scripts like jQuery quicker to download.

That's not all! There are a bunch of other tweaks and examples of practices which can help improve the performance of the site. ASP.NET Core Boilerplate achieves a score of **96/100** on [YSlow](http://yslow.org/) (Its not possible to get the full 100 as some of it's criteria contradict each other and site scripts need to be moved to a CDN which you need to do yourself).

### Search Engine Optimization (SEO)

The default ASP.NET Core template takes no consideration of Search Engine Optimization at all. ASP.NET Core Boilerplate adds a dynamically generated robots.txt file to tell search engines which pages they can index. It also adds a dynamically generated sitemap.xml file where you can help search engines even further by giving them links to all your pages.

ASP.NET Core has some very useful settings for appending trailing slashes to URL's and making all URL's lower case. Unfortunately, both of these are turned off by default, which is terrible for SEO. This project turns them on by default.

It also includes an MVC filter which helps to redirect non-canonical URL's (URL's without a trailing slash or mixed case characters which are considered different URL's by search engines) to their canonical equivalent.

### Accessibility

4% of the world population is estimated to be visually impaired, while 0.55% are blind. Get more statistics [here](http://www.sitepoint.com/how-many-users-need-accessible-websites). ASP.NET Core Boilerplate ensures that your site is accessible by adding aria attributes to your HTML mark-up and special shortcuts for people using screen readers.

### Browser Compatibility

Websites need to reach as many people as possible and look good on a range of different devices. ASP.NET Core Boilerplate supports browsers as old as IE9 (IE9 still has around 2% market share and is mostly used by corporations too lazy to port their old websites to newer browsers).

ASP.NET Core Boilerplate also supports devices other than desktop browsers as much as possible. It has default icons and splash screens for Windows 8, Android, Apple Devices and a few other device specific settings included by default.

### Resilience and Error Handling

At some point your site is probably going to throw an exception and you will need to handle and log that exception to be able to understand and fix it. ASP.NET Core Boilerplate turns on and configures logging by default. It's all preconfigured and ready to use.

ASP.NET Core Boilerplate uses popular Content Delivery Networks (CDN) from Google and Microsoft but what happens in the unlikely event that these go down? Well, ASP.NET Core Boilerplate provides backups for these.

Not only that but standard error pages such as 500 Internal Server Error, 404 Not Found and many others are built in to the template. ASP.NET Core Boilerplate even includes IIS configuration to protect you from [Denial-of-Service](http://en.wikipedia.org/wiki/Denial-of-service_attack) (DoS) attacks.

### Easier Debugging and Performance Testing Tools

ASP.NET Core Boilerplate makes use of [Glimpse](http://getglimpse.com/) (As advertised by Scott Hansleman [here](http://www.hanselman.com/blog/IfYoureNotUsingGlimpseWithASPNETForDebuggingAndProfilingYoureMissingOut.aspx)). It's a great tool to use as you are developing, to find performance problems and bugs. Of course, Glimpse is all preconfigured, so you don't need to lift a finger to install it.

### Patterns and Practices

Doing things right does sometimes take a little extra time. Using the [Inversion of Control (IOC)](http://martinfowler.com/articles/injection.html) pattern for example should be a default.

ASP.NET Core Boilerplate also makes use of the popular [SASS](http://sass-lang.com/) files for making life easier with CSS. For an example, it can make overriding colours and fonts in the default Bootstrap CSS a cinch.

ASP.NET Core is a complicated beast. You can end up with lots of [magic strings](http://en.wikipedia.org/wiki/Magic_string) which can be a nightmare when renaming something. There are many ways of eliminating these magic strings but most trade maintainability for slower performance. ASP.NET Core Boilerplate makes extensive use of constants which are a trade-off between maintainability and performance, giving you the best of both worlds.

### Atom Feed

An [Atom 1.0](http://atomenabled.org/developers/syndication/) has been included by default. Atom was chosen over RSS because it is the [better and newer](http://www.intertwingly.net/wiki/pie/Rss20AndAtom10Compared) specification. [PubSubHubbub](https://github.com/pubsubhubbub) 0.4 support has also been built in, allowing you to push feed updates to subscribers.

### Search

There is a lot more to implementing search in your application than it sounds. ASP.NET Core Boilerplate includes a search feature by default but leaves it open for you to choose how you want to implement it. It also implements [Open Search](http://www.opensearch.org) XML right out of the box. Read Scott Hanselman talk about this feature [here](http://www.hanselman.com/blog/OnTheImportanceOfOpenSearch.aspx).

### Social Media Support

[Open Graph](http://ogp.me/) meta tags and [Twitter Card](https://dev.twitter.com/cards/overview) meta tags are included by default. Not only that but ASP.NET Core Boilerplate includes fully documented HTML helpers that allow you to easily generate Open Graph object or Twitter Card met tags easily and correctly.

## How can I get it?
That's easy, just choose one of the following options:

- Get the Visual Studio extension [here](https://visualstudiogallery.msdn.microsoft.com/6cf50a48-fc1e-4eaf-9e82-0b2a6705ca7d) and in Visual Studio go to File -> New Project -> Web.
- Clone the git repository: `git clone https://github.com/ASP-NET-Core-Boilerplate/Templates`

## Release Notes and To-Do List
You can find release notes for each version [here](https://github.com/ASP-NET-Core-Boilerplate/Templates/blob/master/RELEASE%20NOTES.md) and a To-Do list of new features and enhancements coming soon in the [projects](https://github.com/ASP-NET-Core-Boilerplate/Templates/projects) tab.

## Contributing

Please read the [guide](https://github.com/ASP-NET-Core-Boilerplate/Templates/blob/master/CONTRIBUTING.md) to learn how you can make a contribution.

## Bugs and Issues

Please report any bugs or issues on the GitHub issues page [here](https://github.com/ASP-NET-Core-Boilerplate/Templates/issues).
