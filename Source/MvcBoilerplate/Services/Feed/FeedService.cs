namespace MvcBoilerplate.Services
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel.Syndication;
    using System.Web.Mvc;
    using MvcBoilerplate.Constants;
    using MvcBoilerplate.Framework;

    /// <summary>
    /// Builds <see cref="SyndicationFeed"/>'s containing meta data about the feed and the feed entries.
    /// Note: We are targeting Atom 1.0 over RSS 2.0 because Atom 1.0 is a newer and more well defined format. Atom 1.0 is a standard and RSS is not. 
    /// (See http://www.intertwingly.net/wiki/pie/Rss20AndAtom10Compared).
    /// (See http://atomenabled.org/developers/syndication/ for more information about Atom 1.0).
    /// (See https://tools.ietf.org/html/rfc4287 for the official Atom Syndication Format specifications).
    /// (See http://feedvalidator.org/ to validate your feed).
    /// </summary>
    public sealed class FeedService : IFeedService
    {
        /// <summary>
        /// The feed universally unique identifier. Do not use the URL of your feed as some reccomend as this can change.
        /// A much better ID is to use a GUID which you can generate from Tools->Create GUID in Visual Studio.
        /// </summary>
        private const string FeedId = "[INSERT GUID HERE]";
        private readonly UrlHelper urlHelper;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedService"/> class.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        public FeedService(UrlHelper urlHelper)
        {
            this.urlHelper = urlHelper;
        } 

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the feed containing meta data about the feed and the feed entries.
        /// </summary>
        /// <returns>A <see cref="SyndicationFeed"/>.</returns>
        public SyndicationFeed GetFeed()
        {
            // title (Required) - Contains a human readable title for the feed. Often the same as the title of the associated website. This value should not be blank.
            string title = "ASP.NET MVC Boilerplate";
            
            // subtitle (Recommended) - Contains a human-readable description or subtitle for the feed.
            string description = "This is the ASP.NET MVC Boilerplate feed description.";
            
            // link (Required) - The URL for the syndication feed.
            Uri feedAlternateLink = new Uri(this.urlHelper.RouteUrl(HomeControllerRoute.GetFeed), UriKind.Relative);
            
            // id (Required) - The feed universally unique identifier.
            string id = FeedId;
            
            // updated (Optional) - Indicates the last time the feed was modified in a significant way.
            DateTimeOffset lastUpdatedTime = DateTimeOffset.Now;
            
            // items (Required) - The items to add to the feed.
            List<SyndicationItem> items = this.GetItems();

            SyndicationFeed feed = new SyndicationFeed(
                title,
                description,
                feedAlternateLink,
                id,
                lastUpdatedTime,
                items);

            // base (Recommended) - Add the full base URL to the site so that all other links can be relative. We save a few bytes.
            feed.BaseUri = new Uri(this.urlHelper.AbsoluteRouteUrl(HomeControllerRoute.GetIndex));

            // author (Recommended) - Names one author of the feed. A feed may have multiple author elements. A feed must contain at least one author element unless all of the entry elements contain at least one author element.
            feed.Authors.Add(this.GetPerson());

            // category (Optional) - Specifies a category that the feed belongs to. A feed may have multiple category elements.
            feed.Categories.Add(new SyndicationCategory("CategoryName"));

            // contributor (Optional) - Names one contributor to the feed. An feed may have multiple contributor elements.
            feed.Contributors.Add(this.GetPerson());

            // rights (Optional) - Conveys information about rights, e.g. copyrights, held in and over the feed.
            feed.Copyright = new TextSyndicationContent(string.Format("© {0} - {0}", DateTime.Now.Year, Application.Name));

            // generator (Optional) - Identifies the software used to generate the feed, for debugging and other purposes. Do not put in anything that identifies the technology you are using.
            // feed.Generator = "Sample Code";

            // icon (Optional) - Identifies a small image which provides iconic visual identification for the feed. Icons should be square.
            feed.SetIcon(this.urlHelper.Content("~/content/icons/favicon-192x192.png"));

            // logo (Optional) - Identifies a larger image which provides visual identification for the feed. Images should be twice as wide as they are tall.
            feed.ImageUrl = new Uri(this.urlHelper.Content("~/content/icons/mstile-310x150.png"), UriKind.Relative);

            // lang (Optional) - The language of the feed.
            feed.Language = "en-GB";

            return feed;
        } 

        #endregion

        #region Private Methods

        private SyndicationPerson GetPerson()
        {
            // name (Required) - conveys a human-readable name for the person.
            string name = "Rehan Saeed";
            // email (Optional) - contains an email address for the person.
            string email = "example@email.com";
            // uri (Optional) - contains a home page for the person.
            string url = "http://rehansaeed.co.uk";
            return new SyndicationPerson(email, name, url);
        }

        /// <summary>
        /// Gets the collection of <see cref="SyndicationItem"/>'s that represent the atom entries.
        /// </summary>
        /// <returns>A collection of <see cref="SyndicationItem"/>'s.</returns>
        private List<SyndicationItem> GetItems()
        {
            List<SyndicationItem> items = new List<SyndicationItem>();

            for (int i = 0; i < 3; ++i)
            {
                SyndicationItem item = new SyndicationItem();

                // id (Required) - Identifies the entry using a universally unique and permanent URI. Two entries in a feed can have the same value for id if they represent the same entry at different points in time.
                item.Id = FeedId + i;
                
                // title (Required) - Contains a human readable title for the entry. This value should not be blank.
                item.Title = SyndicationContent.CreatePlaintextContent("Item " + i);

                // description (Reccomended) - A summary of the entry.
                item.Summary = SyndicationContent.CreatePlaintextContent("A summary of item " + i);

                // link (Read On) - Identifies a related Web page. An entry must contain an alternate link if there is no content element.
                item.Links.Add(SyndicationLink.CreateAlternateLink(new Uri(this.urlHelper.RouteUrl(HomeControllerRoute.GetIndex), UriKind.Relative)));
                // OR
                // content (Read On) - Contains or links to the complete content of the entry. Content must be provided if there is no alternate link.
                // item.Content = SyndicationContent.CreatePlaintextContent("The actual plain text content of the entry");
                // content - Content can be plain text or HTML.
                // item.Content = SyndicationContent.CreateHtmlContent("The actual HTML content of the entry");

                // updated (Optional) - Indicates the last time the entry was modified in a significant way. This value need not change after a typo is fixed, only after a substantial modification. Generally, different entries in a feed will have different updated timestamps.
                item.LastUpdatedTime = DateTimeOffset.Now;
                
                // author (Optional) - Names one author of the entry. An entry may have multiple authors. An entry must contain at least one author element unless there is an author element in the enclosing feed, or there is an author element in the enclosed source element.
                item.Authors.Add(this.GetPerson());

                // category (Optional) - Specifies a category that the entry belongs to. A entry may have multiple category elements.
                item.Categories.Add(new SyndicationCategory("CategoryName"));

                // contributor (Optional) - Names one contributor to the entry. An entry may have multiple contributor elements.
                item.Contributors.Add(this.GetPerson());

                // published (Optional) - Contains the time of the initial creation or first availability of the entry.
                item.PublishDate = DateTimeOffset.Now;

                // rights (Optional) - Conveys information about rights, e.g. copyrights, held in and over the entry.
                item.Copyright = new TextSyndicationContent(string.Format("© {0} - {0}", DateTime.Now.Year, Application.Name));

                // link - Add additional links to related images, audio or video like so.
                // item.Links.Add(SyndicationLink.CreateMediaEnclosureLink(new Uri(this.urlHelper.Content("~/content/icons/favicon-192x192.png"), UriKind.Relative), ContentType.Png, 0));

                items.Add(item);
            }

            return items;
        } 

        #endregion
    }
}