namespace Boilerplate.Web.Mvc.TagHelpers
{
    using Microsoft.AspNet.Hosting;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Razor.TagHelpers;
    using Microsoft.Extensions.Caching.Distributed;

    [HtmlTargetElement(Attributes = SrcAttributeName + "," + SubresourceIntegritySrcAttributeName)]
    public class SrcSubresourceIntegrityTagHelper : SubresourceIntegrityTagHelper
    {
        private const string SrcAttributeName = "src";
        private const string SubresourceIntegritySrcAttributeName = "asp-subresource-integrity-src";

        public SrcSubresourceIntegrityTagHelper(
            IDistributedCache distributedCache,
            IHostingEnvironment hostingEnvironment,
            IUrlHelper urlHelper)
            : base(distributedCache, hostingEnvironment, urlHelper)
        {
        }

        [HtmlAttributeName(SubresourceIntegritySrcAttributeName)]
        public override string Source
        {
            get { return base.Source; }
            set { base.Source = value; }
        }

        protected override string UrlAttributeName { get { return SrcAttributeName; } }
    }
}
