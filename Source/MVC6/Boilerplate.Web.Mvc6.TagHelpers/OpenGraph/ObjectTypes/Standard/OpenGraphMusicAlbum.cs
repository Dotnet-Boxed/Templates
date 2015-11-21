namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// This object represents a music album; in other words, an ordered collection of songs from an artist or a collection of artists. An album can 
    /// comprise multiple discs. This object type is part of the Open Graph standard.
    /// See http://ogp.me/
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/music.album/
    /// </summary>
    [HtmlTargetElement(
        "open-graph-music-album", 
        Attributes = TitleAttributeName + "," + MainImageAttributeName + "," + SongUrlsAttributeName + "," + SongDiscAttributeName + "," + SongTrackAttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphMusicAlbum : OpenGraphMetadata
    {
        #region Constants

        private const string MusicianUrlsAttributeName = "musician-urls";
        private const string ReleaseDateAttributeName = "release-date";
        private const string ReleaseTypeAttributeName = "release-type";
        private const string SongDiscAttributeName = "song-disc";
        private const string SongTrackAttributeName = "song-track";
        private const string SongUrlsAttributeName = "song-urls";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "music: http://ogp.me/ns/music#"; } }

        /// <summary>
        /// Gets or sets the URL's to the pages about the musicians who wrote the song. This URL must contain profile meta tags <see cref="OpenGraphProfile"/>.
        /// </summary>
        [HtmlAttributeName(MusicianUrlsAttributeName)]
        public IEnumerable<string> MusicianUrls { get; set; }

        /// <summary>
        /// Gets or sets the release date of the album.
        /// </summary>
        [HtmlAttributeName(ReleaseDateAttributeName)]
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the release of the album. This is a Facebook specific property and not specified by the Open Graph standard.
        /// </summary>
        [HtmlAttributeName(ReleaseTypeAttributeName)]
        public OpenGraphMusicReleaseType? ReleaseType { get; set; }

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
        /// Gets or sets the URL's to the pages about the songs on this album. This URL must contain profile meta tags <see cref="OpenGraphMusicSong"/>.
        /// </summary>
        [HtmlAttributeName(SongUrlsAttributeName)]
        public IEnumerable<string> SongUrls { get; set; }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.MusicAlbum; } }

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
            stringBuilder.AppendMetaPropertyContent("music:song:disc", this.SongDisc);
            stringBuilder.AppendMetaPropertyContent("music:song:track", this.SongTrack);
            stringBuilder.AppendMetaPropertyContentIfNotNull("music:musician", this.MusicianUrls);
            stringBuilder.AppendMetaPropertyContentIfNotNull("music:release_date", this.ReleaseDate);

            if (this.ReleaseType.HasValue)
            {
                stringBuilder.AppendMetaPropertyContent("music:release_type", this.ReleaseType.Value.ToLowercaseString());
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

            if (this.SongUrls == null) { throw new ArgumentNullException(nameof(this.SongUrls)); }
        }

        #endregion
    }
}
