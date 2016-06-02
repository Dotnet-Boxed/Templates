namespace Boilerplate.AspNetCore.TagHelpers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Microsoft.Extensions.Caching.Distributed;

    /// <inheritdoc />
    [HtmlTargetElement(Attributes = SrcAttributeName + "," + SubresourceIntegritySrcAttributeName)]
    public class SrcSubresourceIntegrityTagHelper : SubresourceIntegrityTagHelper
    {
        private const string SrcAttributeName = "src";
        private const string SubresourceIntegritySrcAttributeName = "asp-subresource-integrity-src";

        /// <summary>
        /// Initializes a new instance of the <see cref="SrcSubresourceIntegrityTagHelper"/> class.
        /// </summary>
        /// <param name="distributedCache">The distributed cache.</param>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        /// <param name="urlHelper">The URL helper.</param>
        public SrcSubresourceIntegrityTagHelper(
            IDistributedCache distributedCache,
            IHostingEnvironment hostingEnvironment,
            IUrlHelper urlHelper)
            : base(distributedCache, hostingEnvironment, urlHelper)
        {
        }

        /// <inheritdoc />
        [HtmlAttributeName(SubresourceIntegritySrcAttributeName)]
        public override string Source
        {
            get { return base.Source; }
            set { base.Source = value; }
        }

        /// <inheritdoc />
        protected override string UrlAttributeName
        {
            get { return SrcAttributeName; }
        }
    }
}
