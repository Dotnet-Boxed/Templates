namespace Boilerplate.Web.Mvc.TagHelpers.Twitter
{
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// The App Card is a great way to represent mobile applications on Twitter and to drive installs. The app card 
    /// is designed to allow for a name, description and icon, and also to highlight attributes such as the rating 
    /// and the price. This Card type is currently available on the twitter.com website, as well as iOS and Android 
    /// mobile clients. It is not yet available on mobile web. See https://dev.twitter.com/cards/types/app.
    /// </summary>
    [HtmlTargetElement("twitter-card-app", Attributes = UsernameAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class TwitterCardApp : TwitterCard
    {
        #region Constants

        private const string CountryAttributeName = "country";
        private const string DescriptionAttributeName = "description";
        private const string GooglePlayAttributeName = "google-play";
        private const string GooglePlayCustomUrlSchemeAttributeName = "google-play-custom-url-scheme";
        private const string IPadAttributeName = "ipad";
        private const string IPadCustomUrlSchemeAttributeName = "iphone-custom-url-scheme";
        private const string IPhoneAttributeName = "ipad";
        private const string IPhoneCustomUrlSchemeAttributeName = "iphone-custom-url-scheme"; 

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the description that concisely summarizes the content of the page, as appropriate for 
        /// presentation within a Tweet. Do not re-use the title text as the description, or use this field 
        /// to describe the general services provided by the website. Description text will be truncated at 
        /// the word to 200 characters. If you are using Facebook's Open Graph og:description, do not use this
        /// unless you want a different description.
        /// </summary>
        [HtmlAttributeName(DescriptionAttributeName)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the country. If your application is not available in the US App Store, you must set this 
        /// value to the two-letter country code for the App Store that contains your application.
        /// </summary>
        [HtmlAttributeName(CountryAttributeName)]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the numeric representation of your app ID in Google Play (.i.e. “com.android.app”).
        /// </summary>
        [HtmlAttributeName(GooglePlayAttributeName)]
        public string GooglePlay { get; set; }

        /// <summary>
        /// Gets or sets your google play app’s custom URL scheme (you must include “://” after your scheme name).
        /// </summary>
        [HtmlAttributeName(GooglePlayCustomUrlSchemeAttributeName)]
        public string GooglePlayCustomUrlScheme { get; set; }

        /// <summary>
        /// Gets or sets numeric representation of your iPad app ID in the App Store (.i.e. “307234931”).
        /// </summary>
        [HtmlAttributeName(IPadAttributeName)]
        public string IPad { get; set; }

        /// <summary>
        /// Gets or sets your iPad app’s custom URL scheme (you must include “://” after your scheme name).
        /// </summary>
        [HtmlAttributeName(IPadCustomUrlSchemeAttributeName)]
        public string IPadCustomUrlScheme { get; set; }

        /// <summary>
        /// Gets or sets numeric representation of your iPhone app ID in the App Store (.i.e. “307234931”).
        /// </summary>
        [HtmlAttributeName(IPhoneAttributeName)]
        public string IPhone { get; set; }

        /// <summary>
        /// Gets or sets your iPhone app’s custom URL scheme (you must include “://” after your scheme name).
        /// </summary>
        [HtmlAttributeName(IPhoneCustomUrlSchemeAttributeName)]
        public string IPhoneCustomUrlScheme { get; set; }

        /// <summary>
        /// Gets the type of the Twitter card.
        /// </summary>
        public override TwitterCardType Type { get { return TwitterCardType.App; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Twitter card meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaNameContentIfNotNull("twitter:description", this.Description);
            stringBuilder.AppendMetaNameContentIfNotNull("twitter:app:id:iphone", this.IPhone);
            stringBuilder.AppendMetaNameContentIfNotNull("twitter:app:url:iphone", this.IPhoneCustomUrlScheme);
            stringBuilder.AppendMetaNameContentIfNotNull("twitter:app:id:ipad", this.IPad);
            stringBuilder.AppendMetaNameContentIfNotNull("twitter:app:url:ipad", this.IPadCustomUrlScheme);
            stringBuilder.AppendMetaNameContentIfNotNull("twitter:app:id:googleplay", this.GooglePlay);
            stringBuilder.AppendMetaNameContentIfNotNull("twitter:app:url:googleplay", this.GooglePlayCustomUrlScheme);
            stringBuilder.AppendMetaNameContentIfNotNull("twitter:app:country", this.Country);
        }

        #endregion
    }
}
