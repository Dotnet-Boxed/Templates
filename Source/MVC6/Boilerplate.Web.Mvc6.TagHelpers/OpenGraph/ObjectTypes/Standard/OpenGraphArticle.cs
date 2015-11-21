namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// This object represents an article on a website. It is the preferred type for blog posts and news stories.
    /// This object type is part of the Open Graph standard.
    /// See http://ogp.me/
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/article/
    /// </summary>
    [HtmlTargetElement(
        "open-graph-article", 
        Attributes = TitleAttributeName + "," + MainImageAttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphArticle : OpenGraphMetadata
    {
        #region Constants

        private const string AuthorUrlsAttributeName = "author-urls";
        private const string ExpirationTimeAttributeName = "expiration-time";
        private const string ModifiedTimeAttributeName = "modified-time";
        private const string PublishedTimeAttributeName = "published-time";
        private const string PublisherUrlAttributeName = "publisher-url";
        private const string SectionAttributeName = "section";
        private const string TagsAttributeName = "tags";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the URL's to the pages about the author who wrote the article. This URL must contain profile meta tags <see cref="OpenGraphProfile"/>.
        /// </summary>
        [HtmlAttributeName(AuthorUrlsAttributeName)]
        public IEnumerable<string> AuthorUrls { get; set; }

        /// <summary>
        /// Gets or sets the expiration time, after which the article is out of date.
        /// </summary>
        [HtmlAttributeName(ExpirationTimeAttributeName)]
        public DateTime ExpirationTime { get; set; }

        /// <summary>
        /// Gets or sets the modified time, when the article was last changed.
        /// </summary>
        [HtmlAttributeName(ModifiedTimeAttributeName)]
        public DateTime ModifiedTime { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "article: http://ogp.me/ns/article#"; } }

        /// <summary>
        /// Gets or sets the published time, when the article was published.
        /// </summary>
        [HtmlAttributeName(PublishedTimeAttributeName)]
        public DateTime PublishedTime { get; set; }

        /// <summary>
        /// Gets or sets the URL to the page about the publisher of the article. This URL must contain profile the meta tag <see cref="OpenGraphProfile"/>.
        /// This particular property is not part of the Open Graph standard but is documented by Facebook.
        /// </summary>
        [HtmlAttributeName(PublisherUrlAttributeName)]
        public string PublisherUrl { get; set; }

        /// <summary>
        /// Gets or sets the high-level section or category name e.g. Technology.
        /// </summary>
        [HtmlAttributeName(SectionAttributeName)]
        public string Section { get; set; }

        /// <summary>
        /// Gets or sets the tag words associated with the article.
        /// </summary>
        [HtmlAttributeName(TagsAttributeName)]
        public IEnumerable<string> Tags { get; set; }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.Article; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaPropertyContentIfNotNull("article:author", this.AuthorUrls);
            stringBuilder.AppendMetaPropertyContentIfNotNull("article:expiration_time", this.ExpirationTime);
            stringBuilder.AppendMetaPropertyContentIfNotNull("article:modified_time", this.ModifiedTime);
            stringBuilder.AppendMetaPropertyContentIfNotNull("article:published_time", this.PublishedTime);
            stringBuilder.AppendMetaPropertyContentIfNotNull("article:publisher", this.PublisherUrl);
            stringBuilder.AppendMetaPropertyContentIfNotNull("article:section", this.Section);
            stringBuilder.AppendMetaPropertyContentIfNotNull("article:tag", this.Tags);
        }

        #endregion
    }
}
