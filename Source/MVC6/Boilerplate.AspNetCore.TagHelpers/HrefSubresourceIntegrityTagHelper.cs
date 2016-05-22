namespace Boilerplate.AspNetCore.TagHelpers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Microsoft.Extensions.Caching.Distributed;

    [HtmlTargetElement(Attributes = HrefAttributeName + "," + SubresourceIntegrityHrefAttributeName)]
    public class HrefSubresourceIntegrityTagHelper : SubresourceIntegrityTagHelper
    {
        private const string HrefAttributeName = "href";
        private const string SubresourceIntegrityHrefAttributeName = "asp-subresource-integrity-href";

        public HrefSubresourceIntegrityTagHelper(
            IDistributedCache distributedCache,
            IHostingEnvironment hostingEnvironment,
            IUrlHelper urlHelper)
            : base(distributedCache, hostingEnvironment, urlHelper)
        {
        }

        [HtmlAttributeName(SubresourceIntegrityHrefAttributeName)]
        public override string Source
        {
            get { return base.Source; }
            set { base.Source = value; }
        }

        protected override string UrlAttributeName { get { return HrefAttributeName; } }
    }
}
