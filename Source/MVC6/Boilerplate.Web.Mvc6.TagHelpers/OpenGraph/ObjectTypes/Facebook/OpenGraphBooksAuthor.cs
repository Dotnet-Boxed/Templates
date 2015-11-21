namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// This object represents a single author of a book. This object type is not part of the Open Graph standard but is used by Facebook.
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/books.author/
    /// </summary>
    [HtmlTargetElement(
        "open-graph-books-author", 
        Attributes = TitleAttributeName + "," + MainImageAttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphBooksAuthor : OpenGraphMetadata
    {
        #region Constants

        private const string BookUrlsAttributeName = "book-urls";
        private const string GenderAttributeName = "gender";
        private const string GenreUrlsAttributeName = "gender-urls";
        private const string OfficialSiteUrlAttributeName = "official-site-url";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the URL's to the pages about the books written by the author. This URL must contain books.book meta tags <see cref="OpenGraphBooksBook"/>.
        /// </summary>
        [HtmlAttributeName(BookUrlsAttributeName)]
        public IEnumerable<string> BookUrls { get; set; }

        /// <summary>
        /// Gets or sets the authors gender.
        /// </summary>
        [HtmlAttributeName(GenderAttributeName)]
        public OpenGraphGender? Gender { get; set; }

        /// <summary>
        /// Gets or sets the URL's to the pages about the genres of books the author typically writes. This URL must contain books.genre meta tags <see cref="OpenGraphBooksGenre"/>.
        /// </summary>
        [HtmlAttributeName(GenreUrlsAttributeName)]
        public IEnumerable<string> GenreUrls { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "books: http://ogp.me/ns/books#"; } }

        /// <summary>
        /// Gets or sets the official site URL of the author.
        /// </summary>
        [HtmlAttributeName(OfficialSiteUrlAttributeName)]
        public string OfficialSiteUrl { get; set; }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.BooksAuthor; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaPropertyContentIfNotNull("books:book", this.BookUrls);
            stringBuilder.AppendMetaPropertyContentIfNotNull("books:genre", this.GenreUrls);

            if (this.Gender.HasValue)
            {
                stringBuilder.AppendMetaPropertyContent("books:gender", this.Gender.Value.ToLowercaseString());
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("books:official_site", this.OfficialSiteUrl);
        }

        #endregion
    }
}
