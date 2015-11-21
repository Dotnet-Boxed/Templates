namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// An object representing a website. This object type is part of the Open Graph standard.
    /// See http://ogp.me/
    /// </summary>
    [HtmlTargetElement(
        "open-graph-website", 
        Attributes = TitleAttributeName + "," + MainImageAttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphWebsite : OpenGraphMetadata
    {
        #region Public Properties

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "website: http://ogp.me/ns/website#"; } }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.Website; } }

        #endregion
    }
}
