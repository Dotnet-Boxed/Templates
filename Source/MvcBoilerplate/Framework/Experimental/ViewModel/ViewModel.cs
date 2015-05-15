namespace MvcBoilerplate.Framework
{
    using MvcBoilerplate.Framework.OpenGraph;

    public class ViewModel
    {
        public string Author { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public OpenGraphMetadata OpenGraph { get; set; }

        /// <summary>
        /// Gets or sets the twitter card. https://dev.twitter.com/cards https://blog.bufferapp.com/twitter-cards-guide
        /// </summary>
        public TwitterCard TwitterCard { get; set; }
    }
}