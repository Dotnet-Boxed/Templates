namespace MvcBoilerplate.Services
{
    public interface ISitemapService
    {
        /// <summary>
        /// Gets the sitemap XML for the current site. If there are more than 50,000 sitemap nodes or larger than 10MB
        /// (The maximum allowed before you have to use a sitemap index file), the list is truncated and an error is logged.
        /// See <see cref="http://www.sitemaps.org/protocol.html"/> for more information.
        /// </summary>
        /// <returns>The sitemap XML for the current site.</returns>
        string GetSitemapXml();
    }
}
