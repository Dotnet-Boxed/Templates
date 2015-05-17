namespace MvcBoilerplate.Framework.OpenGraph
{
    public class OpenGraphWebsite : OpenGraphMetadata
    {
        public OpenGraphWebsite(string title, string url, Image image)
            : base(title, url, image)
        {
        }

        public override string Namespace { get { return "http://ogp.me/ns/website#"; } }

        public override OpenGraphType Type { get { return OpenGraphType.Website; } }
    }
}
