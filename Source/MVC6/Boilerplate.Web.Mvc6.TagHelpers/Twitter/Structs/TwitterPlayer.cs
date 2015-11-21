namespace Boilerplate.Web.Mvc.TagHelpers.Twitter
{
    using System;

    /// <summary>
    /// A video used in a Twitter card. If the iframe is wider than 435px, the iframe player will be resized to fit a 
    /// max width of 435px, maintaining the original aspect ratio.
    /// </summary>
    public class TwitterPlayer
    {
        private readonly int height;
        private readonly string playerUrl;
        private readonly int width;

        #region Constructors
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterPlayer" /> class.
        /// </summary>
        /// <param name="playerUrl">The URL to an iframe player. This must be a HTTPS URL which does not generate active
        /// mixed content warnings in a web browser.</param>
        /// <param name="width">The width of the iFrame player in pixels.</param>
        /// <param name="height">The height of the iFrame player in pixels.</param>
        /// <exception cref="ArgumentNullException"><paramref name="playerUrl"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="width"/> and <paramref name="height"/> 
        /// must be more than zero.</exception>
        public TwitterPlayer(string playerUrl, int width, int height)
        {
            if (playerUrl == null) { throw new ArgumentNullException(nameof(playerUrl)); }
            if (width <= 0) { throw new ArgumentOutOfRangeException(nameof(width)); }
            if (height <= 0) { throw new ArgumentOutOfRangeException(nameof(height)); }

            this.height = height;
            this.playerUrl = playerUrl;
            this.width = width;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterPlayer" /> class.
        /// </summary>
        /// <param name="playerUrl">The URL to an iframe player. This must be a HTTPS URL which does not generate active
        /// mixed content warnings in a web browser.</param>
        /// <param name="width">The width of the iFrame player in pixels.</param>
        /// <param name="height">The height of the iFrame player in pixels.</param>
        /// <param name="streamContentType">The MIME type/subtype combination that describes the content contained in 
        /// twitter:player:stream. Takes the form specified in RFC 6381. Currently supported content_type values are 
        /// those defined in RFC 4337 (MIME Type Registration for MP4).</param>
        /// <param name="streamUrl">The URL to a raw stream that will be rendered in Twitter’s mobile applications 
        /// directly. If provided, the stream must be delivered in the MPEG-4 container format (the.mp4 extension). The 
        /// container can store a mix of audio and video with the following codecs: Video: H.264, Baseline Profile(BP), 
        /// Level 3.0, up to 640 x 480 at 30 fps. Audio: AAC, Low Complexity Profile(LC).</param>
        /// <exception cref="ArgumentNullException"><paramref name="playerUrl"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="width"/> and <paramref name="height"/> 
        /// must be more than zero.</exception>
        public TwitterPlayer(string playerUrl, int width, int height, string streamContentType, string streamUrl)
            : this(playerUrl, width, height)
        {
            this.StreamContentType = streamContentType;
            this.StreamUrl = streamUrl;
        }

        #endregion

        /// <summary>
        /// Gets the height of the iFrame player in pixels.
        /// </summary>
        public int Height { get { return this.height; } }

        /// <summary>
        /// Gets the URL to an iframe player. This must be a HTTPS URL which does not generate active 
        /// mixed content warnings in a web browser.
        /// </summary>
        public string PlayerUrl { get { return this.playerUrl; } }

        /// <summary>
        /// Gets or sets the MIME type/subtype combination that describes the content contained in 
        /// twitter:player:stream. Takes the form specified in RFC 6381. Currently supported content_type values are 
        /// those defined in RFC 4337 (MIME Type Registration for MP4).
        /// </summary>
        public string StreamContentType { get; set; }

        /// <summary>
        /// Gets or sets the URL to a raw stream that will be rendered in Twitter’s mobile applications directly. If 
        /// provided, the stream must be delivered in the MPEG-4 container format (the.mp4 extension). The container 
        /// can store a mix of audio and video with the following codecs: Video: H.264, Baseline Profile(BP), 
        /// Level 3.0, up to 640 x 480 at 30 fps. Audio: AAC, Low Complexity Profile(LC).
        /// </summary>
        public string StreamUrl { get; set; }

        /// <summary>
        /// Gets the width of the iFrame player in pixels.
        /// </summary>
        public int Width { get { return this.width; } }
    }
}
