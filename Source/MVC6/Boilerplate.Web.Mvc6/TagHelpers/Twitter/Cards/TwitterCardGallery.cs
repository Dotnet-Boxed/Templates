namespace Boilerplate.Web.Mvc.TagHelpers.Twitter
{
    using System;
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// The Gallery Card allows you to represent collections of photos within a Tweet. This Card type is 
    /// designed to let the user know that there’s more than just a single image at the URL shared, but 
    /// rather a gallery of related images. You can specify up to 4 different images to show in the gallery 
    /// card. You can also provide attribution to the photographer of the  gallery by specifying the value of the creator tag. 
    /// See https://dev.twitter.com/cards/types/gallery.
    /// </summary>
    [HtmlTargetElement(
        "twitter-card-gallery", 
        Attributes = 
            UsernameAttributeName + "," + Image0AttributeName + "," + Image1AttributeName + "," + 
            Image2AttributeName + "," + Image3AttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class TwitterCardGallery : TwitterCard
    {
        #region Constants

        private const string CreatorUsernameAttributeName = "creator-username";
        private const string DescriptionAttributeName = "description";
        private const string Image0AttributeName = "image0";
        private const string Image1AttributeName = "image1";
        private const string Image2AttributeName = "image2";
        private const string Image3AttributeName = "image3";
        private const string TitleAttributeName = "title";

        #endregion
        
        #region Public Properties

        /// <summary>
        /// Gets or sets the Twitter username of the creator of the content on the page e.g. @RehanSaeedUK. This is an 
        /// optional property.
        /// </summary>
        [HtmlAttributeName(CreatorUsernameAttributeName)]
        public string CreatorUsername { get; set; }

        /// <summary>
        /// Gets or sets the description that concisely summarizes the content of the page, as appropriate for 
        /// presentation within a Tweet. Do not re-use the title text as the description, or use this field 
        /// to describe the general services provided by the website. Description text will be truncated at 
        /// the word to 200 characters. This is an optional property. If you are using Facebook's Open Graph 
        /// og:description, do not use this unless you want a different description.
        /// </summary>
        [HtmlAttributeName(DescriptionAttributeName)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the URL to the first image representing the content of the page.  Images must be less than 
        /// 1MB in size. If you are using Facebook's Open Graph og:image, you do not have to use this unless you want 
        /// a different image but it is recommended to supply a smaller image that fits Twitters needs.
        /// </summary>
        [HtmlAttributeName(Image0AttributeName)]
        public TwitterImage Image0 { get; set; }

        /// <summary>
        /// Gets or sets the URL to the second image representing the content of the page.  Images must be less than 
        /// 1MB in size. If you are using Facebook's Open Graph og:image, you do not have to use this unless you want 
        /// a different image but it is recommended to supply a smaller image that fits Twitters needs.
        /// </summary>
        [HtmlAttributeName(Image1AttributeName)]
        public TwitterImage Image1 { get; set; }

        /// <summary>
        /// Gets or sets the URL to the third image representing the content of the page.  Images must be less than 
        /// 1MB in size. If you are using Facebook's Open Graph og:image, you do not have to use this unless you want 
        /// a different image but it is recommended to supply a smaller image that fits Twitters needs.
        /// </summary>
        [HtmlAttributeName(Image2AttributeName)]
        public TwitterImage Image2 { get; set; }

        /// <summary>
        /// Gets or sets the URL to the fourth image representing the content of the page.  Images must be less than 
        /// 1MB in size. If you are using Facebook's Open Graph og:image, you do not have to use this unless you want 
        /// a different image but it is recommended to supply a smaller image that fits Twitters needs.
        /// </summary>
        [HtmlAttributeName(Image3AttributeName)]
        public TwitterImage Image3 { get; set; }

        /// <summary>
        /// Gets or sets the title of your content as it should appear in the card. You may specify an empty 
        /// string if you wish no title to render.
        /// </summary>
        [HtmlAttributeName(TitleAttributeName)]
        public string Title { get; set; }

        /// <summary>
        /// Gets the type of the Twitter card.
        /// </summary>
        public override TwitterCardType Type { get { return TwitterCardType.Gallery; } }

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

            if (this.Image0 != null)
            {
                stringBuilder.AppendMetaNameContent("twitter:image0", this.Image0.ImageUrl);
                stringBuilder.AppendMetaNameContentIfNotNull("twitter:image0:height", this.Image0.Height);
                stringBuilder.AppendMetaNameContentIfNotNull("twitter:image0:width", this.Image0.Width);
            }

            if (this.Image1 != null)
            {
                stringBuilder.AppendMetaNameContent("twitter:image1", this.Image1.ImageUrl);
                stringBuilder.AppendMetaNameContentIfNotNull("twitter:image1:height", this.Image1.Height);
                stringBuilder.AppendMetaNameContentIfNotNull("twitter:image1:width", this.Image1.Width);
            }

            if (this.Image2 != null)
            {
                stringBuilder.AppendMetaNameContent("twitter:image2", this.Image2.ImageUrl);
                stringBuilder.AppendMetaNameContentIfNotNull("twitter:image2:height", this.Image2.Height);
                stringBuilder.AppendMetaNameContentIfNotNull("twitter:image2:width", this.Image2.Width);
            }

            if (this.Image3 != null)
            {
                stringBuilder.AppendMetaNameContent("twitter:image3", this.Image3.ImageUrl);
                stringBuilder.AppendMetaNameContentIfNotNull("twitter:image3:height", this.Image3.Height);
                stringBuilder.AppendMetaNameContentIfNotNull("twitter:image3:width", this.Image3.Width);
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Checks that this instance is valid and throws exceptions if not valid.
        /// </summary>
        protected override void Validate()
        {
            base.Validate();

            if (this.Image0 == null) { throw new ArgumentNullException(nameof(this.Image0)); }
            if (this.Image1 == null) { throw new ArgumentNullException(nameof(this.Image1)); }
            if (this.Image2 == null) { throw new ArgumentNullException(nameof(this.Image2)); }
            if (this.Image3 == null) { throw new ArgumentNullException(nameof(this.Image3)); }
        }

        #endregion
    }
}
