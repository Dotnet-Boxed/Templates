namespace MvcBoilerplate.Framework.OpenGraph
{
    public class OpenGraphBook : OpenGraphMetadata
    {
        public OpenGraphBook(string title, string url, Image image)
            : base(title, url, image)
        {
        }

        /// <summary>
        /// Gets or sets the author who wrote the book.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the books unique ISBN number.
        /// </summary>
        public string ISBN { get; set; }

        public override string Namespace { get { return "http://ogp.me/ns/book#"; } }

        public override OpenGraphType Type { get { return OpenGraphType.Book; } }
    }
}
