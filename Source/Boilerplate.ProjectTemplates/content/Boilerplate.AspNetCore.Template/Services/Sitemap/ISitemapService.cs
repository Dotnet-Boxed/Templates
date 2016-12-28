namespace MvcBoilerplate.Services
{
    using System.Threading.Tasks;

    /// <summary>
    /// Generates sitemap XML for the current site.
    /// </summary>
    public interface ISitemapService
    {
        /// <summary>
        /// Gets the sitemap XML for the current site. If an index of null is passed and there are more than 25,000
        /// sitemap nodes, a sitemap index file is returned (A sitemap index file contains links to other sitemap files
        /// and is a way of splitting up your sitemap into separate files). If an index is specified, a standard
        /// sitemap is returned for the specified index parameter. See http://www.sitemaps.org/protocol.html
        /// </summary>
        /// <param name="index">The index of the sitemap to retrieve. <c>null</c> if you want to retrieve the root
        /// sitemap or sitemap index document, depending on the number of sitemap nodes.</param>
        /// <returns>The sitemap XML for the current site.</returns>
        Task<string> GetSitemapXml(int? index = null);
    }
}
