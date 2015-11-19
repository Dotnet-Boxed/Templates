namespace MvcBoilerplate.Services
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Boilerplate.Web.Mvc;
    // $Start-ApplicationInsights$
    using Microsoft.ApplicationInsights;
    // $End-ApplicationInsights$
    using Microsoft.AspNet.Hosting;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.OptionsModel;
    using MvcBoilerplate.Constants;
    using MvcBoilerplate.Settings;

    public class SitemapPingerService : ISitemapPingerService
    {
        #region Fields

        private readonly HttpClient httpClient;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ILogger<SitemapPingerService> logger;
        private readonly IOptions<SitemapSettings> sitemapSettings;
        // $Start-ApplicationInsights$
        private readonly TelemetryClient telemetryClient;
        // $End-ApplicationInsights$
        private readonly IUrlHelper urlHelper;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SitemapPingerService"/> class.
        /// </summary>
        /// <param name="hostingEnvironment">The environment the application is running under. This can be Development, 
        /// Staging or Production by default.</param>
        /// <param name="logger">The <see cref="SitemapPingerService"/> logger.</param>
        /// <param name="sitemapSettings">The sitemap settings.</param>
        // $Start-ApplicationInsights$
        /// <param name="telemetryClient">The Azure Application Insights telemetry client.</param>
        // $End-ApplicationInsights$
        /// <param name="urlHelper">The URL helper.</param>
        public SitemapPingerService(
            IHostingEnvironment hostingEnvironment,
            ILogger<SitemapPingerService> logger,
            IOptions<SitemapSettings> sitemapSettings,
            // $Start-ApplicationInsights$
            TelemetryClient telemetryClient,
            // $End-ApplicationInsights$
            IUrlHelper urlHelper)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
            this.sitemapSettings = sitemapSettings;
            // $Start-ApplicationInsights$
            this.telemetryClient = telemetryClient;
            // $End-ApplicationInsights$
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
        public async Task PingSearchEngines()
        {
            // $Start-ApplicationInsights$
            this.telemetryClient.TrackEvent("PingSitemapToSearchEngines");
            // $End-ApplicationInsights$

            if (this.hostingEnvironment.IsProduction())
            {
                foreach (string sitemapPingLocation in this.sitemapSettings.Value.SitemapPingLocations)
                {
                    string sitemapUrl = this.urlHelper.AbsoluteRouteUrl(HomeControllerRoute.GetSitemapXml).TrimEnd('/');
                    string url = sitemapPingLocation + WebUtility.UrlEncode(sitemapUrl);
                    HttpResponseMessage response = await this.httpClient.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        HttpRequestException exception = new HttpRequestException(string.Format(
                            CultureInfo.InvariantCulture,
                            "Pinging search engine {0}. Response status code does not indicate success: {1} ({2}).",
                            url,
                            (int)response.StatusCode,
                            response.ReasonPhrase));
                        // $Start-ApplicationInsights$
                        this.telemetryClient.TrackException(exception, new Dictionary<string, string>() { { "Url", url } });
                        // $End-ApplicationInsights$
                        this.logger.LogError("Error while pinging site-map to search engines.", exception);
                    }
                }
            }
        }

        #endregion
    }
}