namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using Microsoft.AspNet.Mvc.Rendering;

    /// <summary>
    /// Creates Open Graph meta tags. <see cref="OpenGraphMetadata"/> for more information.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        private const string OgNamespace = "og: http://ogp.me/ns# ";
        private const string FacebookNamespace = "fb: http://ogp.me/ns/fb# ";

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
            string prefix;
            if ((openGraphMetadata.FacebookAdministrators == null) && 
                (openGraphMetadata.FacebookApplicationId == null) &&
                (openGraphMetadata.FacebookProfileId == null))
            {
                prefix = "prefix=\"" + OgNamespace + openGraphMetadata.Namespace + "\"";
            }
            else
            {
                prefix = "prefix=\"" + OgNamespace + FacebookNamespace + openGraphMetadata.Namespace + "\"";
            }
            
            return new HtmlString(prefix);
        }
    }
}
