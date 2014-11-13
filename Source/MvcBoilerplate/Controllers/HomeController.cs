namespace MvcBoilerplate.Controllers
{
    using System.Diagnostics;
    using System.Text;
    using System.Web.Mvc;
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