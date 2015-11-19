namespace MvcBoilerplate.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Boilerplate.Web.Mvc;
    using Boilerplate.Web.Mvc.Caching;
    using Boilerplate.Web.Mvc.Sitemap;
    // $Start-ApplicationInsights$
    using Microsoft.ApplicationInsights;
    // $End-ApplicationInsights$
    using Microsoft.AspNet.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.OptionsModel;
    using MvcBoilerplate.Constants;
    using MvcBoilerplate.Settings;

    /// <summary>
    /// Generates sitemap XML for the current site.
    /// </summary>
    public class SitemapService : SitemapGenerator, ISitemapService
    {
        #region Fields

        private readonly IDistributedCache distributedCache;
        private readonly ILogger<SitemapService> logger;
        // $Start-ApplicationInsights$
        private readonly TelemetryClient telemetryClient;
        // $End-ApplicationInsights$
        private readonly IUrlHelper urlHelper;

        private readonly TimeSpan cacheSlidingExpiration;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SitemapService" /> class.
        /// </summary>
        /// <param name="cacheProfileSettings">The cache profile settings.</param>
        /// <param name="distributedCache">The distributed cache for the application.</param>
        /// <param name="logger">The <see cref="SitemapService"/> logger.</param>
        // $Start-ApplicationInsights$
        /// <param name="telemetryClient">The Azure Application Insights telemetry client.</param>
        // $End-ApplicationInsights$
        /// <param name="urlHelper">The URL helper.</param>
        public SitemapService(
            IOptions<CacheProfileSettings> cacheProfileSettings,
            IDistributedCache distributedCache,
            ILogger<SitemapService> logger,
            // $Start-ApplicationInsights$
            TelemetryClient telemetryClient,
            // $End-ApplicationInsights$
            IUrlHelper urlHelper)
        {
            CacheProfile cacheProfile = cacheProfileSettings.Value.CacheProfiles[CacheProfileName.SitemapNodes];
            this.cacheSlidingExpiration = TimeSpan.FromSeconds(cacheProfile.Duration.Value);
            this.distributedCache = distributedCache;
            this.logger = logger;
            // $Start-ApplicationInsights$
            this.telemetryClient = telemetryClient;
            // $End-ApplicationInsights$
            this.urlHelper = urlHelper;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the sitemap XML for the current site. If an index of null is passed and there are more than 25,000 
        /// sitemap nodes, a sitemap index file is returned (A sitemap index file contains links to other sitemap files 
        /// and is a way of splitting up your sitemap into separate files). If an index is specified, a standard 
        /// sitemap is returned for the specified index parameter. See http://www.sitemaps.org/protocol.html
        /// </summary>
        /// <param name="index">The index of the sitemap to retrieve. <c>null</c> if you want to retrieve the root 
        /// sitemap or sitemap index document, depending on the number of sitemap nodes.</param>
        /// <returns>The sitemap XML for the current site or <c>null</c> if the sitemap index is out of range.</returns>
        public async Task<string> GetSitemapXml(int? index = null)
        {
            // Here we are caching the entire set of sitemap documents. We cannot use the caching attribute because 
            // cache expiry could get out of sync if the number of sitemaps changes.
            List<string> sitemapDocuments;
            var tuple = await this.distributedCache.TryGetAsJsonAsync<List<string>>(CacheProfileName.SitemapNodes);
            if (tuple.Item1)
            {
                sitemapDocuments = tuple.Item2;
            }
            else
            {
                IReadOnlyCollection<SitemapNode> sitemapNodes = this.GetSitemapNodes();
                sitemapDocuments = this.GetSitemapDocuments(sitemapNodes);
                await this.distributedCache.SetAsJsonAsync(
                    CacheProfileName.SitemapNodes,
                    sitemapDocuments,
                    new DistributedCacheEntryOptions()
                    {
                        SlidingExpiration = cacheSlidingExpiration
                    });
            }

            if (index.HasValue && ((index < 1) || (index.Value >= sitemapDocuments.Count)))
            {
                return null;
            }

            return sitemapDocuments[index.HasValue ? index.Value : 0];
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets a collection of sitemap nodes for the current site.
        /// TODO: Add code here to create nodes to all your important sitemap URL's.
        /// You may want to do this from a database or in code.
        /// </summary>
        /// <returns>A collection of sitemap nodes for the current site.</returns>
        protected virtual IReadOnlyCollection<SitemapNode> GetSitemapNodes()
        {
            List<SitemapNode> nodes = new List<SitemapNode>();

            nodes.Add(
                new SitemapNode(this.urlHelper.AbsoluteRouteUrl(HomeControllerRoute.GetIndex))
                {
                    Priority = 1
                });
            // $Start-AboutPage$
            nodes.Add(
               new SitemapNode(this.urlHelper.AbsoluteRouteUrl(HomeControllerRoute.GetAbout))
               {
                   Priority = 0.9
               });
            // $End-AboutPage$
            // $Start-ContactPage$
            nodes.Add(
               new SitemapNode(this.urlHelper.AbsoluteRouteUrl(HomeControllerRoute.GetContact))
               {
                   Priority = 0.9
               });
            // $End-ContactPage$

            // An example of how to add many pages into your sitemap.
            // foreach (int productId in myProductRepository.GetProductIds())
            // {
            //     nodes.Add(
            //        new SitemapNode(this.urlHelper.AbsoluteRouteUrl(ProductControllerRoute.GetProduct, new { id = productId }))
            //        {
            //            Frequency = SitemapFrequency.Weekly,
            //            LastModified = DateTime.Now,
            //            Priority = 0.8
            //        });
            // }

            return nodes;
        }

        protected override string GetSitemapUrl(int index)
        {
            return this.urlHelper.AbsoluteRouteUrl(HomeControllerRoute.GetSitemapXml).TrimEnd('/') + "?index=" + index;
        }

        protected override void LogWarning(Exception exception)
        {
            // $Start-ApplicationInsights$
            this.telemetryClient.TrackException(exception);
            // $End-ApplicationInsights$
            this.logger.LogWarning(exception.Message, exception);
        }

        #endregion
    }
}