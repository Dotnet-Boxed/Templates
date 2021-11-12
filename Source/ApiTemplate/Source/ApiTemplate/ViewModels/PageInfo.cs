namespace ApiTemplate.ViewModels;

#if Swagger
/// <summary>
/// Holds metadata about a page of items.
/// </summary>
#endif
public class PageInfo
{
    private const string NextLinkItem = "next";
    private const string PreviousLinkItem = "previous";
    private const string FirstLinkItem = "first";
    private const string LastLinkItem = "last";

#if Swagger
    /// <summary>
    /// Gets or sets the count of items.
    /// </summary>
    /// <example>10</example>
#endif
    public int Count { get; set; }

#if Swagger
    /// <summary>
    /// Gets or sets a value indicating whether this instance has a next page.
    /// </summary>
    /// <example>true</example>
#endif
    public bool HasNextPage { get; set; }

#if Swagger
    /// <summary>
    /// Gets or sets a value indicating whether this instance has a previous page.
    /// </summary>
    /// <example>false</example>
#endif
    public bool HasPreviousPage { get; set; }

#if Swagger
    /// <summary>
    /// Gets or sets the next page URL.
    /// </summary>
    /// <example>/cars?First=10&amp;After=MjAxOS0xMC0yNlQxNDozNDo1MC4xOTgwNzY2KzAwOjAw</example>
#endif
    public Uri? NextPageUrl { get; set; }

#if Swagger
    /// <summary>
    /// Gets or sets the previous page URL.
    /// </summary>
    /// <example>/cars?First=10&amp;Before=MjAxOS0xMC0yNlQxNDozNDo1MC4xOTgwNzY2KzAwOjAw</example>
#endif
    public Uri? PreviousPageUrl { get; set; }

#if Swagger
    /// <summary>
    /// Gets or sets the URL to the first page.
    /// </summary>
    /// <example>/cars?First=10</example>
#endif
    public Uri FirstPageUrl { get; set; } = default!;

#if Swagger
    /// <summary>
    /// Gets or sets the URL to the last page.
    /// </summary>
    /// <example>/cars?Last=10</example>
#endif
    public Uri LastPageUrl { get; set; } = default!;

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

        if (this.HasNextPage && this.NextPageUrl is not null)
        {
            values.Add(GetLinkValueItem(NextLinkItem, this.NextPageUrl));
        }

        if (this.HasPreviousPage && this.PreviousPageUrl is not null)
        {
            values.Add(GetLinkValueItem(PreviousLinkItem, this.PreviousPageUrl));
        }

        if (this.FirstPageUrl is not null)
        {
            values.Add(GetLinkValueItem(FirstLinkItem, this.FirstPageUrl));
        }

        if (this.LastPageUrl is not null)
        {
            values.Add(GetLinkValueItem(LastLinkItem, this.LastPageUrl));
        }

        return string.Join(", ", values);
    }

    private static string GetLinkValueItem(string rel, Uri url) =>
        FormattableString.Invariant($"<{url}>; rel=\"{rel}\"");
}
