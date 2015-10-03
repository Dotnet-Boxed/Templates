namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNet.Razor.Runtime.TagHelpers;

    /// <summary>
    /// This object represents a single book or publication. This is an appropriate type for ebooks, as well as traditional paperback or hardback books.
    /// This object type is not part of the Open Graph standard but is used by Facebook.
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/books.book/
    /// </summary>
    [TargetElement(
        "open-graph-books-book", 
        Attributes = TitleAttributeName + "," + MainImageAttributeName + "," + ISBNAttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphBooksBook : OpenGraphMetadata
    {
        #region Constants

        private const string AuthorUrlsAttributeName = "author-urls";
        private const string GenreUrlsAttributeName = "genre-urls";
        private const string InitialReleaseDateAttributeName = "initial-release-date";
        private const string ISBNAttributeName = "isbn";
        private const string LanguageAttributeName = "language";
        private const string PageCountAttributeName = "page-count";
        private const string RatingAttributeName = "rating";
        private const string ReleaseDateAttributeName = "release-date";
        private const string SampleUrlAttributeName = "sample-url";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphBooksBook" /> class.
        /// </summary>
        public OpenGraphBooksBook() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphBooksBook" /> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="mainImage">The main image which should represent your object within the graph. This is a required property.</param>
        /// <param name="isbn">The books unique ISBN number.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph. Leave as <c>null</c> to get the URL of the current page.</param>
        public OpenGraphBooksBook(string title, OpenGraphImage mainImage, string isbn, string url = null)
            : base(title, mainImage, url)
        {
            if (isbn == null) { throw new ArgumentNullException(nameof(isbn)); }

            this.ISBN = isbn;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the URL's to the pages about the authors of the book. This URL must contain books.author meta tags <see cref="OpenGraphBooksAuthor"/>.
        /// </summary>
        [HtmlAttributeName(AuthorUrlsAttributeName)]
        public IEnumerable<string> AuthorUrls { get; set; }

        /// <summary>
        /// Gets or sets the URL's to the pages about the genres of the book. This URL must contain books.genre meta tags <see cref="OpenGraphBooksGenre"/>.
        /// </summary>
        [HtmlAttributeName(GenreUrlsAttributeName)]
        public IEnumerable<string> GenreUrls { get; set; }

        /// <summary>
        /// Gets or sets the initial release date of the book.
        /// </summary>
        [HtmlAttributeName(InitialReleaseDateAttributeName)]
        public DateTime? InitialReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the books unique ISBN number.
        /// </summary>
        [HtmlAttributeName(ISBNAttributeName)]
        public string ISBN { get; set; }

        /// <summary>
        /// Gets or sets the language of the book in the format language_TERRITORY.
        /// </summary>
        [HtmlAttributeName(LanguageAttributeName)]
        public string Language { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "books: http://ogp.me/ns/books#"; } }

        /// <summary>
        /// Gets or sets the number of pages in the book.
        /// </summary>
        [HtmlAttributeName(PageCountAttributeName)]
        public int? PageCount { get; set; }

        /// <summary>
        /// Gets or sets the rating of the book.
        /// </summary>
        [HtmlAttributeName(RatingAttributeName)]
        public OpenGraphRating Rating { get; set; }

        /// <summary>
        /// Gets or sets the release date of the book.
        /// </summary>
        [HtmlAttributeName(ReleaseDateAttributeName)]
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the URL to a sample of the book.
        /// </summary>
        [HtmlAttributeName(SampleUrlAttributeName)]
        public string SampleUrl { get; set; }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.BooksBook; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaPropertyContentIfNotNull("books:author", this.AuthorUrls);
            stringBuilder.AppendMetaPropertyContentIfNotNull("books:genre", this.GenreUrls);
            stringBuilder.AppendMetaPropertyContentIfNotNull("books:initial_release_date", this.InitialReleaseDate);
            stringBuilder.AppendMetaPropertyContentIfNotNull("books:isbn", this.ISBN);
            stringBuilder.AppendMetaPropertyContentIfNotNull("books:language", this.Language);
            stringBuilder.AppendMetaPropertyContentIfNotNull("books:page_count", this.PageCount);

            if (this.Rating != null)
            {
                stringBuilder.AppendMetaPropertyContent("books:rating:value", this.Rating.Value);
                stringBuilder.AppendMetaPropertyContent("books:rating:scale", this.Rating.Scale);
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("books:release_date", this.ReleaseDate);
            stringBuilder.AppendMetaPropertyContentIfNotNull("books:sample", this.SampleUrl);
        }

        #endregion
    }
}
