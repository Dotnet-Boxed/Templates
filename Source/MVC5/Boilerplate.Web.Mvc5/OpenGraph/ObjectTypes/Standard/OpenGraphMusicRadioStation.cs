namespace Boilerplate.Web.Mvc.OpenGraph
{
    using System;
    using System.Text;

    /// <summary>
    /// This object represents a 'radio' station of a stream of audio. The audio properties should be used to identify the location of the stream itself. 
    /// This object type is part of the Open Graph standard.
    /// See http://ogp.me/
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/music.radio_station/
    /// </summary>
    public class OpenGraphMusicRadioStation : OpenGraphMetadata
    {
        #region Constructors
        
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphMusicRadioStation"/> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph.</param>
        public OpenGraphMusicRadioStation(string title, OpenGraphImage image, string url = null)
            : base(title, image, url)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the URL to the page about the creator of the radio station. This URL must contain profile meta tags <see cref="OpenGraphProfile"/>.
        /// </summary>
        public string CreatorUrl { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "music: http://ogp.me/ns/music#"; } }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.MusicRadioStation; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaPropertyContentIfNotNull("music:creator", this.CreatorUrl);
        }

        #endregion
    }
}
