namespace Boilerplate.Web.Mvc.TagHelpers.Twitter
{
    using System;
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// The Photo Card puts the image front and center in the Tweet. Clicking on the photo expands it to a 
    /// richer, detailed view. On twitter.com and mobile clients, the image appears below the tweet text.
    /// See https://dev.twitter.com/cards/types/photo
    /// </summary>
    [HtmlTargetElement(
        "twitter-card-photo", 
        Attributes = UsernameAttributeName + "," + ImageAttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class TwitterCardPhoto : TwitterCard
    {
        #region Constants

        private const string CreatorUsernameAttributeName = "creator-username";
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
        /// Gets or sets the image. A URL to the image representing the content. Image must be less than 1MB in size.
        /// Twitter will resize images, maintaining original aspect ratio to fit the following sizes:
        /// - Web: maximum height of 375px, maximum width of 435px
        /// - Mobile (non-retina displays): maximum height of 375px, maximum width of 280px
        /// - Mobile (retina displays): maximum height of 750px, maximum width of 560px
        /// Twitter will not create a photo card unless the twitter:image is of a minimum size of 280px wide by 150px tall. 
        /// Images will not be cropped unless they have an exceptional aspect ratio. 
        /// Images will be fetched and proxied by Twitter to ensure a high quality of service and SSL security for users.
        /// Specifying the width and height using twitter:image:width and twitter:image:height helps Twitter more accurately 
        /// preserve the aspect ratio of the image when resizing.
        /// Photo Cards are the only type of card which support a blank title, even if you are not using Open Graph.
        /// Animated gifs are currently supported in Twitter Cards via the Player Card.
        /// </summary>
        [HtmlAttributeName(ImageAttributeName)]
        public TwitterImage Image { get; set; }

        /// <summary>
        /// Gets or sets the title of your content as it should appear in the card. You may specify an empty 
        /// string if you wish no title to render.
        /// </summary>
        [HtmlAttributeName(TitleAttributeName)]
        public string Title { get; set; }

        /// <summary>
        /// Gets the type of the Twitter card.
        /// </summary>
        public override TwitterCardType Type { get { return TwitterCardType.Photo; } }

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
            if (this.Image != null)
            {
                stringBuilder.AppendMetaNameContent("twitter:image", this.Image.ImageUrl);
                stringBuilder.AppendMetaNameContentIfNotNull("twitter:image:height", this.Image.Height);
                stringBuilder.AppendMetaNameContentIfNotNull("twitter:image:width", this.Image.Width);
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

            if (this.Image == null) { throw new ArgumentNullException(nameof(this.Image)); }
        }

        #endregion
    }
}
