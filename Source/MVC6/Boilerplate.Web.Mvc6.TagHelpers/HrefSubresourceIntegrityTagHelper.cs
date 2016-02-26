namespace Boilerplate.Web.Mvc.TagHelpers
{
    using Microsoft.AspNet.Razor.TagHelpers;
    using Microsoft.Extensions.Caching.Distributed;

    [HtmlTargetElement(Attributes = HrefAttributeName + "," + SubresourceIntegrityAttributeName)]
    public class HrefSubresourceIntegrityTagHelper : SubresourceIntegrityTagHelper
    {
        private const string HrefAttributeName = "href";

        public HrefSubresourceIntegrityTagHelper(IDistributedCache distributedCache)
            : base(distributedCache)
        {
        }

        protected override string UrlAttributeName { get { return HrefAttributeName; } }
    }
}
