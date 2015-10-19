namespace MvcBoilerplate.Services
{
    using System.Globalization;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Boilerplate.Web.Mvc;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Framework.Logging;
    using MvcBoilerplate.Constants;

    public class SitemapPingerService : ISitemapPingerService
    {
        #region Fields

        /// <summary>
        /// The URL's provided by search engines where we can send the location of our sitemap.
        /// </summary>
        private static readonly string[] SitemapPingLocations = new string[]
        {
            // Google
            "https://www.google.com/ping?sitemap=",
            // Bing and Yahoo share the same sitemap ping URL.
            "http://www.bing.com/ping?sitemap="
        };

        private readonly HttpClient httpClient;
        private readonly ILogger<SitemapPingerService> logger;
        private readonly IUrlHelper urlHelper;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SitemapPingerService"/> class.
        /// </summary>
        /// <param name="logger">The <see cref="SitemapPingerService"/> logger.</param>
        /// <param name="urlHelper">The URL helper.</param>
        public SitemapPingerService(
            ILogger<SitemapPingerService> logger,
            IUrlHelper urlHelper)
        {
            this.logger = logger;
            this.urlHelper = urlHelper;
            this.httpClient = new HttpClient();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Send (or 'ping') the URL of this sites sitemap.xml file to search engines like Google, Bing and Yahoo, 
        /// This method should be called each time the sitemap changes. Google says that 'We recommend that you 
        /// resubmit a Sitemap no more than once per hour.' The way we 'ping' our sitemap to search engines is 
        /// actually an open standard See 
        /// http://www.sitemaps.org/protocol.html#submit_ping
        /// You can read the sitemap ping documentation for the top search engines below:
        /// Google - http://googlewebmastercentral.blogspot.co.uk/2014/10/best-practices-for-xml-sitemaps-rssatom.html
        /// Bing - http://www.bing.com/webmaster/help/how-to-submit-sitemaps-82a15bd4.
        /// Yahoo - https://developer.yahoo.com/search/siteexplorer/V1/ping.html
        /// </summary>
#if Release
        public async Task PingSearchEngines()
        {
            foreach (string sitemapPingLocation in SitemapPingLocations)
            {
                string url = sitemapPingLocation + 
                    this.urlHelper.Encode(this.urlHelper.AbsoluteRouteUrl(HomeControllerRoute.GetSitemapXml));
                HttpResponseMessage response = await this.httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    HttpRequestException exception = new HttpRequestException(string.Format(
                        CultureInfo.InvariantCulture,
                        "Pinging search engine {0}. Response status code does not indicate success: {1} ({2}).",
                        url,
                        (int)response.StatusCode,
                        response.ReasonPhrase));
                    this.logger.LogError("Error while pinging site-map to search engines.", exception)
                }
            }
        }
#else
        public Task PingSearchEngines()
        {
            return Task.FromResult<object>(null);
        }
#endif

        #endregion
    }
}