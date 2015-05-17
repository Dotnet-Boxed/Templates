namespace MvcBoilerplate.Framework.OpenGraph
{
    public class OpenGraphProfile : OpenGraphMetadata
    {
        public OpenGraphProfile(string title, string url, Image image)
            : base(title, url, image)
        {
        }

        public override string Namespace { get { return "http://ogp.me/ns/profile#"; } }

        /// <summary>
        /// Gets or sets the name normally given to an individual by a parent or self-chosen.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Gets or sets the name inherited from a family or marriage and by which the individual is commonly known.
        /// </summary>
        public string LastName { get; set; }

        public override OpenGraphType Type { get { return OpenGraphType.Profile; } }

        /// <summary>
        /// Gets or sets the short unique string to identify them.
        /// </summary>
        public string Username { get; set; }
    }
}
