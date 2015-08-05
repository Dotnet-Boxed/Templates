namespace Boilerplate.Web.Mvc.Twitter
{
    using System;
    using System.Text;


    /// <summary>
    /// The Photo Card puts the image front and center in the Tweet. Clicking on the photo expands it to a 
    /// richer, detailed view. On twitter.com and mobile clients, the image appears below the tweet text.
    /// See https://dev.twitter.com/cards/types/photo
    /// </summary>
    public class PhotoTwitterCard : TwitterCard
    {
        private readonly TwitterImage image;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoTwitterCard"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="image">The image to use.</param>
        /// <exception cref="System.ArgumentNullException">username or image is <c>null</c>.</exception>
        public PhotoTwitterCard(string username, TwitterImage image)
            : base(username)
        {
            if (image == null) { throw new ArgumentNullException("image"); }

            this.image = image;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Twitter username of the creator of the content on the page e.g. @RehanSaeedUK. This is an optional property.
        /// </summary>
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
        public TwitterImage Image { get { return this.image; } }

        /// <summary>
        /// Gets or sets the title of your content as it should appear in the card. You may specify an empty 
        /// string if you wish no title to render.
        /// </summary>
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
    }
}
