namespace Boilerplate.Web.Mvc.OpenGraph
{
    using System.Text;

    /// <summary>
    /// This object type represents a person. While appropriate for celebrities, artists, or musicians, this object type can be used for the profile of 
    /// any individual. This object type is part of the Open Graph standard.
    /// </summary>
    public class OpenGraphProfile : OpenGraphMetadata
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphProfile"/> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        public OpenGraphProfile(string title, OpenGraphImage image)
            : base(title, image)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphProfile"/> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph.</param>
        public OpenGraphProfile(string title, OpenGraphImage image, string url)
            : base(title, image, url)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the name normally given to an individual by a parent or self-chosen.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        public OpenGraphGender? Gender { get; set; }

        /// <summary>
        /// Gets or sets the name inherited from a family or marriage and by which the individual is commonly known.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "profile: http://ogp.me/ns/profile#"; } }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.Profile; } }

        /// <summary>
        /// Gets or sets the short unique string to identify them.
        /// </summary>
        public string Username { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <see cref="stringBuilder" /> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaIfNotNull("profile:first_name", this.FirstName);
            stringBuilder.AppendMetaIfNotNull("profile:last_name", this.LastName);
            stringBuilder.AppendMetaIfNotNull("profile:username", this.Username);

            if (this.Gender.HasValue)
            {
                stringBuilder.AppendMeta("profile:gender", this.Gender.Value.ToLowercaseString());
            }
        } 

        #endregion
    }
}
