namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;
    using System.Text;

    /// <summary>
    /// An media which should represent your object within the graph.
    /// </summary>
    public abstract class OpenGraphMedia
    {
        private readonly string mediaUrl;
        private readonly string mediaUrlSecure;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphMedia"/> class.
        /// </summary>
        /// <param name="mediaUrl">The media URL.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="mediaUrl"/> is <c>null</c>.</exception>
        public OpenGraphMedia(string mediaUrl)
        {
            if (mediaUrl == null) { throw new ArgumentNullException(nameof(mediaUrl)); }

            // If the URL starts with https.
            if (mediaUrl[4] == 's')
            {
                this.mediaUrl = new UriBuilder(mediaUrl)
                {
                    Port = -1,
                    Scheme = "https"
                }.ToString();
                this.mediaUrlSecure = mediaUrl;
            }
            else
            {
                this.mediaUrl = mediaUrl;
            }
        }
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the MIME type of the media e.g. media/jpeg. This is optional if your media URL ends with a file extension, 
        /// otherwise it is recommended.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets the absolute HTTP media URL which should represent your object within the graph.
        /// </summary>
        public string Url { get { return this.mediaUrl; } }

        /// <summary>
        /// Gets the absolute HTTPS media URL which should represent your object within the graph.
        /// </summary>
        public string UrlSecure { get { return this.mediaUrlSecure; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public abstract void ToString(StringBuilder stringBuilder); 

        #endregion
    }
}
