namespace MvcBoilerplate.Controllers
{
    using System.Diagnostics;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Boilerplate.Web.Mvc;
    using Boilerplate.Web.Mvc.Filters;
    using Microsoft.AspNet.Mvc;
    using MvcBoilerplate.Constants;
    using MvcBoilerplate.Services;

    public class HomeController : Controller
    {
#if DNX451
        // The FeedService is not available for .NET Core because the System.ServiceModel.Syndication.SyndicationFeed 
        // type does not yet exist. See https://github.com/dotnet/wcf/issues/76.
        private readonly IFeedService feedService;
#endif
        private readonly IOpenSearchService openSearchService;
        private readonly IRobotsService robotsService;
        private readonly ISitemapService sitemapService;

        #region Constructors

        public HomeController(
#if DNX451
            // The FeedService is not available for .NET Core because the System.ServiceModel.Syndication.SyndicationFeed 
            // type does not yet exist. See https://github.com/dotnet/wcf/issues/76.
            IFeedService feedService,
#endif
            IOpenSearchService openSearchService,
            IRobotsService robotsService,
            ISitemapService sitemapService)
        {
#if DNX451
            // The FeedService is not available for .NET Core because the System.ServiceModel.Syndication.SyndicationFeed 
            // type does not yet exist. See https://github.com/dotnet/wcf/issues/76.
            this.feedService = feedService;
#endif
            this.openSearchService = openSearchService;
            this.robotsService = robotsService;
            this.sitemapService = sitemapService;
        }

        #endregion

        [HttpGet("", Name = HomeControllerRoute.GetIndex)]
        public IActionResult Index()
        {
            return this.View(HomeControllerAction.Index);
        }

        [HttpGet("about", Name = HomeControllerRoute.GetAbout)]
        public IActionResult About()
        {
            return this.View(HomeControllerAction.About);
        }

        [HttpGet("contact", Name = HomeControllerRoute.GetContact)]
        public IActionResult Contact()
        {
            return this.View(HomeControllerAction.Contact);
        }

#if DNX451
        // The FeedService is not available for .NET Core because the System.ServiceModel.Syndication.SyndicationFeed 
        // type does not yet exist. See https://github.com/dotnet/wcf/issues/76.

        /// <summary>
        /// Gets the Atom 1.0 feed for the current site. Note that Atom 1.0 is used over RSS 2.0 because Atom 1.0 is a 
        /// newer and more well defined format. Atom 1.0 is a standard and RSS is not. See
        /// http://rehansaeed.com/building-rssatom-feeds-for-asp-net-mvc/
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> signifying if the request is cancelled.
        /// See http://www.davepaquette.com/archive/2015/07/19/cancelling-long-running-queries-in-asp-net-mvc-and-web-api.aspx</param>
        /// <returns>The Atom 1.0 feed for the current site.</returns>
        [ResponseCache(CacheProfileName = CacheProfileName.Feed)]
        [Route("feed", Name = HomeControllerRoute.GetFeed)]
        public async Task<IActionResult> Feed(CancellationToken cancellationToken)
        {
            return new AtomActionResult(await this.feedService.GetFeed(cancellationToken));
        }
#endif

        [Route("search", Name = HomeControllerRoute.GetSearch)]
        public IActionResult Search(string query)
        {
            // You can implement a proper search function here and add a Search.cshtml page.
            // return this.View(HomeControllerAction.Search);

            // Or you could use Google Custom Search (https://cse.google.co.uk/cse) to index your site and display your 
            // search results in your own page.

            // For simplicity we are just assuming your site is indexed on Google and redirecting to it.
            return this.Redirect(string.Format(
                "https://www.google.co.uk/?q=site:{0} {1}",
                this.Url.AbsoluteRouteUrl(HomeControllerRoute.GetIndex),
                query));
        }

        /// <summary>
        /// Gets the Open Search XML for the current site. You can customize the contents of this XML here. The open 
        /// search action is cached for one day, adjust this time to whatever you require. See
        /// http://www.hanselman.com/blog/CommentView.aspx?guid=50cc95b1-c043-451f-9bc2-696dc564766d#commentstart
        /// http://www.opensearch.org
        /// </summary>
        /// <returns>The Open Search XML for the current site.</returns>
        [NoTrailingSlash]
        [ResponseCache(CacheProfileName = CacheProfileName.OpenSearchXml)]
        [Route("opensearch.xml", Name = HomeControllerRoute.GetOpenSearchXml)]
        public IActionResult OpenSearchXml()
        {
            Trace.WriteLine(string.Format(
                "opensearch.xml requested. User Agent:<{0}>.",
                this.Request.Headers.Get("User-Agent")));
            string content = this.openSearchService.GetOpenSearchXml();
            return this.Content(content, ContentType.Xml, Encoding.UTF8);
        }

        /// <summary>
        /// Tells search engines (or robots) how to index your site. 
        /// The reason for dynamically generating this code is to enable generation of the full absolute sitemap URL
        /// and also to give you added flexibility in case you want to disallow search engines from certain paths. The 
        /// sitemap is cached for one day, adjust this time to whatever you require. See
        /// http://en.wikipedia.org/wiki/Robots_exclusion_standard
        /// </summary>
        /// <returns>The robots text for the current site.</returns>
        [NoTrailingSlash]
        [ResponseCache(CacheProfileName = CacheProfileName.RobotsText)]
        [Route("robots.txt", Name = HomeControllerRoute.GetRobotsText)]
        public IActionResult RobotsText()
        {
            Trace.WriteLine(string.Format(
                "robots.txt requested. User Agent:<{0}>.",
                this.Request.Headers.Get("User-Agent")));
            string content = this.robotsService.GetRobotsText();
            return this.Content(content, ContentType.Text, Encoding.UTF8);
        }

        /// <summary>
        /// Gets the sitemap XML for the current site. You can customize the contents of this XML from the 
        /// <see cref="SitemapService"/>. The sitemap is cached for one day, adjust this time to whatever you require.
        /// http://www.sitemaps.org/protocol.html
        /// </summary>
        /// <param name="index">The index of the sitemap to retrieve. <c>null</c> if you want to retrieve the root 
        /// sitemap file, which may be a sitemap index file.</param>
        /// <returns>The sitemap XML for the current site.</returns>
        [NoTrailingSlash]
        [Route("sitemap.xml", Name = HomeControllerRoute.GetSitemapXml)]
        public IActionResult SitemapXml(int? index = null)
        {
            string content = this.sitemapService.GetSitemapXml(index);

            if (content == null)
            {
                return this.HttpBadRequest("Sitemap index is out of range.");
            }

            return this.Content(content, ContentType.Xml, Encoding.UTF8);
        }
    }
}
