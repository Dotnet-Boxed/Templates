namespace MvcBoilerplate.Framework.OpenGraph
{
    using System;

    /// <summary>
    /// An media which should represent your object within the graph.
    /// </summary>
    public abstract class Media
    {
        private readonly string mediaUrl;
        private readonly string mediaUrlSecure;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Media"/> class.
        /// </summary>
        /// <param name="mediaUrl">The media URL.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if mediaUrl is <c>null</c>.</exception>
        public Media(string mediaUrl)
        {
            if (mediaUrl == null)
            {
                throw new ArgumentNullException("mediaUrl");
            }

            if (mediaUrl.StartsWith("https"))
            {
                this.mediaUrl = new UriBuilder(mediaUrl) { Scheme = "http" }.ToString();
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
        public string UrlSecure { get { return this.mediaUrl; } }

        #endregion
    }
}
