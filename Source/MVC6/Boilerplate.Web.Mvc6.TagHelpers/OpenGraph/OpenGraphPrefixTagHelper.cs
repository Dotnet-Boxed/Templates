namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Mvc.Rendering;
    using Microsoft.AspNet.Mvc.ViewFeatures;
    using Microsoft.AspNet.Razor.TagHelpers;

    [HtmlTargetElement("head", Attributes = EnabledAttributeName)]
    public class OpenGraphPrefixTagHelper : TagHelper
    {
        private const string PrefixAttributeName = "prefix";
        private const string EnabledAttributeName = "asp-open-graph-prefix";

        [HtmlAttributeName(EnabledAttributeName)]
        public bool Enabled { get; set; }

        // Workaround for context.Items not working across _Layout.cshtml and Index.cshtml using ViewContext.
        // https://github.com/aspnet/Mvc/issues/3233 and https://github.com/aspnet/Razor/issues/564
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (this.Enabled)
            {
                await output.GetChildContentAsync();

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
