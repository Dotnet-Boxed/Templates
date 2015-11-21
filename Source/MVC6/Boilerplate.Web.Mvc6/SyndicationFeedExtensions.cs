#if NET451
namespace Boilerplate.Web.Mvc
{
    using System;
    using System.Linq;
    using System.ServiceModel.Syndication;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// <see cref="SyndicationFeed"/> extension methods.
    /// </summary>
    public static class SyndicationFeedExtensions
    {
        private const string YahooMediaNamespacePrefix = "media";
        private const string YahooMediaNamespace = "http://search.yahoo.com/mrss/";

        /// <summary>
        /// Adds a namespace to the specified feed.
        /// </summary>
        /// <param name="feed">The syndication feed.</param>
        /// <param name="namespacePrefix">The namespace prefix.</param>
        /// <param name="xmlNamespace">The XML namespace.</param>
        public static void AddNamespace(this SyndicationFeed feed, string namespacePrefix, string xmlNamespace)
        {
            feed.AttributeExtensions.Add(
                new XmlQualifiedName(namespacePrefix, XNamespace.Xmlns.ToString()), 
                xmlNamespace);
        }

        /// <summary>
        /// Adds the yahoo media namespace to the specified feed.
        /// </summary>
        /// <param name="feed">The syndication feed.</param>
        public static void AddYahooMediaNamespace(this SyndicationFeed feed)
        {
            AddNamespace(feed, YahooMediaNamespacePrefix, YahooMediaNamespace);
        }

        /// <summary>
        /// Gets the icon URL for the feed.
        /// </summary>
        /// <param name="feed">The syndication feed.</param>
        /// <returns>The icon URL.</returns>
        public static string GetIcon(this SyndicationFeed feed)
        {
            SyndicationElementExtension iconExtension = feed.ElementExtensions.FirstOrDefault(
                x => string.Equals(x.OuterName, "icon", StringComparison.OrdinalIgnoreCase));
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

        /// <summary>
        /// Sets the Yahoo Media thumbnail for the feed entry.
        /// </summary>
        /// <param name="item">The feed entry.</param>
        /// <param name="url">The thumbnail URL.</param>
        /// <param name="width">The optional width of the thumbnail image.</param>
        /// <param name="height">The optional height of the thumbnail image.</param>
        public static void SetThumbnail(this SyndicationItem item, string url, int? width, int? height)
        {
            XNamespace ns = YahooMediaNamespace;
            item.ElementExtensions.Add(new SyndicationElementExtension(
                new XElement(
                    ns + "thumbnail",
                    new XAttribute("url", url),
                    width.HasValue ? new XAttribute("width", width) : null,
                    height.HasValue ? new XAttribute("height", height) : null)));
        }
    }
}
#endif