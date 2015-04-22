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
        private readonly IFeedService feedService;
        private readonly IOpenSearchService openSearchService;
        private readonly IRobotsService robotsService;
        private readonly ISitemapService sitemapService;

        #region Constructors

        public HomeController(
            IFeedService feedService,
            IOpenSearchService openSearchService,
            IRobotsService robotsService,
            ISitemapService sitemapService)
        {
            this.feedService = feedService;
            this.openSearchService = openSearchService;
            this.robotsService = robotsService;
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
        /// Gets the Atom 1.0 feed for the current site. Note that Atom 1.0 is used over RSS 2.0
        /// because Atom 1.0 is a newer and more well defined format. Atom 1.0 is a standard and RSS is not. 
        /// (See http://www.intertwingly.net/wiki/pie/Rss20AndAtom10Compared).
        /// </summary>
        /// <returns>The Atom 1.0 feed for the current site.</returns>
        [OutputCache(CacheProfile = CacheProfileName.Feed)]
        [Route("feed", Name = HomeControllerRoute.GetFeed)]
        public ActionResult Feed()
        {
            return new AtomActionResult(this.feedService.GetFeed());
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
        /// See <see cref="http://www.hanselman.com/blog/CommentView.aspx?guid=50cc95b1-c043-451f-9bc2-696dc564766d#commentstart"/> 
        /// and <see cref="http://www.opensearch.org"/> for more information.
        /// </summary>
        /// <returns>The Open Search XML for the current site.</returns>
        [NoTrailingSlash]
        [OutputCache(CacheProfile = CacheProfileName.OpenSearchXml)]
        [Route("opensearch.xml", Name = HomeControllerRoute.GetOpenSearchXml)]
        public ContentResult OpenSearchXml()
        {
            Trace.WriteLine(string.Format("opensearch.xml requested. User Agent:<{0}>.", this.Request.Headers.Get("User-Agent")));
            string content = this.openSearchService.GetOpenSearchXml();
            return this.Content(content, ContentType.Xml, Encoding.UTF8);
        } 

        /// <summary>
        /// Tells search engines (or robots) how to index your site. 
        /// The reason for dynamically generating this code is to enable generation of the full absolute sitemap URL
        /// and also to give you added flexibility in case you want to disallow search engines from certain paths.
        /// The sitemap is cached for one day, adjust this time to whatever you require.
        /// See <see cref="http://en.wikipedia.org/wiki/Robots_exclusion_standard"/> for more information.
        /// </summary>
        /// <returns>The robots text for the current site.</returns>
        [NoTrailingSlash]
        [OutputCache(CacheProfile = CacheProfileName.RobotsText)]
        [Route("robots.txt", Name = HomeControllerRoute.GetRobotsText)]
        public ContentResult RobotsText()
        {
            Trace.WriteLine(string.Format("robots.txt requested. User Agent:<{0}>.", this.Request.Headers.Get("User-Agent")));
            string content = this.robotsService.GetRobotsText();
            return this.Content(content, ContentType.Text, Encoding.UTF8);
        }

        /// <summary>
        /// Gets the sitemal XML for the current site. You can customize the contents of 
        /// this XML from the <see cref="SitemapService"/>. 
        /// The sitemap is cached for one day, adjust this time to whatever you require.
        /// See <see cref="http://www.sitemaps.org/protocol.html"/> for more information.
        /// </summary>
        /// <returns>The sitemap XML for the current site.</returns>
        [NoTrailingSlash]
        [OutputCache(CacheProfile = CacheProfileName.SitemapXml)]
        [Route("sitemap.xml", Name = HomeControllerRoute.GetSitemapXml)]
        public ContentResult SitemapXml()
        {
            Trace.WriteLine(string.Format("sitemap.xml requested. User Agent:<{0}>.", this.Request.Headers.Get("User-Agent")));
            string content = this.sitemapService.GetSitemapXml();
            return this.Content(content, ContentType.Xml, Encoding.UTF8);
        }  
    }
}