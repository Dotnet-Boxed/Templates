namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// This object represents a single song. This object type is part of the Open Graph standard.
    /// See http://ogp.me/
    /// https://developers.facebook.com/docs/reference/opengraph/object-type/music.song/
    /// </summary>
    [HtmlTargetElement(
        "open-graph-music-song", 
        Attributes = TitleAttributeName + "," + MainImageAttributeName + "," + AlbumUrlsAttributeName + "," + AlbumDiscAttributeName + "," + AlbumTrackAttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphMusicSong : OpenGraphMetadata
    {
        #region Constants

        private const string AlbumDiscAttributeName = "album-disc";
        private const string AlbumTrackAttributeName = "album-track";
        private const string AlbumUrlsAttributeName = "album-urls";
        private const string DurationAttributeName = "duration";
        private const string ISRCAttributeName = "isrc";
        private const string MusicianUrlsAttributeName = "musician-urls";
        private const string ReleaseDateAttributeName = "release-date";
        private const string ReleaseTypeAttributeName = "release-type";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets which disc in the album the song is from.
        /// </summary>
        [HtmlAttributeName(AlbumDiscAttributeName)]
        public int AlbumDisc { get; set; }

        /// <summary>
        /// Gets or sets which track in the album the song is from.
        /// </summary>
        [HtmlAttributeName(AlbumTrackAttributeName)]
        public int AlbumTrack { get; set; }

        /// <summary>
        /// Gets or sets the URL's to the pages about the album the song comes from. This URL's must contain profile meta tags <see cref="OpenGraphMusicAlbum"/>.
        /// </summary>
        [HtmlAttributeName(AlbumUrlsAttributeName)]
        public IEnumerable<string> AlbumUrls { get; set; }

        /// <summary>
        /// Gets or sets the duration of the song in seconds.
        /// </summary>
        [HtmlAttributeName(DurationAttributeName)]
        public int? Duration { get; set; }

        /// <summary>
        /// Gets or sets the International Standard Recording Code (ISRC) for the song. This is a Facebook specific property and is not specified in the 
        /// Open Graph standard.
        /// </summary>
        [HtmlAttributeName(ISRCAttributeName)]
        public string ISRC { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "music: http://ogp.me/ns/music#"; } }

        /// <summary>
        /// Gets or sets the URL's to the pages about the musicians who wrote the song. This URL's must contain profile meta tags <see cref="OpenGraphProfile"/>.
        /// </summary>
        [HtmlAttributeName(MusicianUrlsAttributeName)]
        public IEnumerable<string> MusicianUrls { get; set; }

        /// <summary>
        /// Gets or sets the release date of the song. This is a Facebook specific property and is not specified in the Open Graph standard.
        /// </summary>
        [HtmlAttributeName(ReleaseDateAttributeName)]
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the release of the song. This is a Facebook specific property and not specified by the Open Graph standard.
        /// </summary>
        [HtmlAttributeName(ReleaseTypeAttributeName)]
        public OpenGraphMusicReleaseType? ReleaseType { get; set; }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.MusicSong; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaPropertyContentIfNotNull("music:duration", this.Duration);
            stringBuilder.AppendMetaPropertyContentIfNotNull("music:album", this.AlbumUrls);
            stringBuilder.AppendMetaPropertyContent("music:album:disc", this.AlbumDisc);
            stringBuilder.AppendMetaPropertyContent("music:album:track", this.AlbumTrack);
            stringBuilder.AppendMetaPropertyContentIfNotNull("music:musician", this.MusicianUrls);
            stringBuilder.AppendMetaPropertyContentIfNotNull("music:isrc", this.ISRC);
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

            if (this.AlbumUrls == null) { throw new ArgumentNullException(nameof(this.AlbumUrls)); }
        }

        #endregion
    }
}
