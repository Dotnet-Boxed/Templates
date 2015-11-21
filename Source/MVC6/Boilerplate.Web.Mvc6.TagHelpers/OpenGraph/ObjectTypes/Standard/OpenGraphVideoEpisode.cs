namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// This object type represents an episode of a TV show and contains references to the actors and other professionals involved in its production. 
    /// An episode is defined by us as a full-length episode that is part of a series. This type must reference the series this it is part of.
    /// This object type is part of the Open Graph standard.
    /// See http://ogp.me/
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/video.episode/
    /// </summary>
    [HtmlTargetElement(
        "open-graph-video-episode", 
        Attributes = TitleAttributeName + "," + MainImageAttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphVideoEpisode : OpenGraphMetadata
    {
        #region Constants

        private const string ActorsAttributeName = "actors";
        private const string DirectorUrlsAttributeName = "director-urls";
        private const string DurationAttributeName = "duration";
        private const string ReleaseDateAttributeName = "release-date";
        private const string SeriesUrlAttributeName = "series-url";
        private const string TagsAttributeName = "tags";
        private const string WriterUrlsAttributeName = "writer-urls";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the actors in the episode.
        /// </summary>
        [HtmlAttributeName(ActorsAttributeName)]
        public IEnumerable<OpenGraphActor> Actors { get; set; }

        /// <summary>
        /// Gets or sets the URL's to the pages about the directors. This URL's must contain profile meta tags <see cref="OpenGraphProfile"/>.
        /// </summary>
        [HtmlAttributeName(DirectorUrlsAttributeName)]
        public IEnumerable<string> DirectorUrls { get; set; }

        /// <summary>
        /// Gets or sets the duration of the episode in seconds.
        /// </summary>
        [HtmlAttributeName(DurationAttributeName)]
        public int? Duration { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "video: http://ogp.me/ns/video#"; } }

        /// <summary>
        /// Gets or sets the release date of the episode.
        /// </summary>
        [HtmlAttributeName(ReleaseDateAttributeName)]
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the URL to the page about the television series. This URL's must contain television show meta tags <see cref="OpenGraphVideoTvShow"/>.
        /// </summary>
        [HtmlAttributeName(SeriesUrlAttributeName)]
        public string SeriesUrl { get; set; }

        /// <summary>
        /// Gets or sets the tag words associated with the episode.
        /// </summary>
        [HtmlAttributeName(TagsAttributeName)]
        public IEnumerable<string> Tags { get; set; }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.VideoEpisode; } }

        /// <summary>
        /// Gets or sets the URL's to the pages about the writers. This URL's must contain profile meta tags <see cref="OpenGraphProfile"/>.
        /// </summary>
        [HtmlAttributeName(WriterUrlsAttributeName)]
        public IEnumerable<string> WriterUrls { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            if (this.Actors != null)
            {
                foreach (OpenGraphActor actor in this.Actors)
                {
                    stringBuilder.AppendMetaPropertyContentIfNotNull("video:actor", actor.ActorUrl);
                    stringBuilder.AppendMetaPropertyContentIfNotNull("video:actor:role", actor.Role);
                }
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("video:director", this.DirectorUrls);
            stringBuilder.AppendMetaPropertyContentIfNotNull("video:writer", this.WriterUrls);
            stringBuilder.AppendMetaPropertyContentIfNotNull("video:duration", this.Duration);
            stringBuilder.AppendMetaPropertyContentIfNotNull("video:release_date", this.ReleaseDate);
            stringBuilder.AppendMetaPropertyContentIfNotNull("video:tag", this.Tags);
            stringBuilder.AppendMetaPropertyContentIfNotNull("video:series", this.SeriesUrl);
        }

        #endregion
    }
}
