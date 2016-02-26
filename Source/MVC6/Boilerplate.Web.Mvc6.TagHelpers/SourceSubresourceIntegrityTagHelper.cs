namespace Boilerplate.Web.Mvc.TagHelpers
{
    using Microsoft.AspNet.Razor.TagHelpers;
    using Microsoft.Extensions.Caching.Distributed;

    [HtmlTargetElement(Attributes = SrcAttributeName + "," + SubresourceIntegrityAttributeName)]
    public class SourceSubresourceIntegrityTagHelper : SubresourceIntegrityTagHelper
    {
        private const string SrcAttributeName = "src";

        public SourceSubresourceIntegrityTagHelper(IDistributedCache distributedCache)
            : base(distributedCache)
        {
        }

        protected override string UrlAttributeName { get { return SrcAttributeName; } }
    }
}
