namespace $safeprojectname$.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Xml.Linq;
    using $safeprojectname$.Constants;
    using $safeprojectname$.Framework;

    /// <summary>
    /// Generates sitemap XML for the current site.
    /// </summary>
    public class SitemapService : ISitemapService
    {
        #region Fields

        private const string SitemapsNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";

        /// <summary>
        /// The maximum number of sitemaps a sitemap index file can contain.
        /// </summary>
        private const int MaximumSitemapCount = 50000;

        /// <summary>
        /// The maximum number of sitemap nodes allowed in a sitemap file. The absolute maximum allowed is 50,000 according to the specification
        /// (See <see cref="http://www.sitemaps.org/protocol.html"/>) but the file size must also be less than 10MB. After some experimentation, a maximum
        /// of 25,000 nodes keeps the file size below 10MB.
        /// </summary>
        private const int MaximumSitemapNodeCount = 25000;

        /// <summary>
        /// The maximum size of a sitemap file in bytes (10MB).
        /// </summary>
        private const int MaximumSitemapSizeInBytes = 10485760;

        private readonly ICacheService cacheService;
        private readonly ILoggingService loggingService;
        private readonly UrlHelper urlHelper;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SitemapService" /> class.
        /// </summary>
        /// <param name="cacheService">The cache service.</param>
        /// <param name="loggingService">The logging service.</param>
        /// <param name="urlHelper">The URL helper.</param>
        public SitemapService(
            ICacheService cacheService,
            ILoggingService loggingService,
            UrlHelper urlHelper)
        {
            this.cacheService = cacheService;
            this.loggingService = loggingService;
            this.urlHelper = urlHelper;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the sitemap XML for the current site. If an index of null is passed and there are more than <see cref="MaximumSitemapNodeCount"/> sitemap 
        /// nodes, a sitemap index file is returned (A sitemap index file contains links to other sitemap files and is a way of splitting up your sitemap 
        /// into separate files). If an index is specified, a standard sitemap is returned for the specified index parameter.
        /// See <see cref="http://www.sitemaps.org/protocol.html"/> for more information.
        /// </summary>
        /// <param name="index">The index of the sitemap to retrieve. <c>null</c> if you want to retrieve the root sitemap or sitemap index document, 
        /// depending on the number of sitemap nodes.</param>
        /// <returns>The sitemap XML for the current site.</returns>
        public string GetSitemapXml(int? index = null)
        {
            // Here we are caching the entire set of sitemap documents. We cannot use OutputCacheAttribute because cache expiry could get out of sync
            // if the number of sitemaps changes.
            List<string> sitemapDocuments = this.cacheService.GetOrAdd(
                CacheSetting.SitemapNodes.Key,
                () =>
                {
                    IReadOnlyCollection<SitemapNode> sitemapNodes = this.GetSitemapNodes();
                    return this.GetSitemapDocuments(sitemapNodes);
                },
                CacheSetting.SitemapNodes.SlidingExpiration);

            if (index.HasValue && ((index < 1) || (index.Value >= sitemapDocuments.Count)))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Sitemap index is out of range.");
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
            nodes.Add(
               new SitemapNode(this.urlHelper.AbsoluteRouteUrl(HomeControllerRoute.GetAbout))
               {
                   Priority = 2
               });
            nodes.Add(
               new SitemapNode(this.urlHelper.AbsoluteRouteUrl(HomeControllerRoute.GetContact))
               {
                   Priority = 2
               });

            // An example of how to add many pages into your sitemap.
            // foreach (Product product in myProductRepository.GetProducts())
            // {
            //     nodes.Add(
            //        new SitemapNode(this.urlHelper.AbsoluteRouteUrl(ProductControllerRoute.GetProduct, new { id = product.ProductId }))
            //        {
            //            Frequency = SitemapFrequency.Weekly,
            //            LastModified = DateTime.Now,
            //            Priority = 2
            //        });
            // }

            return nodes;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the collection of XML sitemap documents for the current site. If there are less than 25,000 sitemap nodes, only one sitemap
        /// document will exist in the collection, otherwise a sitemap index document will be the first entry in the collection and all other
        /// entries will be sitemap XML documents.
        /// </summary>
        /// <param name="sitemapNodes">The sitemap nodes for the current site.</param>
        /// <returns>A collection of XML sitemap documents.</returns>
        private List<string> GetSitemapDocuments(IReadOnlyCollection<SitemapNode> sitemapNodes)
        {
            int sitemapCount = (int)Math.Ceiling(sitemapNodes.Count / (double)MaximumSitemapNodeCount);
            this.CheckSitemapCount(sitemapCount);
            var sitemaps = Enumerable
                .Range(0, sitemapCount)
                .Select(x => 
                    {
                        return new KeyValuePair<int, IEnumerable<SitemapNode>>(
                            x + 1,
                            sitemapNodes.Skip(x * MaximumSitemapNodeCount).Take(MaximumSitemapNodeCount));
                    });

            List<string> sitemapDocuments = new List<string>(sitemapCount);

            if (sitemapCount > 1)
            {
                string xml = this.GetSitemapIndexDocument(sitemaps);
                sitemapDocuments.Add(xml);
            }

            foreach (KeyValuePair<int, IEnumerable<SitemapNode>> sitemap in sitemaps)
            {
                string xml = this.GetSitemapDocument(sitemap.Value);
                sitemapDocuments.Add(xml);
            }

            return sitemapDocuments;
        }

        /// <summary>
        /// Gets the sitemap index XML document, containing links to all the sitemap XML documents.
        /// </summary>
        /// <param name="sitemaps">The collection of sitemaps containing their index and nodes.</param>
        /// <returns>The sitemap index XML document, containing links to all the sitemap XML documents.</returns>
        private string GetSitemapIndexDocument(IEnumerable<KeyValuePair<int, IEnumerable<SitemapNode>>> sitemaps)
        {
            XNamespace xmlns = SitemapsNamespace;
            XElement root = new XElement(xmlns + "sitemapindex");

            foreach (KeyValuePair<int, IEnumerable<SitemapNode>> sitemap in sitemaps)
            {
                // Get the latest LastModified DateTime from the sitemap nodes or null if there is none.
                DateTime? lastModified = sitemap.Value
                    .Select(x => x.LastModified)
                    .Where(x => x.HasValue)
                    .DefaultIfEmpty()
                    .Max();
                
                XElement sitemapElement = new XElement(
                    xmlns + "sitemap",
                    new XElement(xmlns + "loc", this.urlHelper.AbsoluteRouteUrl(HomeControllerRoute.GetSitemapXml).TrimEnd('/') + "?index=" + sitemap.Key),
                    lastModified.HasValue ? new XElement(xmlns + "lastmod", lastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")) : null);

                root.Add(sitemapElement);
            }

            XDocument document = new XDocument(root);
            string xml = document.ToString(Encoding.UTF8);
            this.CheckDocumentSize(xml);
            return xml;
        }

        /// <summary>
        /// Gets the sitemap XML document for the specified set of nodes.
        /// </summary>
        /// <param name="sitemapNodes">The sitemap nodes.</param>
        /// <returns>The sitemap XML document for the specified set of nodes.</returns>
        private string GetSitemapDocument(IEnumerable<SitemapNode> sitemapNodes)
        {
            XNamespace xmlns = SitemapsNamespace;
            XElement root = new XElement(xmlns + "urlset");

            foreach (SitemapNode sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                    xmlns + "url",
                    new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.Url)),
                    sitemapNode.LastModified == null ? null : new XElement(xmlns + "lastmod", sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    sitemapNode.Frequency == null ? null : new XElement(xmlns + "changefreq", sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
                    sitemapNode.Priority == null ? null : new XElement(xmlns + "priority", sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
                root.Add(urlElement);
            }

            XDocument document = new XDocument(root);
            string xml = document.ToString(Encoding.UTF8);
            this.CheckDocumentSize(xml);
            return xml;
        }

        /// <summary>
        /// Checks the size of the XML sitemap document. If it is over 10MB, logs an error.
        /// </summary>
        /// <param name="sitemapXml">The sitemap XML document.</param>
        private void CheckDocumentSize(string sitemapXml)
        {
            if (sitemapXml.Length >= MaximumSitemapSizeInBytes)
            {
                this.loggingService.Log(new ApplicationException(
                    string.Format("Sitemap exceeds the maximum size of 10MB. This is because you have unusually long URL's. Consider reducing the MaximumSitemapNodeCount. Size:<{1}>", sitemapXml.Length)));
            }
        }

        /// <summary>
        /// Checks the count of the number of sitemaps. If it is over 50,000, logs an error.
        /// </summary>
        /// <param name="sitemapCount">The sitemap count.</param>
        private void CheckSitemapCount(int sitemapCount)
        {
            if (sitemapCount > MaximumSitemapCount)
            {
                this.loggingService.Log(new ApplicationException(
                    string.Format("Sitemap index file exceeds the maximum number of allowed sitemaps of 50,000. Count:<{1}>", sitemapCount)));
            }
        }

        #endregion
    }
}