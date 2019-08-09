namespace ApiTemplate.ViewModels
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Holds metadata about a page of items.
    /// </summary>
    public class PageInfo
    {
        /// <summary>
        /// Gets or sets the count of items.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has a next page.
        /// </summary>
        public bool HasNextPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has a previous page.
        /// </summary>
        public bool HasPreviousPage { get; set; }

        /// <summary>
        /// Gets or sets the next page URL.
        /// </summary>
        public Uri NextPageUrl { get; set; }

        /// <summary>
        /// Gets or sets the previous page URL.
        /// </summary>
        public Uri PreviousPageUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL to the first page.
        /// </summary>
        public Uri FirstPageUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL to the last page.
        /// </summary>
        public Uri LastPageUrl { get; set; }

        /// <summary>
        /// Gets the Link HTTP header value to add URL's to next, previous, first and last pages.
        /// See https://tools.ietf.org/html/rfc5988#page-6
        /// There is a standard list of link relation types e.g. next, previous, first and last.
        /// See https://www.iana.org/assignments/link-relations/link-relations.xhtml
        /// </summary>
        /// <returns>The Link HTTP header value.</returns>
        public string ToLinkHttpHeaderValue()
        {
            var values = new List<string>(4);

            if (this.HasNextPage && this.NextPageUrl != null)
            {
                values.Add(this.GetLinkValueItem("next", this.NextPageUrl));
            }

            if (this.HasPreviousPage && this.PreviousPageUrl != null)
            {
                values.Add(this.GetLinkValueItem("previous", this.PreviousPageUrl));
            }

            if (this.FirstPageUrl != null)
            {
                values.Add(this.GetLinkValueItem("first", this.FirstPageUrl));
            }

            if (this.LastPageUrl != null)
            {
                values.Add(this.GetLinkValueItem("last", this.LastPageUrl));
            }

            return string.Join(", ", values);
        }

        private string GetLinkValueItem(string rel, Uri url) =>
            FormattableString.Invariant($"<{url}>; rel=\"{rel}\"");
    }
}
