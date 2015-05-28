namespace Boilerplate.Web.Mvc.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// This object represents a music playlist, an ordered collection of songs from a collection of artists. This object type is part of the Open 
    /// Graph standard.
    /// </summary>
    public class OpenGraphMusicPlaylist : OpenGraphMetadata
    {
        private readonly IEnumerable<string> songUrls;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphMusicPlaylist"/> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        /// <param name="songUrls">The URL's to the pages about the songs on this playlist. This URL must contain profile meta tags <see cref="OpenGraphMusicSong"/>.</param>
        public OpenGraphMusicPlaylist(string title, OpenGraphImage image, IEnumerable<string> songUrls)
            : base(title, image)
        {
            if (songUrls == null)
            {
                throw new ArgumentNullException("songUrls");
            }

            this.SongDisc = 1;
            this.SongTrack = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphMusicPlaylist"/> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        /// <param name="songUrls">The URL's to the pages about the songs on this playlist. This URL must contain profile meta tags <see cref="OpenGraphMusicSong"/>.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph.</param>
        public OpenGraphMusicPlaylist(string title, OpenGraphImage image, IEnumerable<string> songUrls, string url)
            : base(title, image, url)
        {
            if (songUrls == null)
            {
                throw new ArgumentNullException("songUrls");
            }

            this.songUrls = songUrls;
            this.SongDisc = 1;
            this.SongTrack = 1;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the URL to the page about the creator of the playlist. This URL must contain profile meta tags <see cref="OpenGraphProfile"/>.
        /// </summary>
        public string CreatorUrl { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "music: http://ogp.me/ns/music#"; } }

        /// <summary>
        /// Gets or sets which disc in the album the song is from.
        /// </summary>
        public int SongDisc { get; set; }

        /// <summary>
        /// Gets or sets which track in the album the song is from.
        /// </summary>
        public int SongTrack { get; set; }

        /// <summary>
        /// Gets the URL's to the pages about the songs on this playlist. This URL must contain profile meta tags <see cref="OpenGraphMusicSong"/>.
        /// </summary>
        public IEnumerable<string> SongUrls { get { return this.songUrls; } }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.MusicPlaylist; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <see cref="stringBuilder" /> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaIfNotNull("music:song", this.SongUrls);

            // The number of songs on the playlist. This is a Facebook specific property.
            stringBuilder.AppendMetaIfNotNull("music:song_count", this.SongUrls.Count());

            stringBuilder.AppendMeta("music:song:disc", this.SongDisc);
            stringBuilder.AppendMeta("music:song:track", this.SongTrack);
            stringBuilder.AppendMetaIfNotNull("music:creator", this.CreatorUrl);
        }

        #endregion
    }
}
