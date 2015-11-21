namespace Boilerplate.Web.Mvc.TagHelpers.Twitter
{
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// The Summary Card with Large Image features a large, full-width prominent image alongside a tweet. 
    /// It is designed to give the reader a rich photo experience, and clicking on the image brings the 
    /// user to your website. On twitter.com and the mobile clients, the image appears below the tweet text.
    /// See https://dev.twitter.com/cards/types/summary-large-image.
    /// </summary>
    [HtmlTargetElement("twitter-card-summary-large-image", Attributes = UsernameAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class TwitterCardSummaryLargeImage : TwitterCard
    {
        #region Constants

        private const string CreatorUsernameAttributeName = "creator-username";
        private const string DescriptionAttributeName = "description";
        private const string ImageAttributeName = "image";
        private const string TitleAttributeName = "title";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Twitter username of the creator of the content on the page e.g. @RehanSaeedUK. This is an optional property.
        /// </summary>
        [HtmlAttributeName(CreatorUsernameAttributeName)]
        public string CreatorUsername { get; set; }

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
        /// Gets or sets the URL to a unique image representing the content of the page. Do not use a generic 
        /// image such as your website logo, author photo, or other image that spans multiple pages. Images 
        /// for this Card should be at least 280px in width, and at least 150px in height. Image must be less 
        /// than 1MB in size. If you are using Facebook's Open Graph og:image, you do not have to use
        /// this unless you want a different image but it is recommended to get supply a smaller image that fits
        /// Twitters needs.
        /// </summary>
        [HtmlAttributeName(ImageAttributeName)]
        public TwitterImage Image { get; set; }

        /// <summary>
        /// Gets or sets the title of the summary. Title should be concise and will be truncated at 70 characters.
        /// If you are using Facebook's Open Graph og:title, do not use this unless you want a different description.
        /// </summary>
        [HtmlAttributeName(TitleAttributeName)]
        public string Title { get; set; }

        /// <summary>
        /// Gets the type of the Twitter card.
        /// </summary>
        public override TwitterCardType Type { get { return TwitterCardType.SummaryLargeImage; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Twitter card meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaNameContentIfNotNull("twitter:title", this.Title);
            stringBuilder.AppendMetaNameContentIfNotNull("twitter:description", this.Description);
            if (this.Image != null)
            {
                stringBuilder.AppendMetaNameContent("twitter:image", this.Image.ImageUrl);
                stringBuilder.AppendMetaNameContentIfNotNull("twitter:image:height", this.Image.Height);
                stringBuilder.AppendMetaNameContentIfNotNull("twitter:image:width", this.Image.Width);
            }
        }

        #endregion
    }
}
