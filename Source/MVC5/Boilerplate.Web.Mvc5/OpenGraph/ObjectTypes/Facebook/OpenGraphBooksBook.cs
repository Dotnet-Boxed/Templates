namespace Boilerplate.Web.Mvc.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// This object represents a single book or publication. This is an appropriate type for ebooks, as well as traditional paperback or hardback books.
    /// This object type is not part of the Open Graph standard but is used by Facebook.
    /// </summary>
    public class OpenGraphBooksBook : OpenGraphMetadata
    {
        private readonly string isbn;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphBooksBook"/> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        /// <param name="isbn">The books unique ISBN number.</param>
        public OpenGraphBooksBook(string title, OpenGraphImage image, string isbn)
            : base(title, image)
        {
            this.isbn = isbn;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphBooksBook" /> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        /// <param name="isbn">The books unique ISBN number.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph.</param>
        public OpenGraphBooksBook(string title, OpenGraphImage image, string isbn, string url)
            : base(title, image, url)
        {
            this.isbn = isbn;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the URL's to the pages about the authors of the book. This URL must contain books.author meta tags <see cref="OpenGraphBooksAuthor"/>.
        /// </summary>
        public IEnumerable<string> AuthorUrls { get; set; }

        /// <summary>
        /// Gets or sets the URL's to the pages about the genres of the book. This URL must contain books.genre meta tags <see cref="OpenGraphBooksGenre"/>.
        /// </summary>
        public IEnumerable<string> GenreUrls { get; set; }

        /// <summary>
        /// Gets or sets the initial release date of the book.
        /// </summary>
        public DateTime? InitialReleaseDate { get; set; }

        /// <summary>
        /// Gets the books unique ISBN number.
        /// </summary>
        public string ISBN { get { return this.isbn; } }

        /// <summary>
        /// Gets or sets the language of the book in the format language_TERRITORY.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "books: http://ogp.me/ns/books#"; } }

        /// <summary>
        /// Gets or sets the number of pages in the book.
        /// </summary>
        public int? PageCount { get; set; }

        /// <summary>
        /// Gets or sets the rating of the book.
        /// </summary>
        public OpenGraphRating Rating { get; set; }

        /// <summary>
        /// Gets or sets the release date of the book.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the URL to a sample of the book.
        /// </summary>
        public string SampleUrl { get; set; }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.BooksBook; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <see cref="stringBuilder" /> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaIfNotNull("books:author", this.AuthorUrls);
            stringBuilder.AppendMetaIfNotNull("books:genre", this.GenreUrls);
            stringBuilder.AppendMetaIfNotNull("books:initial_release_date", this.InitialReleaseDate);
            stringBuilder.AppendMetaIfNotNull("books:isbn", this.ISBN);
            stringBuilder.AppendMetaIfNotNull("books:language", this.Language);
            stringBuilder.AppendMetaIfNotNull("books:page_count", this.PageCount);

            if (this.Rating != null)
            {
                stringBuilder.AppendMeta("books:rating:value", this.Rating.Value);
                stringBuilder.AppendMeta("books:rating:scale", this.Rating.Scale);
            }

            stringBuilder.AppendMetaIfNotNull("books:release_date", this.ReleaseDate);
            stringBuilder.AppendMetaIfNotNull("books:sample", this.SampleUrl);
        }

        #endregion
    }
}
