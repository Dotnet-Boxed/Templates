namespace MvcBoilerplate.Framework.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;


    /// <summary>
    /// The Open Graph protocol enables any web page to become a rich object in a social graph. 
    /// For instance, this is used on Facebook to allow any web page to have the same functionality 
    /// as any other object on Facebook (See http://ogp.me).
    /// </summary>
    public abstract class OpenGraphMetadata : IHtmlString
    {
        private readonly List<Image> media;
        private readonly string title;
        private readonly string url;

        #region Constructors

        public OpenGraphMetadata(string title, string url, Image image)
        {
            if (title == null)
            {
                throw new ArgumentNullException("title");
            }

            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            this.title = title;
            this.url = url;
            this.media = new List<Image>();
            this.media.Add(image);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the collection of alternate locales this page is available in.
        /// </summary>
        public IEnumerable<string> AlternateLocales { get; set; }

        /// <summary>
        /// Gets the audio files which should represent your object within the graph. Use the Media property to add an audio file.
        /// </summary>
        public IEnumerable<Audio> Audio { get { return this.media.OfType<Audio>(); } }

        /// <summary>
        /// Gets or sets a one to two sentence description of your object.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the word that appears before this object's title in a sentence. An enum of (a, an, the, "", auto). 
        /// If auto is chosen, the consumer of your data should chose between "a" or "an". Default is "" (blank).
        /// </summary>
        public Determiner Determiner { get; set; }

        /// <summary>
        /// Gets the images which should represent your object within the graph. Use the Media property to add an image.
        /// </summary>
        public IEnumerable<Image> Images { get { return this.media.OfType<Image>(); } }

        /// <summary>
        /// Gets or sets the locale these tags are marked up in. Of the format language_TERRITORY. Default is en_US.
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// Gets the images, videos or audio which should represent your object within the graph.
        /// </summary>
        public ICollection<Media> Media { get; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public abstract string Namespace { get; }

        /// <summary>
        /// Gets or sets the name of the site. if your object is part of a larger web site, the name which should be displayed 
        /// for the overall site. e.g. "IMDb".
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// Gets the title of your object as it should appear within the graph, e.g. "The Rock".
        /// </summary>
        public string Title { get { return this.title; } }

        /// <summary>
        /// Gets the type of your object, e.g. "video.movie". Depending on the type you specify, other properties may also be required.
        /// </summary>
        public abstract OpenGraphType Type { get; }

        /// <summary>
        /// Gets the canonical URL of your object that will be used as its permanent ID in the graph, e.g. "http://www.imdb.com/title/tt0117500/".
        /// </summary>
        public string Url { get { return this.url; } }

        /// <summary>
        /// Gets the videos which should represent your object within the graph. Use the Media property to add a video file.
        /// </summary>
        public IEnumerable<Video> Videos { get { return this.media.OfType<Video>(); } }

        #endregion

        #region Public Methods

        public string ToHtmlString()
        {
            return this.ToString();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            this.ToString(stringBuilder);
            return stringBuilder.ToString();
        }

        public virtual void ToString(StringBuilder stringBuilder)
        {
            stringBuilder.Append("<meta property=\"og:title\" content=\"");
            stringBuilder.Append(this.Title);
            stringBuilder.Append("\">");
            //    
            //       < meta property = "og:type" content = "@Model.OpenGraph.Type" >
            //          < meta property = "og:url" content = "@Model.OpenGraph.Url" >
            //             < meta property = "og:image" content = "@Model.OpenGraph.Image.Url" >
        }

        #endregion
    }
}
