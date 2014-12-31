<h1><a href="https://github.com/RehanSaeed/ASP.NET-MVC-Boilerplate">ASP.NET MVC Boilerplate</a></h1>

<!--<img alt="ASP.NET-MVC-Boilerplate in the Visual Studio 'New Project' dialogue"
     border="5"
     src="https://github.com/RehanSaeed/ASP.NET-MVC-Boilerplate/blob/master/Images/New%20Project.png"/>-->
     
<p>A professional ASP.NET MVC template for building secure, fast, robust and adaptable web applications or sites. It provides the minimum amount of code required on top of the default MVC template provided by Microsoft. Find out more at <a href="http://rehansaeed.co.uk/asp-net-mvc-boilerplate">RehanSaeed.co.uk</a> or the <a href="https://visualstudiogallery.msdn.microsoft.com/6cf50a48-fc1e-4eaf-9e82-0b2a6705ca7d">Visual Studio Gallery</a>.</p>

<img alt="ASP.NET-MVC-Boilerplate Logo"
     border="5"
     src="https://github.com/RehanSaeed/ASP.NET-MVC-Boilerplate/blob/master/Images/Preview%20Image.png"/>

<h2>Why Do I Need It?</h2>

<p>The default MVC template that Visual Studio gives you does not make best use of the tools available. It's insecure, slow, and really has a very basic feature list (That's the point of it). ASP.NET MVC Boilerplate provides you with a few more pieces of the puzzle to get you started quicker. It makes liberal use of comments and even gives you a checklist of tasks which you need to perform to make it even better. The main benefits of using this template are:</p>

<ul>
     <li>Security</li>
     <li>Performance</li>
     <li>Search Engine Optimization (SEO)</li>
     <li>Browser Compatibility</li>
     <li>Resilience and Error Handling</li>
     <li>Easier Debugging and Performance Testing Tools</li>
     <li>Patterns and Practices</li>
</ul>

<img alt="ASP.NET-MVC-Boilerplate Logo"
     border="5"
     src="https://github.com/RehanSaeed/ASP.NET-MVC-Boilerplate/blob/master/Images/Technology%20Map.png"/>

<h2>Secure By Default</h2>

<p>The default MVC template is not as secure as it could be. There are various settings (Mostly in the web.config file) which are insecure by default. For example, it leaks information about which version of IIS you are using and allows external scripts to access cookies by default!</p>

<p>ASP.NET MVC Boilerplate makes everything secure by default but goes further and uses various HTTP headers which are sent to the browser to restrict things further.</p>

<p>It also makes use of the new <a href="https://developer.mozilla.org/en-US/docs/Web/Security/CSP/Introducing_Content_Security_Policy">Content Security Policy (CSP)</a> HTTP Header using the <a href="https://nwebsec.codeplex.com/">NWebSec</a> NuGet packages. CSP revolutionizes web security and I highly recommend reading the above link.</p>

<h2>Fast By Default</h2>

<p>The default MVC template does a pretty poor job in the performance department. Probably because they don't make any assumptions about which web server you are using. Most of the world and dog that are writing ASP.NET MVC sites use IIS and there are settings in the web.config file under the system.webServer section which can make a big difference when it comes to performance.</p>

<p>ASP.NET MVC Boilerplate makes no such assumptions. It turns on GZip compression for static and dynamic files being sent to the browsers making them smaller and quicker to download. It also uses Content Delivery Networks (CDN) by default to make common scripts like jQuery quicker to download (You can turn this off of course but the point is ASP.NET MVC Boilerplate is fast by default).</p>

<p>That's not all! There are a bunch of other tweaks and examples of practices which can help improve the performance of the site. ASP.NET MVC Boilerplate achieves a score of <em>96/100</em> on <a href="http://yslow.org/">YSlow</a> (Its not possible to get the full 100 as some of it's criteria contradict each other and site scripts need to be moved to a CDN which you need to do yourself).</p>

<h2>Search Engine Optimization (SEO)</h2>

<p>The default ASP.NET MVC template takes no consideration of Search Engine Optimization at all. ASP.NET MVC Boilerplate adds a robots.txt file to tell search engines which pages they can index. It also adds a sitemap.xml file where you can add links to all your pages programmatically.</p>

<p>ASP.NET MVC has some very useful settings for appending trailing slashes to URL's and making all URL's lower case. Unfortunately, both of these are turned off by default, which is terrible for SEO. This project turns them on by default.</p>

<h2>Browser Compatibility</h2>

<p>Websites need to reach as many people as possible and look good on a range of different devices. ASP.NET MVC Boilerplate supports browsers as old as IE8 (IE8 still has around 4% market share and is mostly used by corporations too lazy to port their old websites to newer browsers).</p>

<p>ASP.NET MVC Boilerplate also supports devices other than desktop browsers as much as possible. It has default icons and splash screens for Windows 8, Android, Apple Devices and a few other device specific settings included by default.</p>

<h2>Resilience and Error Handling</h2>

<p>At some point your site is probably going to throw an exception and you will need to handle and log that exception to be able to understand and fix it. ASP.NET MVC Boilerplate includes <a href="https://code.google.com/p/elmah/">Elmah</a>, the popular error logging addin by default. It's all preconfigured and ready to use.</p>

<p>ASP.NET MVC Boilerplate uses popular Content Delivery Networks (CDN) from Google and Microsoft but what happens in the unlikely event that these go down? Well, ASP.NET MVC Boilerplate provides backups for these.</p>

<h2>Easier Debugging and Performance Testing Tools</h2>

<p>ASP.NET MVC Boilerplate makes use of <a href="http://getglimpse.com/">Glimpse</a> (As advertised by Scott Hansleman <a href="http://www.hanselman.com/blog/IfYoureNotUsingGlimpseWithASPNETForDebuggingAndProfilingYoureMissingOut.aspx">here</a>). It's a great tool to use as you are developing, to find performance problems and bugs. Of course, Glimpse is all preconfigured, so you don't need to lift a finger to install it.</p>

<h2>Patterns and Practices</h2>

<p>Doing things right does sometimes take a little extra time. Using the <a href="http://martinfowler.com/articles/injection.html">Inversion of Control (IOC)</a> pattern for example should be a default. ASP.NET MVC Boilerplate uses the <a href="http://autofac.org/">Autofac</a> IOC container by default. Some people get a bit tribal when talking about IOC containers but to be honest, they all work great. Autofac was picked because it has lots of helpers for ASP.NET MVC and Microsoft even uses it for Azure Mobile Services.</p>

<p>ASP.NET MVC Boilerplate also makes use of the popular <a href="http://lesscss.org/">Less</a> files for making life easier with CSS. For an example, it can make overriding colours and fonts in the default Bootstrap CSS a cinch.</p>

<p>ASP.NET MVC is a complicated beast. You can end up with lots of <a href="http://en.wikipedia.org/wiki/Magic_string">magic strings</a> which can be a nightmare when renaming something. There are many ways of eliminating these magic strings but most trade maintainability for slower performance. ASP.NET MVC Boilerplate makes extensive use of constants which are a trade-off between maintainability and performance, giving you the best of both worlds.</p>

<h2>How can I get it?</h2>
That's easy, just choose one of the following options:
<ol>
  <li>Get the Visual Studio extension <a href="https://visualstudiogallery.msdn.microsoft.com/6cf50a48-fc1e-4eaf-9e82-0b2a6705ca7d">here</a> and in Visual Studio go to File -> New Project -> Web.</li>
  <li>
  Clone the git repository
  <blockquote>git clone https://github.com/RehanSaeed/ASP.NET-MVC-Boilerplate</blockquote>
  </li>
</ol>
