namespace Boilerplate.AspNetCore.Sitemap
{
    /// <summary>
    /// How frequently the page or URL is likely to change.
    /// </summary>
    public enum SitemapFrequency
    {
        /// <summary>
        /// Describes archived URLs that never change.
        /// </summary>
        Never,

        /// <summary>
        /// Describes URL's that change yearly.
        /// </summary>
        Yearly,

        /// <summary>
        /// Describes URL's that change monthly.
        /// </summary>
        Monthly,

        /// <summary>
        /// Describes URL's that change weekly.
        /// </summary>
        Weekly,

        /// <summary>
        /// Describes URL's that change daily.
        /// </summary>
        Daily,

        /// <summary>
        /// Describes URL's that change hourly.
        /// </summary>
        Hourly,

        /// <summary>
        /// Describes documents that change each time they are accessed.
        /// </summary>
        Always
    }
}