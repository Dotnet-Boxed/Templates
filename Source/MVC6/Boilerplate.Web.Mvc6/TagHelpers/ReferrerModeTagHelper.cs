namespace Boilerplate.Web.Mvc.TagHelpers
{
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// Meta tag <see cref="TagHelper"/> which controls what is sent in the HTTP referrer header when a client 
    /// navigates from your page to an external site.
    /// </summary>
    [HtmlTargetElement("meta", Attributes = ReferrerAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class ReferrerModeTagHelper : TagHelper
    {
        private const string ContentAttributeName = "content";
        private const string NameAttributeName = "name";
        private const string ReferrerAttributeName = "asp-referrer";

        /// <summary>
        /// Gets or sets the referrer mode, which controls what is sent in the HTTP referrer header when a client 
        /// navigates from your page to an external site. This is a required property.
        /// </summary>
        [HtmlAttributeName(ReferrerAttributeName)]
        public ReferrerMode Referrer { get; set; }

        /// <summary>
        /// Synchronously executes the <see cref="TagHelper"/> with the given context and output.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes[NameAttributeName] = "referrer";
            output.Attributes[ContentAttributeName] = this.Referrer.ToLowercaseString();
        }
    }
}
