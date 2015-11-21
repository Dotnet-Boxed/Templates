namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using Microsoft.AspNet.Mvc.Rendering;

    /// <summary>
    /// Creates Open Graph meta tags. <see cref="OpenGraphMetadata"/> for more information.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Creates a string containing the Open Graph meta tags (Also used by Facebook). <see cref="OpenGraphMetadata"/> for more information.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="openGraphMetadata">The open graph metadata.</param>
        /// <returns>The meta tags.</returns>
        public static HtmlString OpenGraph(this IHtmlHelper htmlHelper, OpenGraphMetadata openGraphMetadata)
        {
            return new HtmlString(openGraphMetadata.ToString());
        }

        /// <summary>
        /// Creates a <see cref="string"/> representing the Open Graph, Facebook and object namespaces. The namespaces are added to the HTML head element.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="openGraphMetadata">The open graph metadata.</param>
        /// <returns>The Open Graph namespaces.</returns>
        public static HtmlString OpenGraphNamespace(this IHtmlHelper htmlHelper, OpenGraphMetadata openGraphMetadata)
        {
            return new HtmlString("prefix=\"" + openGraphMetadata.GetNamespaces() + "\"");
        }
    }
}
