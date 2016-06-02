namespace Boilerplate.AspNetCore.TagHelpers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Microsoft.Extensions.Caching.Distributed;

    /// <inheritdoc />
    [HtmlTargetElement(Attributes = HrefAttributeName + "," + SubresourceIntegrityHrefAttributeName)]
    public class HrefSubresourceIntegrityTagHelper : SubresourceIntegrityTagHelper
    {
        private const string HrefAttributeName = "href";
        private const string SubresourceIntegrityHrefAttributeName = "asp-subresource-integrity-href";

        /// <summary>
        /// Initializes a new instance of the <see cref="HrefSubresourceIntegrityTagHelper"/> class.
        /// </summary>
        /// <param name="distributedCache">The distributed cache.</param>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        /// <param name="urlHelper">The URL helper.</param>
        public HrefSubresourceIntegrityTagHelper(
            IDistributedCache distributedCache,
            IHostingEnvironment hostingEnvironment,
            IUrlHelper urlHelper)
            : base(distributedCache, hostingEnvironment, urlHelper)
        {
        }

        /// <inheritdoc />
        [HtmlAttributeName(SubresourceIntegrityHrefAttributeName)]
        public override string Source
        {
            get { return base.Source; }
            set { base.Source = value; }
        }

        /// <inheritdoc />
        protected override string UrlAttributeName
        {
            get { return HrefAttributeName; }
        }
    }
}
