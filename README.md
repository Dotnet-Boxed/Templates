<h1>
<img src="https://raw.githubusercontent.com/RehanSaeed/ASP.NET-MVC-Boilerplate/master/Images/Nuget Icon.png" alt="ASP.NET MVC Boilerplate Logo" width="30px" height="30px"/> <a href="https://github.com/RehanSaeed/ASP.NET-MVC-Boilerplate">ASP.NET MVC Boilerplate</a>
</h1>

<img alt="ASP.NET-MVC-Boilerplate in the Visual Studio 'New Project' dialogue"
     border="5"
     src="https://github.com/RehanSaeed/ASP.NET-MVC-Boilerplate/blob/master/Images/New%20Project.png"/>
     
<p>A professional ASP.NET MVC template for building secure, fast, robust and adaptable web applications or sites. It provides the minimum amount of code required on top of the default MVC template provided by Microsoft. Find out more at <a href="http://rehansaeed.com/asp-net-mvc-boilerplate/">RehanSaeed.com</a>, the <a href="https://visualstudiogallery.msdn.microsoft.com/6cf50a48-fc1e-4eaf-9e82-0b2a6705ca7d">Visual Studio Gallery</a> or at <a href="https://www.owasp.org/index.php/OWASP_ASP.NET_MVC_Boilerplate_Project">The Open Web Security Project (OWASP)</a>. You can also follow me on Twitter at <a href="https://twitter.com/rehansaeeduk">@RehanSaeedUK</a>.</p>

<img alt="ASP.NET-MVC-Boilerplate Preview Image"
     border="5"
     src="https://github.com/RehanSaeed/ASP.NET-MVC-Boilerplate/blob/master/Images/MVC%206%20Preview%20Image.png"/>

<h2>Why Do I Need It?</h2>

<p>The default MVC template that Visual Studio gives you does not make best use of the tools available. It's insecure, slow, and really has a very basic feature list (That's the point of it). ASP.NET MVC Boilerplate provides you with a few more pieces of the puzzle to get you started quicker. It makes liberal use of comments and even gives you a checklist of tasks which you need to perform to make it even better. The main benefits of using this template are:</p>

<ul>
     <li>Security</li>
     <li>Performance</li>
     <li>Search Engine Optimization (SEO)</li>
     <li>Accessibility</li>
     <li>Browser Compatibility</li>
     <li>Resilience and Error Handling</li>
     <li>Easier Debugging and Performance Testing Tools</li>
     <li>Patterns and Practices</li>
     <li>Atom Feed</li>
     <li>Search</li>
     <li>Social Media Support</li>
</ul>

<h2>ASP.NET 4.6 MVC 5 and ASP.NET 5 MVC 6 Support</h2>

<p>Two templates are provided. One for ASP.NET 4.6 MVC 5 and another ASP.NET 5 MVC 6 template which is currently under development and is missing some features due to ASP.NET 5 still being in Beta.</p>

<h4>MVC 5 Technology Map</h4>

<img alt="ASP.NET-MVC-5-Boilerplate Technology Map"
     border="5"
     src="https://github.com/RehanSaeed/ASP.NET-MVC-Boilerplate/blob/master/Images/MVC%205%20Technology%20Map.png"/>

<h4>MVC 6 Technology Map</h4>

<img alt="ASP.NET-MVC-6-Boilerplate Technology Map"
     border="5"
     src="https://github.com/RehanSaeed/ASP.NET-MVC-Boilerplate/blob/master/Images/MVC%206%20Technology%20Map.png"/>

<h2>Secure By Default</h2>

<p>The default MVC template is not as secure as it could be. There are various settings (Mostly in the web.config file) which are insecure by default. For example, it leaks information about which version of IIS you are using and allows external scripts to access cookies by default!</p>

<p>ASP.NET MVC Boilerplate makes everything secure by default but goes further and uses various HTTP headers which are sent to the browser to restrict things further.</p>

<p>It also makes use of the new <a href="https://developer.mozilla.org/en-US/docs/Web/Security/CSP/Introducing_Content_Security_Policy">Content Security Policy (CSP)</a> HTTP Header using the <a href="https://nwebsec.codeplex.com/">NWebSec</a> NuGet packages. CSP revolutionizes web security and I highly recommend reading the above link.</p>

<p>Setting up <a href="http://en.wikipedia.org/wiki/SSL">SSL</a>/<a href="http://en.wikipedia.org/wiki/Transport_Layer_Security">TLS</a>, so that your site runs over HTTPS is made easy with easy step by step instructions and links.</p>

<h2>Fast By Default</h2>

<p>The default MVC template does a pretty poor job in the performance department. Probably because they don't make any assumptions about which web server you are using. Most of the world and dog that are writing ASP.NET MVC sites use IIS and there are settings in the web.config file under the system.webServer section which can make a big difference when it comes to performance.</p>

<p>ASP.NET MVC Boilerplate makes no such assumptions. It turns on GZip compression for static and dynamic files being sent to the browsers making them smaller and quicker to download. It also uses Content Delivery Networks (CDN) by default to make common scripts like jQuery quicker to download (You can turn this off of course but the point is ASP.NET MVC Boilerplate is fast by default).</p>

<p>That's not all! There are a bunch of other tweaks and examples of practices which can help improve the performance of the site. ASP.NET MVC Boilerplate achieves a score of <em>96/100</em> on <a href="http://yslow.org/">YSlow</a> (Its not possible to get the full 100 as some of it's criteria contradict each other and site scripts need to be moved to a CDN which you need to do yourself).</p>

<h2>Search Engine Optimization (SEO)</h2>

<p>The default ASP.NET MVC template takes no consideration of Search Engine Optimization at all. ASP.NET MVC Boilerplate adds a dynamically generated robots.txt file to tell search engines which pages they can index. It also adds a dynamically generated sitemap.xml file where you can help search engines even further by giving them links to all your pages.</p>

<p>ASP.NET MVC has some very useful settings for appending trailing slashes to URL's and making all URL's lower case. Unfortunately, both of these are turned off by default, which is terrible for SEO. This project turns them on by default.</p>

<p>It also includes an MVC filter which helps to redirect non-canonical URL's (URL's without a trailing slash or mixed case characters which are considered different URL's by search engines) to their canonical equivalent.</p>

<h2>Accessibility</h2>

<p>4% of the world population is estimated to be visually impaired, while 0.55% are blind. Get more statistics <a href="http://www.sitepoint.com/how-many-users-need-accessible-websites">here</a>. ASP.NET MVC Boilerplate ensures that your site is accessible by adding aria attributes to your HTML mark-up and special shortcuts for people using screen readers.</p>

<h2>Browser Compatibility</h2>

<p>Websites need to reach as many people as possible and look good on a range of different devices. ASP.NET MVC Boilerplate supports browsers as old as IE8 (IE8 still has around 4% market share and is mostly used by corporations too lazy to port their old websites to newer browsers).</p>

<p>ASP.NET MVC Boilerplate also supports devices other than desktop browsers as much as possible. It has default icons and splash screens for Windows 8, Android, Apple Devices and a few other device specific settings included by default.</p>

<h2>Resilience and Error Handling</h2>

<p>At some point your site is probably going to throw an exception and you will need to handle and log that exception to be able to understand and fix it. ASP.NET MVC Boilerplate includes <a href="https://code.google.com/p/elmah/">Elmah</a>, the popular error logging addin by default. It's all preconfigured and ready to use.</p>

<p>ASP.NET MVC Boilerplate uses popular Content Delivery Networks (CDN) from Google and Microsoft but what happens in the unlikely event that these go down? Well, ASP.NET MVC Boilerplate provides backups for these.</p>

<p>Not only that but standard error pages such as 500 Internal Server Error, 404 Not Found and many others are built in to the template. ASP.NET MVC Boilerplate even includes IIS configuration to protect you from <a href="http://en.wikipedia.org/wiki/Denial-of-service_attack">Denial-of-Service</a> (DoS) attacks.</p>

<h2>Easier Debugging and Performance Testing Tools</h2>

<p>ASP.NET MVC Boilerplate makes use of <a href="http://getglimpse.com/">Glimpse</a> (As advertised by Scott Hansleman <a href="http://www.hanselman.com/blog/IfYoureNotUsingGlimpseWithASPNETForDebuggingAndProfilingYoureMissingOut.aspx">here</a>). It's a great tool to use as you are developing, to find performance problems and bugs. Of course, Glimpse is all preconfigured, so you don't need to lift a finger to install it.</p>

<h2>Patterns and Practices</h2>

<p>Doing things right does sometimes take a little extra time. Using the <a href="http://martinfowler.com/articles/injection.html">Inversion of Control (IOC)</a> pattern for example should be a default. ASP.NET MVC Boilerplate uses the <a href="http://autofac.org/">Autofac</a> IOC container by default. Some people get a bit tribal when talking about IOC containers but to be honest, they all work great. Autofac was picked because it has lots of helpers for ASP.NET MVC and Microsoft even uses it for Azure Mobile Services.</p>

<p>ASP.NET MVC Boilerplate also makes use of the popular <a href="http://lesscss.org/">Less</a> files for making life easier with CSS. For an example, it can make overriding colours and fonts in the default Bootstrap CSS a cinch.</p>

<p>ASP.NET MVC is a complicated beast. You can end up with lots of <a href="http://en.wikipedia.org/wiki/Magic_string">magic strings</a> which can be a nightmare when renaming something. There are many ways of eliminating these magic strings but most trade maintainability for slower performance. ASP.NET MVC Boilerplate makes extensive use of constants which are a trade-off between maintainability and performance, giving you the best of both worlds.</p>

<h2>Atom Feed</h2>

An <a href="http://atomenabled.org/developers/syndication/">Atom 1.0</a> has been included by default. Atom was chosen over RSS because it is the <a href="http://www.intertwingly.net/wiki/pie/Rss20AndAtom10Compared">better and newer</a> specification. <a href="https://github.com/pubsubhubbub">PubSubHubbub</a> 0.4 support has also been built in, allowing you to push feed updates to subscribers.

<h2>Search</h2>

There is a lot more to implementing search in your application than it sounds. ASP.NET MVC Boilerplate includes a search feature by default but leaves it open for you to choose how you want to implement it. It also implements <a href="http://www.opensearch.org">Open Search</a> XML right out of the box. Read Scott Hanselman talk about this feature <a href="http://www.hanselman.com/blog/CommentView.aspx?guid=50cc95b1-c043-451f-9bc2-696dc564766d#commentstart">here</a>.

<h2>Social Media Support</h2>

<a href="http://ogp.me/">Open Graph</a> meta tags and <a href="https://dev.twitter.com/cards/overview">Twitter Card</a> meta tags are included by default. Not only that but ASP.NET MVC Boilerplate includes fully documented HTML helpers that allow you to easily generate Open Graph object or Twitter Card met tags easily and correctly.

<h2>How can I get it?</h2>
That's easy, just choose one of the following options:
<ol>
  <li>Get the Visual Studio extension <a href="https://visualstudiogallery.msdn.microsoft.com/6cf50a48-fc1e-4eaf-9e82-0b2a6705ca7d">here</a> and in Visual Studio go to File -> New Project -> Web.</li>
  <li>
  Clone the git repository
  <blockquote>git clone https://github.com/RehanSaeed/ASP.NET-MVC-Boilerplate</blockquote>
  </li>
</ol>

<h2>Release Notes and To-Do List</h2>
You can find release notes for each version <a href="https://github.com/RehanSaeed/ASP.NET-MVC-Boilerplate/blob/master/Source/Boilerplate.Vsix/Release%20Notes.txt">here</a> and a TODO list of new features and enhancements coming soon <a href="https://github.com/RehanSaeed/ASP.NET-MVC-Boilerplate/blob/master/TODO.md">here</a>.

<h2>Bugs and Issues</h2>

Please report any bugs or issues on the GitHub issues page <a href="https://github.com/RehanSaeed/ASP.NET-MVC-Boilerplate/issues">here</a>.
