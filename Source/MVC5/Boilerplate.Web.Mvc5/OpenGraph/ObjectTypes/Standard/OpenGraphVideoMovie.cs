namespace Boilerplate.Web.Mvc.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// This object type represents a movie, and contains references to the actors and other professionals involved in its production. A movie is 
    /// defined by us as a full-length feature or short film. Do not use this type to represent movie trailers, movie clips, user-generated video 
    /// content, etc. This object type is part of the Open Graph standard.
    /// </summary>
    public class OpenGraphVideoMovie : OpenGraphMetadata
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphVideoMovie"/> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        public OpenGraphVideoMovie(string title, OpenGraphImage image)
            : base(title, image)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphVideoMovie"/> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph.</param>
        public OpenGraphVideoMovie(string title, OpenGraphImage image, string url)
            : base(title, image, url)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the actors in the movie.
        /// </summary>
        public IEnumerable<OpenGraphActor> Actors { get; set; }

        /// <summary>
        /// Gets or sets the URL's to the pages about the directors. This URL's must contain profile meta tags <see cref="OpenGraphProfile"/>.
        /// </summary>
        public IEnumerable<string> DirectorUrls { get; set; }

        /// <summary>
        /// Gets or sets the duration of the movie in seconds.
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "video: http://ogp.me/ns/video#"; } }

        /// <summary>
        /// Gets or sets the release date of the movie.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the tag words associated with the movie.
        /// </summary>
        public IEnumerable<string> Tags { get; set; }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.VideoMovie; } }

        /// <summary>
        /// Gets or sets the URL's to the pages about the writers. This URL's must contain profile meta tags <see cref="OpenGraphProfile"/>.
        /// </summary>
        public IEnumerable<string> WriterUrls { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <see cref="stringBuilder" /> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            if (this.Actors != null)
            {
                foreach (OpenGraphActor actor in this.Actors)
                {
                    stringBuilder.AppendMetaIfNotNull("video:actor", actor.ActorUrl);
                    stringBuilder.AppendMetaIfNotNull("video:actor:role", actor.Role);
                }
            }

            stringBuilder.AppendMetaIfNotNull("video:director", this.DirectorUrls);
            stringBuilder.AppendMetaIfNotNull("video:writer", this.WriterUrls);
            stringBuilder.AppendMetaIfNotNull("video:duration", this.Duration);
            stringBuilder.AppendMetaIfNotNull("video:release_date", this.ReleaseDate);
            stringBuilder.AppendMetaIfNotNull("video:tag", this.Tags);
        }

        #endregion
    }
}
