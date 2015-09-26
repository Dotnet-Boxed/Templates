namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;
    using System.Text;
    using Microsoft.AspNet.Razor.Runtime.TagHelpers;

    /// <summary>
    /// This object represents a 'radio' station of a stream of audio. The audio properties should be used to identify the location of the stream itself. 
    /// This object type is part of the Open Graph standard.
    /// See http://ogp.me/
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/music.radio_station/
    /// </summary>
    [TargetElement(Attributes = nameof(Title) + "," + nameof(MainImage), TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphMusicRadioStation : OpenGraphMetadata
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphMusicRadioStation"/> class.
        /// </summary>
        public OpenGraphMusicRadioStation() : base()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphMusicRadioStation"/> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="mainImage">The main image which should represent your object within the graph. This is a required property.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph. Leave as <c>null</c> to get the URL of the current page.</param>
        public OpenGraphMusicRadioStation(string title, OpenGraphImage mainImage, string url = null)
            : base(title, mainImage, url)
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
