namespace $safeprojectname$.Framework
{
    using System;
    using System.Linq;
    using System.ServiceModel.Syndication;

    /// <summary>
    /// <see cref="SyndicationFeed"/> extension methods.
    /// </summary>
    public static class SyndicationFeedExtensions
    {
        /// <summary>
        /// Gets the icon URL for the feed.
        /// </summary>
        /// <param name="feed">The syndication feed.</param>
        /// <returns>The icon URL.</returns>
        public static string GetIcon(this SyndicationFeed feed)
        {
            SyndicationElementExtension iconExtension = feed.ElementExtensions.FirstOrDefault(x => string.Equals(x.OuterName, "icon", StringComparison.OrdinalIgnoreCase));
            return iconExtension.GetObject<string>();
        }

        /// <summary>
        /// Sets the icon URL for the feed.
        /// </summary>
        /// <param name="feed">The syndication feed.</param>
        /// <param name="iconUrl">The icon URL.</param>
        public static void SetIcon(this SyndicationFeed feed, string iconUrl)
        {
            feed.ElementExtensions.Add(new SyndicationElementExtension("icon", null, iconUrl));
        }
    }
}