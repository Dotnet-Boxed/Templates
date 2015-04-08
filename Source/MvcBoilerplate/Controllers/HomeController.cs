namespace MvcBoilerplate.Controllers
{
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Web.Mvc;
    using System.Xml.Linq;
    using MvcBoilerplate.Constants;
    using MvcBoilerplate.Framework;
    using MvcBoilerplate.Services;

    public class HomeController : Controller
    {
        private readonly ISitemapService sitemapService;

        #region Constructors

        public HomeController(ISitemapService sitemapService)
        {
            this.sitemapService = sitemapService;
        }

        #endregion

        [Route("", Name = HomeControllerRoute.GetIndex)]
        public ActionResult Index()
        {
            return this.View(HomeControllerAction.Index);
        }

        [Route("about", Name = HomeControllerRoute.GetAbout)]
        public ActionResult About()
        {
            return this.View(HomeControllerAction.About);
        }

        [Route("contact", Name = HomeControllerRoute.GetContact)]
        public ActionResult Contact()
        {
            return this.View(HomeControllerAction.Contact);
        }

        [Route("search", Name = HomeControllerRoute.GetSearch)]
        public ActionResult Search(string query)
        {
            // You can implement a proper search function here and add a Search.cshtml page.
            // return this.View(HomeControllerAction.Search);

            // Or you could use Google Custom Search (https://cse.google.co.uk/cse) to index your site 
            // and display your search results in your own page.

            // For simplicity we are just assuming your site is indexed on Google and redirecting to it.
            return this.Redirect(string.Format(
                "https://www.google.co.uk/?q=site:{0} {1}", 
                this.Url.AbsoluteRouteUrl(HomeControllerRoute.GetIndex),
                query));
        }

        /// <summary>
        /// Gets the Open Search XML for the current site. You can customize the contents of this XML here.
        /// The open search action is cached for one day, adjust this time to whatever you require.
        /// See http://www.hanselman.com/blog/CommentView.aspx?guid=50cc95b1-c043-451f-9bc2-696dc564766d#commentstart 
        /// and http://www.opensearch.org for more information.
        /// </summary>
        /// <returns>The Open Search XML for the current site.</returns>
        [OutputCache(CacheProfile = CacheProfileName.OpenSearchXml)]
        [Route("opensearch.xml", Name = HomeControllerRoute.OpenSearchXml)]
        public ActionResult OpenSearchXml()
        {
            Trace.WriteLine(string.Format("opensearch.xml requested. User Agent:<{0}>.", this.Request.Headers.Get("User-Agent")));

            // Short name must be less than or equal to 16 characters.
            string shortName = "Search";
            string description = "Search the ASP.NET MVC Boilerplate Site";
            // The link to the search page with the query string set to 'searchTerms' which gets replaced with a user defined query.
            string searchUrl = this.Url.AbsoluteRouteUrl(HomeControllerRoute.GetSearch, new { query = "{searchTerms}" });
            // The link to the page with the search form on it. The home page has the search form on it.
            string searchFormUrl = this.Url.AbsoluteRouteUrl(HomeControllerRoute.GetIndex);
            // The link to the favicon.ico file for the site.
            string favicon16Url = this.Url.AbsoluteRouteUrl(HomeControllerRoute.GetIndex) + "content/icons/favicon.ico";
            // The link to the favicon.png file for the site.
            string favicon32Url = this.Url.AbsoluteRouteUrl(HomeControllerRoute.GetIndex) + "content/icons/favicon-32x32.png";
            // The link to the favicon.png file for the site.
            string favicon96Url = this.Url.AbsoluteRouteUrl(HomeControllerRoute.GetIndex) + "content/icons/favicon-96x96.png";

            XNamespace ns = "http://a9.com/-/spec/opensearch/1.1";
            XDocument document = new XDocument(
                new XElement(ns + "OpenSearchDescription",
                    new XElement(ns + "ShortName", shortName),
                    new XElement(ns + "Description", description),
                    new XElement(ns + "Url",
                        new XAttribute("type", "text/html"),
                        new XAttribute("method", "get"),
                        new XAttribute("template", searchUrl)),
                    // Search results can also be returned as RSS. Here, our start index is zero for the first result.
                    // new XElement(ns + "Url",
                    //     new XAttribute("type", "application/rss+xml"),
                    //     new XAttribute("indexOffset", "0"),
                    //     new XAttribute("rel", "results"),
                    //     new XAttribute("template", "http://example.com/?q={searchTerms}&amp;start={startIndex?}&amp;format=rss")),
                    // Search suggestions can also be returned as JSON.
                    // new XElement(ns + "Url",
                    //     new XAttribute("type", "application/json"),
                    //     new XAttribute("indexOffset", "0"),
                    //     new XAttribute("rel", "suggestions"),
                    //     new XAttribute("template", "http://example.com/suggest?q={searchTerms}")),
                    new XElement(ns + "Image", favicon16Url,
                        new XAttribute("height", "16"),
                        new XAttribute("width", "16"),
                        new XAttribute("type", "image/x-icon")),
                    new XElement(ns + "Image", favicon32Url,
                        new XAttribute("height", "32"),
                        new XAttribute("width", "32"),
                        new XAttribute("type", "image/png")),
                    new XElement(ns + "Image", favicon96Url,
                        new XAttribute("height", "96"),
                        new XAttribute("width", "96"),
                        new XAttribute("type", "image/png")),
                    new XElement(ns + "InputEncoding", "UTF-8"),
                    new XElement(ns + "SearchForm", searchFormUrl)));

            StringBuilder stringBuilder = new StringBuilder();
            using (StringWriter stringWriter = new StringWriterWithEncoding(stringBuilder, Encoding.UTF8))
            {
                document.Save(stringWriter);
            }

            return this.Content(stringBuilder.ToString(), ContentType.Xml, Encoding.UTF8);
        } 

        /// <summary>
        /// Tells search engines (or robots) how to index your site. 
        /// The reason for dynamically generating this code is to enable generation of the full absolute sitemap URL
        /// and also to give you added flexibility in case you want to disallow search engines from certain paths.
        /// The sitemap is cached for one day, adjust this time to whatever you require.
        /// See http://en.wikipedia.org/wiki/Robots_exclusion_standard for more information.
        /// </summary>
        /// <returns></returns>
        [OutputCache(CacheProfile = CacheProfileName.RobotsText)]
        [Route("robots.txt", Name = HomeControllerRoute.RobotsText)]
        public ActionResult RobotsText()
        {
            Trace.WriteLine(string.Format("robots.txt requested. User Agent:<{0}>.", this.Request.Headers.Get("User-Agent")));

            StringBuilder stringBuilder = new StringBuilder();

            // Allow all robots.
            stringBuilder.AppendLine("user-agent: *");

            // Tell all robots not to index any directories.
            // stringBuilder.AppendLine("disallow: /");

            // Tell all robots not to index everything under the following directory.
            // stringBuilder.AppendLine("disallow: /SomeRelativePath");

            // Tell all robots they can visit everything under the following sub-directory, even if the parent directory is disallowed.
            // stringBuilder.AppendLine("allow: /SomeRelativePath/SomeSubDirectory");

            // SECURITY ALERT - BE CAREFUL WHAT YOU ADD HERE
            // The line below stops all robots from indexing the following secret folder.
            // For example, this could be your Elmah error logs. Very useful to any hacker.
            // You should be securing these pages using some form of authentication but
            // hiding where these things are can help through a bit of security through obscurity.
            // stringBuilder.AppendLine("disallow: /MySecretStuff");

            // Add a link to the sitemap. Unfortunately this must be an absolute URL. 
            stringBuilder.Append("sitemap: ");
            stringBuilder.AppendLine(this.Url.AbsoluteRouteUrl(HomeControllerRoute.SitemapXml));

            return this.Content(
                stringBuilder.ToString(),
                ContentType.Text,
                Encoding.UTF8);
        }

        /// <summary>
        /// Gets the sitemal XML for the current site. You can customize the contents of 
        /// this XML from the <see cref="SitemapService"/>. 
        /// The sitemap is cached for one day, adjust this time to whatever you require.
        /// See http://www.sitemaps.org/protocol.html for more information.
        /// </summary>
        /// <returns>The sitemap XML for the current site.</returns>
        [OutputCache(CacheProfile = CacheProfileName.SitemapXml)]
        [Route("sitemap.xml", Name = HomeControllerRoute.SitemapXml)]
        public ActionResult SitemapXml()
        {
            Trace.WriteLine(string.Format("sitemap.xml requested. User Agent:<{0}>.", this.Request.Headers.Get("User-Agent")));

            string content = this.sitemapService.GetSitemapXml();
            return this.Content(content, ContentType.Xml, Encoding.UTF8);
        }  
    }
}