namespace MvcBoilerplate.Services
{
    using System.ServiceModel.Syndication;

    /// <summary>
    /// Builds <see cref="SyndicationFeed"/>'s containing meta data about the feed and the feed entries.
    /// Note: We are targeting Atom 1.0 over RSS 2.0 because Atom 1.0 is a newer and more well defined format. Atom 1.0 is a standard and RSS is not. 
    /// (See http://www.intertwingly.net/wiki/pie/Rss20AndAtom10Compared).
    /// (See http://atomenabled.org/developers/syndication/ for more information about Atom 1.0).
    /// (See https://tools.ietf.org/html/rfc4287 for the official Atom Syndication Format specifications).
    /// (See http://feedvalidator.org/ to validate your feed).
    /// (See http://stackoverflow.com/questions/1301392/pagination-in-feeds-like-atom-and-rss to see how you can add paging to your feed).
    /// </summary>
    public interface IFeedService
    {
        /// <summary>
        /// Gets the feed containing meta data about the feed and the feed entries.
        /// </summary>
        /// <returns>A <see cref="SyndicationFeed"/>.</returns>
        SyndicationFeed GetFeed();
    }
}
