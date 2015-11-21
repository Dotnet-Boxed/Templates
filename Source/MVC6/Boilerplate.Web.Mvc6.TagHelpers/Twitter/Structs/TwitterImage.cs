namespace Boilerplate.Web.Mvc.TagHelpers.Twitter
{
    using System;

    /// <summary>
    /// An image used in a Twitter card. The Image must be less than 1MB in size.
    /// </summary>
    public class TwitterImage
    {
        private string imageUrl;

        #region Constructors
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterImage"/> class.
        /// </summary>
        /// <param name="imageUrl">The image URL. The Image must be less than 1MB in size.</param>
        public TwitterImage(string imageUrl)
        {
            if (imageUrl == null) { throw new ArgumentNullException(nameof(imageUrl)); }

            this.imageUrl = imageUrl;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterImage"/> class.
        /// </summary>
        /// <param name="imageUrl">The image URL. The Image must be less than 1MB in size.</param>
        /// <param name="width">The width of the image in pixels to help Twitter accurately preserve the aspect ratio 
        /// of the image when resizing. This property is optional.</param>
        /// <param name="height">The height of the image in pixels to help Twitter accurately preserve the aspect ratio 
        /// of the image when resizing. This property is optional.</param>
        public TwitterImage(string imageUrl, int width, int height) : this(imageUrl)
        {
            this.Height = height;
            this.Width = width;
        }

        #endregion

        #region Public Properties
        
        /// <summary>
        /// Gets or sets the height of the image in pixels to help Twitter accurately preserve the aspect ratio of 
        /// the image when resizing. This property is optional.
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Gets the image URL. The Image must be less than 1MB in size.
        /// </summary>
        public string ImageUrl { get { return this.imageUrl; } }

        /// <summary>
        /// Gets or sets the width of the image in pixels to help Twitter accurately preserve the aspect ratio of 
        /// the image when resizing. This property is optional.
        /// </summary>
        public int? Width { get; set; } 

        #endregion
    }
}
