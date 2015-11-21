namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// This object type represents the genre of a book or publication. This object type is not part of the Open Graph standard but is used by Facebook.
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/books.genre/
    /// </summary>
    [HtmlTargetElement(
        "open-graph-books-genre", 
        Attributes = TitleAttributeName + "," + MainImageAttributeName + "," + CanonicalNameAttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphBooksGenre : OpenGraphMetadata
    {
        #region Constants

        private const string AuthorUrlsAttributeName = "author-urls";
        private const string BookUrlsAttributeName = "book-urls";
        private const string CanonicalNameAttributeName = "canonical-name";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the URL's to the pages about the authors who wrote the books. This URL must contain books.book meta tags <see cref="OpenGraphBooksAuthor"/>.
        /// </summary>
        [HtmlAttributeName(AuthorUrlsAttributeName)]
        public IEnumerable<string> AuthorUrls { get; set; }

        /// <summary>
        /// Gets or sets the URL's to the pages about the books written by the author. This URL must contain books.book meta tags <see cref="OpenGraphBooksBook"/>.
        /// </summary>
        [HtmlAttributeName(BookUrlsAttributeName)]
        public IEnumerable<string> BookUrls { get; set; }

        /// <summary>
        /// Gets or sets the canonical name of the genre. Only one of the following names is allowed Adventure, Children's Fiction, Drama, Erotica, Essays, Fantasy, Gay &amp; Lesbian, Graphic Novels, Historical Fiction, Horror, Fiction &amp; Literature, Mystery, Mythology &amp; Folklore, Poetry, Religious &amp; Inspiratonal, Rhetoric &amp; Critcism, Romance, Satire &amp; Humor, Science Fiction, Thrillers &amp; Suspense, Westerns, Women&#039;s Fiction, Young Adult Fiction, Biography &amp; Memoir, Current Affairs &amp; Politics, Genealogy, Geography, History, History of the Ancient World, History of Africa, History of Asia, History of Europe, History of North America, History of South America, Travel, Bibliographies, Children&#039;s Non-fiction, Computer Science, Encyclopedias, General Collections, Gift Books, Information Sciences, Journalism &amp; Publishing, Magazines &amp; Journals, Manuscripts &amp; Rare Books, Epistemology, Ethics, Logic, Metaphysics, Philosophy, Parapsychology &amp; Occultism, Psychology, Self-help, Bible, Comparative Religions, Natural Theory, Theology, Business, Customs &amp; Etiquette, Economics, Education, Finance, Gay &amp; Lesbian Non-Fiction, Gender Studies, Law, Political Science, Social Sciences, Social Services, Statistics, True Crime, English &amp; Old English, French, German, Greek, Italian, Language, Latin, Linguistics, Other Languages, Portugese, Spanish, Astronomy, Chemistry, Earth Sciences, Life Sciences, Mathematics, Paleontology &amp; Paleozoology, Physics, Plants Sciences, Zoology, Agriculture, Chemical Engineering, Engineering &amp; Opera?ons, Management, Manufacturing, Medical Sciences, Technology, Cooking &amp; Cookbooks, Gardening, Home Decorating, Home Economics, Parenting, Pets, Architecture, Design, Drawing, Fine Art, Gardening, Graphic Art, Music, Painting, Performing Arts, Photography, Sculpture, Games, Fitness, Health and Sports.
        /// </summary>
        [HtmlAttributeName(CanonicalNameAttributeName)]
        public string CanonicalName { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "books: http://ogp.me/ns/books#"; } }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.BooksGenre; } }

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
            stringBuilder.AppendMetaPropertyContentIfNotNull("books:book", this.BookUrls);
            stringBuilder.AppendMetaPropertyContentIfNotNull("books:canonical_name", this.CanonicalName);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Checks that this instance is valid and throws exceptions if not valid.
        /// </summary>
        protected override void Validate()
        {
            base.Validate();

            if (this.CanonicalName == null) { throw new ArgumentNullException(nameof(this.CanonicalName)); }
        }

        #endregion
    }
}
