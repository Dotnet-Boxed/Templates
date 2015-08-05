namespace Boilerplate.Web.Mvc.Experimental
{
    using Boilerplate.Web.Mvc.OpenGraph;
    using Boilerplate.Web.Mvc.Twitter;

    /// <summary>
    /// A base class for a view's model.
    /// </summary>
    public class ViewModel
    {
        /// <summary>
        /// Gets or sets the author of the view. This is an optional property.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the description of the view. This is used by search engines and you should aim for your description to be more than 160 characters.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the title of the view. The title of the page. Appears in the browser window title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Open Graph metadata. See <see cref="OpenGraphMetadata"/> for more information.
        /// </summary>
        public OpenGraphMetadata OpenGraph { get; set; }

        /// <summary>
        /// Gets or sets the twitter card. See <see cref="TwitterCard"/> for more information.
        /// </summary>
        public TwitterCard TwitterCard { get; set; }
    }
}