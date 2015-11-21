namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// This object represents a music playlist, an ordered collection of songs from a collection of artists. This object type is part of the Open 
    /// Graph standard.
    /// See http://ogp.me/
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/music.playlist/
    /// </summary>
    [HtmlTargetElement(
        "open-graph-music-playlist", 
        Attributes = TitleAttributeName + "," + MainImageAttributeName + "," + SongUrlsAttributeName + "," + SongDiscAttributeName + "," + SongTrackAttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphMusicPlaylist : OpenGraphMetadata
    {
        #region Constants

        private const string CreatorUrlAttributeName = "creator-url";
        private const string SongDiscAttributeName = "song-disc";
        private const string SongTrackAttributeName = "song-track";
        private const string SongUrlsAttributeName = "song-urls";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the URL to the page about the creator of the playlist. This URL must contain profile meta tags <see cref="OpenGraphProfile"/>.
        /// </summary>
        [HtmlAttributeName(CreatorUrlAttributeName)]
        public string CreatorUrl { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "music: http://ogp.me/ns/music#"; } }

        /// <summary>
        /// Gets or sets which disc in the album the song is from.
        /// </summary>
        [HtmlAttributeName(SongDiscAttributeName)]
        public int SongDisc { get; set; }

        /// <summary>
        /// Gets or sets which track in the album the song is from.
        /// </summary>
        [HtmlAttributeName(SongTrackAttributeName)]
        public int SongTrack { get; set; }

        /// <summary>
        /// Gets or sets the URL's to the pages about the songs on this playlist. This URL must contain profile meta tags <see cref="OpenGraphMusicSong"/>.
        /// </summary>
        [HtmlAttributeName(SongUrlsAttributeName)]
        public IEnumerable<string> SongUrls { get; set; }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.MusicPlaylist; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaPropertyContentIfNotNull("music:song", this.SongUrls);

            // The number of songs on the playlist. This is a Facebook specific property.
            stringBuilder.AppendMetaPropertyContentIfNotNull("music:song_count", this.SongUrls.Count());

            stringBuilder.AppendMetaPropertyContent("music:song:disc", this.SongDisc);
            stringBuilder.AppendMetaPropertyContent("music:song:track", this.SongTrack);
            stringBuilder.AppendMetaPropertyContentIfNotNull("music:creator", this.CreatorUrl);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Checks that this instance is valid and throws exceptions if not valid.
        /// </summary>
        protected override void Validate()
        {
            base.Validate();

            if (this.SongUrls == null) { throw new ArgumentNullException(nameof(this.SongUrls)); }
        }

        #endregion
    }
}
