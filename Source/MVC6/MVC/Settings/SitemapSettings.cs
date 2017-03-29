namespace MvcBoilerplate.Settings
{
    /// <summary>
    /// Stores sitemap related settings.
    /// </summary>
    public class SitemapSettings
    {
        /// <summary>
        /// Gets or sets an array of URL's where the sitemap can be pinged to. Note that Yahoo uses the same sitemap
        /// ping location as Bing.
        /// </summary>
        /// <value>
        /// The sitemap ping locations.
        /// </value>
        public string[] SitemapPingLocations { get; set; }
    }
}
