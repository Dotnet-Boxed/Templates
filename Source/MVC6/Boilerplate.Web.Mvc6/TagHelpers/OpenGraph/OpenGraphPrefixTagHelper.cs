namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Razor.Runtime.TagHelpers;

    [TargetElement("head", Attributes = EnabledAttributeName)]
    public class OpenGraphPrefixTagHelper : TagHelper
    {
        private const string PrefixAttributeName = "prefix";
        private const string EnabledAttributeName = "asp-open-graph-prefix";

        [HtmlAttributeName(EnabledAttributeName)]
        public bool Enabled { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (this.Enabled)
            {
                context.Items.Add(typeof(OpenGraphMetadata), null);

                await context.GetChildContentAsync();

                string namespaces = context.Items[typeof(OpenGraphMetadata)] as string;
                if (namespaces != null)
                {
                    output.Attributes.Add(PrefixAttributeName, namespaces);
                }
            }
        }
    }
}
