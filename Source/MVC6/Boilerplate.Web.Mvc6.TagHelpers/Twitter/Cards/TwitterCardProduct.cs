namespace Boilerplate.Web.Mvc.TagHelpers.Twitter
{
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// The Product Card is a great way to represent product and retail items on Twitter. This Card type is designed to showcase your products via an 
    /// image, a description, and allow you to highlight two other key details about your product.
    /// See https://dev.twitter.com/cards/types/product
    /// </summary>
    [HtmlTargetElement("twitter-card-product", Attributes = UsernameAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class TwitterCardProduct : TwitterCard
    {
        #region Constants

        private const string CreatorUsernameAttributeName = "creator-username";
        private const string Data1AttributeName = "data1";
        private const string Data2AttributeName = "data2";
        private const string DescriptionAttributeName = "description";
        private const string ImageAttributeName = "image";
        private const string Label1AttributeName = "label1";
        private const string Label2AttributeName = "label2";
        private const string TitleAttributeName = "title";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Twitter username of the creator of the content on the page e.g. @RehanSaeedUK. This is an optional property.
        /// </summary>
        [HtmlAttributeName(CreatorUsernameAttributeName)]
        public string CreatorUsername { get; set; }

        /// <summary>
        /// Gets or sets the first data value for label one.
        /// </summary>
        [HtmlAttributeName(Data1AttributeName)]
        public string Data1 { get; set; }

        /// <summary>
        /// Gets or sets the second data value for label two.
        /// </summary>
        [HtmlAttributeName(Data2AttributeName)]
        public string Data2 { get; set; }

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
        /// image such as your website logo, author photo, or other image that spans multiple pages. The 
        /// image must be a minimum size of 160px by 160px and must be less than 1MB in file size. It prefers a 
        /// square image but we crop/resize oddly shaped images to fit as long as both dimensions are greater than 
        /// or equal to 160 pixels. If you are using Facebook's Open Graph og:image, you do not have to use
        /// this unless you want a different image but it is recommended to get supply a smaller image that fits
        /// Twitters needs.
        /// </summary>
        [HtmlAttributeName(ImageAttributeName)]
        public TwitterImage Image { get; set; }

        /// <summary>
        /// Gets or sets the first label and allows you to specify the types of data you want to offer (price, country, etc.).
        /// </summary>
        [HtmlAttributeName(Label1AttributeName)]
        public string Label1 { get; set; }

        /// <summary>
        /// Gets or sets the second label and allows you to specify the types of data you want to offer (price, country, etc.).
        /// </summary>
        [HtmlAttributeName(Label2AttributeName)]
        public string Label2 { get; set; }

        /// <summary>
        /// Gets or sets the title of the summary. Title should be concise and will be truncated at 70 characters.
        /// If you are using Facebook's Open Graph og:title, do not use this unless you want a different title.
        /// </summary>
        [HtmlAttributeName(TitleAttributeName)]
        public string Title { get; set; }

        /// <summary>
        /// Gets the type of the Twitter card.
        /// </summary>
        public override TwitterCardType Type { get { return TwitterCardType.Product; } }

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

            stringBuilder.AppendMetaNameContentIfNotNull("twitter:label1", this.Label1);
            stringBuilder.AppendMetaNameContentIfNotNull("twitter:data1", this.Data1);
            stringBuilder.AppendMetaNameContentIfNotNull("twitter:label2", this.Label2);
            stringBuilder.AppendMetaNameContentIfNotNull("twitter:data2", this.Data2);
        }

        #endregion
    }
}
