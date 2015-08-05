namespace Boilerplate.Web.Mvc.OpenGraph
{
    using System;

    /// <summary>
    /// Represents an actor in a video.
    /// </summary>
    public class OpenGraphActor
    {
        private readonly string actorUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphActor"/> class.
        /// </summary>
        /// <param name="actorUrl">The URL to the page about the actor. This URL must contain profile meta tags <see cref="OpenGraphProfile"/>.</param>
        public OpenGraphActor(string actorUrl)
        {
            if (actorUrl == null)
            {
                throw new ArgumentNullException("actorUrl");
            }

            this.actorUrl = actorUrl;
        }

        /// <summary>
        /// Gets the URL to the page about the actor. This URL must contain profile meta tags <see cref="OpenGraphProfile"/>.
        /// </summary>
        public string ActorUrl { get { return this.actorUrl; } }

        /// <summary>
        /// Gets or sets the role the actor played.
        /// </summary>
        public string Role { get; set; }
    }
}
