namespace MvcBoilerplate.Services
{
    using System.Threading.Tasks;

    public interface ISitemapPingerService
    {
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
        Task PingSearchEngines();
    }
}