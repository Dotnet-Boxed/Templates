namespace Boilerplate.AspNetCore.TagHelpers.OpenGraph
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    /// <summary>
    /// Adds the open graph prefix attribute to the head tag. The prefix is different depending on the type of open
    /// graph object being used.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" />
    [HtmlTargetElement("head", Attributes = EnabledAttributeName)]
    public class OpenGraphPrefixTagHelper : TagHelper
    {
        private const string PrefixAttributeName = "prefix";
        private const string EnabledAttributeName = "asp-open-graph-prefix";

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="OpenGraphPrefixTagHelper"/> is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        [HtmlAttributeName(EnabledAttributeName)]
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the view context. Workaround for context.Items not working across _Layout.cshtml and
        /// Index.cshtml using ViewContext. See https://github.com/aspnet/Mvc/issues/3233 and
        /// https://github.com/aspnet/Razor/issues/564.
        /// </summary>
        /// <value>
        /// The view context.
        /// </value>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <inhericdoc />
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (this.Enabled)
            {
                await output.GetChildContentAsync().ConfigureAwait(false);

                // Workaround for context.Items not working across _Layout.cshtml and Index.cshtml using ViewContext.
                // https://github.com/aspnet/Mvc/issues/3233 and https://github.com/aspnet/Razor/issues/564
                if (this.ViewContext.ViewData.ContainsKey(nameof(OpenGraphPrefixTagHelper)))
                {
                    string namespaces = (string)this.ViewContext.ViewData[nameof(OpenGraphPrefixTagHelper)];
                    output.Attributes.Add(PrefixAttributeName, namespaces);
                }

                // if (context.Items.ContainsKey(typeof(OpenGraphMetadata)))
                // {
                //     string namespaces = context.Items[typeof(OpenGraphMetadata)] as string;
                //     output.Attributes.Add(PrefixAttributeName, namespaces);
                // }
            }
        }
    }
}
