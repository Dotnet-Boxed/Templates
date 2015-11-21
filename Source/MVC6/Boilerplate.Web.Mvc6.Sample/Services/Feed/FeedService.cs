#if DNX451
// The FeedService is not available for .NET Core because the System.ServiceModel.Syndication.SyndicationFeed 
// type does not yet exist. See https://github.com/dotnet/wcf/issues/76.
namespace MvcBoilerplate.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.ServiceModel.Syndication;
    using System.Threading;
    using System.Threading.Tasks;
    using Boilerplate.Web.Mvc;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Extensions.OptionsModel;
    using MvcBoilerplate.Constants;
    using MvcBoilerplate.Settings;

    /// <summary>
    /// Builds <see cref="SyndicationFeed"/>'s containing meta data about the feed and the feed entries.
    /// Note: We are targeting Atom 1.0 over RSS 2.0 because Atom 1.0 is a newer and more well defined format. Atom 1.0
    /// is a standard and RSS is not. See http://rehansaeed.com/building-rssatom-feeds-for-asp-net-mvc/.
    /// </summary>
    public sealed class FeedService : IFeedService
    {
        /// <summary>
        /// The feed universally unique identifier. Do not use the URL of your feed as this can change.
        /// A much better ID is to use a GUID which you can generate from Tools->Create GUID in Visual Studio.
        /// </summary>
        private const string FeedId = "[INSERT GUID HERE]";
        private const string PubSubHubbubHubUrl = "https://pubsubhubbub.appspot.com/";

        private readonly IOptions<AppSettings> appSettings;
        private readonly HttpClient httpClient;
        private readonly IUrlHelper urlHelper;

#region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedService"/> class.
        /// </summary>
        /// <param name="appSettings">The application settings.</param>
        /// <param name="urlHelper">The URL helper.</param>
        public FeedService(
            IOptions<AppSettings> appSettings,
            IUrlHelper urlHelper)
        {
            this.appSettings = appSettings;
            this.urlHelper = urlHelper;
            this.httpClient = new HttpClient();
        }

#endregion

#region Public Methods

        /// <summary>
        /// Gets the feed containing meta data about the feed and the feed entries.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> signifying if the request is cancelled.</param>
        /// <returns>A <see cref="SyndicationFeed"/>.</returns>
        public async Task<SyndicationFeed> GetFeed(CancellationToken cancellationToken)
        {
            SyndicationFeed feed = new SyndicationFeed()
            {
                // id (Required) - The feed universally unique identifier.
                Id = FeedId,
                // title (Required) - Contains a human readable title for the feed. Often the same as the title of the 
                //                    associated website. This value should not be blank.
                Title = SyndicationContent.CreatePlaintextContent(this.appSettings.Value.SiteTitle),
                // items (Required) - The items to add to the feed.
                Items = await this.GetItems(cancellationToken),
                // subtitle (Recommended) - Contains a human-readable description or subtitle for the feed.
                Description = SyndicationContent.CreatePlaintextContent(
                    "This is the ASP.NET MVC Boilerplate feed description."),
                // updated (Optional) - Indicates the last time the feed was modified in a significant way.
                LastUpdatedTime = DateTimeOffset.Now,
                // logo (Optional) - Identifies a larger image which provides visual identification for the feed. 
                //                   Images should be twice as wide as they are tall.
                ImageUrl = new Uri(this.urlHelper.AbsoluteContent("~/img/icons/atom-logo-96x48.png")),
                // rights (Optional) - Conveys information about rights, e.g. copyrights, held in and over the feed.
                Copyright = SyndicationContent.CreatePlaintextContent(
                    string.Format("© {0} - {1}", DateTime.Now.Year, this.appSettings.Value.SiteTitle)),
                // lang (Optional) - The language of the feed.
                Language = "en-GB",
                // generator (Optional) - Identifies the software used to generate the feed, for debugging and other 
                //                        purposes. Do not put in anything that identifies the technology you are using.
                // Generator = "Sample Code",
                // base (Buggy) - Add the full base URL to the site so that all other links can be relative. This is 
                //                great, except some feed readers are buggy with it, INCLUDING FIREFOX!!! 
                //                (See https://bugzilla.mozilla.org/show_bug.cgi?id=480600).
                // BaseUri = new Uri(this.urlHelper.AbsoluteRouteUrl(HomeControllerRoute.GetIndex))
            };
        
            // self link (Required) - The URL for the syndication feed.
            feed.Links.Add(SyndicationLink.CreateSelfLink(
                new Uri(this.urlHelper.AbsoluteRouteUrl(HomeControllerRoute.GetFeed)), 
                ContentType.Atom));
        
            // alternate link (Recommended) - The URL for the web page showing the same data as the syndication feed.
            feed.Links.Add(SyndicationLink.CreateAlternateLink(
                new Uri(this.urlHelper.AbsoluteRouteUrl(HomeControllerRoute.GetIndex)), 
                ContentType.Html));
        
            // hub link (Recommended) - The URL for the PubSubHubbub hub. Used to push new entries to subscribers 
            //                          instead of making them poll the feed. See feed updated method below.
            feed.Links.Add(new SyndicationLink(new Uri(PubSubHubbubHubUrl), "hub", null, null, 0));
        
            // first, last, next previous (Optional) - Atom 1.0 supports paging of your feed. This is good if your feed
            //                                         becomes very large and needs splitting up into pages. To add
            //                                         paging, you must add links to the first, last, next and previous
            //                                         feed pages.
            // feed.Links.Add(new SyndicationLink(new Uri("http://example.com/feed"), "first", null, null, 0));
            // feed.Links.Add(new SyndicationLink(new Uri("http://example.com/feed?page=10"), "last", null, null, 0));
            // if ([HAS_PREVIOUS_PAGE])
            //     feed.Links.Add(new SyndicationLink(new Uri("http://example.com/feed?page=1")), "previous", null, null, 0));
            // if ([HAS_NEXT_PAGE])
            //     feed.Links.Add(new SyndicationLink(new Uri("http://example.com/feed?page=3"), "next", null, null, 0));

            // author (Recommended) - Names one author of the feed. A feed may have multiple author elements. A feed 
            //                        must contain at least one author element unless all of the entry elements contain 
            //                        at least one author element.
            feed.Authors.Add(this.GetPerson());
        
            // category (Optional) - Specifies a category that the feed belongs to. A feed may have multiple category 
            //                       elements.
            feed.Categories.Add(new SyndicationCategory("CategoryName"));
        
            // contributor (Optional) - Names one contributor to the feed. An feed may have multiple contributor 
            //                          elements.
            feed.Contributors.Add(this.GetPerson());
        
            // icon (Optional) - Identifies a small image which provides iconic visual identification for the feed. 
            //                   Icons should be square.
            feed.SetIcon(this.urlHelper.AbsoluteContent("~/img/icons/atom-icon-48x48.png"));
        
            // Add the Yahoo Media namespace (xmlns:media="http://search.yahoo.com/mrss/") to the Atom feed. 
            // This gives us extra abilities, like the ability to give thumbnail images to entries. 
            // See http://www.rssboard.org/media-rss for more information.
            feed.AddYahooMediaNamespace();
        
            return feed;
        }

        /// <summary>
        /// Publishes the fact that the feed has updated to subscribers using the PubSubHubbub v0.4 protocol.
        /// </summary>
        /// <remarks>
        /// The PubSubHubbub is an open standard created by Google which allows subscription of feeds and allows 
        /// updates to be pushed to them rather than them having to poll the feed. This means subscribers get live
        /// updates as they happen and also we may save some bandwidth because we have less polling of our feed.
        /// See https://pubsubhubbub.googlecode.com/git/pubsubhubbub-core-0.4.html for PubSubHubbub v0.4 specification.
        /// See https://github.com/pubsubhubbub for PubSubHubbub GitHub projects.
        /// See http://pubsubhubbub.appspot.com/ for Google's implementation of the PubSubHubbub hub we are using.
        /// </remarks>
        public Task PublishUpdate()
        {
            return httpClient.PostAsync(
                PubSubHubbubHubUrl, 
                new FormUrlEncodedContent(
                    new KeyValuePair<string, string>[]
                    {
                        new KeyValuePair<string, string>("hub.mode", "publish"),
                        new KeyValuePair<string, string>(
                            "hub.url", 
                            this.urlHelper.AbsoluteRouteUrl(HomeControllerRoute.GetFeed))
                    }));
        }

#endregion

#region Private Methods

        private SyndicationPerson GetPerson()
        {
            return new SyndicationPerson()
            {
                // name (Required) - conveys a human-readable name for the person.
                Name = "Rehan Saeed",
                // uri (Optional) - contains a home page for the person.
                Uri = "http://rehansaeed.com",
                // email (Optional) - contains an email address for the person.
                Email = "example@email.com"
            };
        }

        /// <summary>
        /// Gets the collection of <see cref="SyndicationItem"/>'s that represent the atom entries.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> signifying if the request is cancelled.</param>
        /// <returns>A collection of <see cref="SyndicationItem"/>'s.</returns>
        private async Task<List<SyndicationItem>> GetItems(CancellationToken cancellationToken)
        {
            List<SyndicationItem> items = new List<SyndicationItem>();
        
            for (int i = 1; i < 4; ++i)
            {
                SyndicationItem item = new SyndicationItem()
                {
                    // id (Required) - Identifies the entry using a universally unique and permanent URI. Two entries 
                    //                 in a feed can have the same value for id if they represent the same entry at 
                    //                 different points in time.
                    Id = FeedId + i,
                    // title (Required) - Contains a human readable title for the entry. This value should not be blank.
                    Title = SyndicationContent.CreatePlaintextContent("Item " + i),
                    // description (Recommended) - A summary of the entry.
                    Summary = SyndicationContent.CreatePlaintextContent("A summary of item " + i),
                    // updated (Optional) - Indicates the last time the entry was modified in a significant way. This 
                    //                      value need not change after a typo is fixed, only after a substantial 
                    //                      modification. Generally, different entries in a feed will have different 
                    //                      updated timestamps.
                    LastUpdatedTime = DateTimeOffset.Now,
                    // published (Optional) - Contains the time of the initial creation or first availability of the entry.
                    PublishDate = DateTimeOffset.Now,
                    // rights (Optional) - Conveys information about rights, e.g. copyrights, held in and over the entry.
                    Copyright = new TextSyndicationContent(
                        string.Format("© {0} - {1}", DateTime.Now.Year, this.appSettings.Value.SiteTitle)),
                };
        
                // link (Recommended) - Identifies a related Web page. An entry must contain an alternate link if there 
                //                      is no content element.
                item.Links.Add(SyndicationLink.CreateAlternateLink(
                    new Uri(this.urlHelper.AbsoluteRouteUrl(HomeControllerRoute.GetIndex)), 
                    ContentType.Html));
                // AND/OR
                // Text content  (Optional) - Contains or links to the complete content of the entry. Content must be 
                //                            provided if there is no alternate link.
                // item.Content = SyndicationContent.CreatePlaintextContent("The actual plain text content of the entry");
                // HTML content (Optional) - Content can be plain text or HTML. Here is a HTML example.
                // item.Content = SyndicationContent.CreateHtmlContent("The actual HTML content of the entry");
        
                // author (Optional) - Names one author of the entry. An entry may have multiple authors. An entry must 
                //                     contain at least one author element unless there is an author element in the 
                //                     enclosing feed, or there is an author element in the enclosed source element.
                item.Authors.Add(this.GetPerson());
        
                // contributor (Optional) - Names one contributor to the entry. An entry may have multiple contributor elements.
                item.Contributors.Add(this.GetPerson());
        
                // category (Optional) - Specifies a category that the entry belongs to. A entry may have multiple 
                //                       category elements.
                item.Categories.Add(new SyndicationCategory("CategoryName"));
        
                // link - Add additional links to related images, audio or video like so.
                item.Links.Add(SyndicationLink.CreateMediaEnclosureLink(
                    new Uri(this.urlHelper.AbsoluteContent("~/img/icons/atom-icon-48x48.png")), 
                    ContentType.Png, 
                    0));
        
                // media:thumbnail - Add a Yahoo Media thumbnail for the entry. See http://www.rssboard.org/media-rss 
                //                   for more information.
                item.SetThumbnail(this.urlHelper.AbsoluteContent("~/img/icons/atom-icon-48x48.png"), 48, 48);
        
                items.Add(item);
            }
        
            return items;
        }

#endregion
    }
}
#endif