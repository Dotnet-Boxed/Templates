namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNet.Razor.Runtime.TagHelpers;

    /// <summary>
    /// This object type represents the genre of a book or publication. This object type is not part of the Open Graph standard but is used by Facebook.
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/books.genre/
    /// </summary>
    [TargetElement(Attributes = nameof(Title) + "," + nameof(MainImage) + "," + nameof(CanonicalName), TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphBooksGenre : OpenGraphMetadata
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphBooksGenre"/> class.
        /// </summary>
        public OpenGraphBooksGenre() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphBooksGenre"/> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="mainImage">The main image which should represent your object within the graph. This is a required property.</param>
        /// <param name="canonicalName">THe genres canonical name. Only one of the following names is allowed Adventure, Children's Fiction, Drama, Erotica, Essays, Fantasy, Gay &amp; Lesbian, Graphic Novels, Historical Fiction, Horror, Fiction &amp; Literature, Mystery, Mythology &amp; Folklore, Poetry, Religious &amp; Inspiratonal, Rhetoric &amp; Critcism, Romance, Satire &amp; Humor, Science Fiction, Thrillers &amp; Suspense, Westerns, Women&#039;s Fiction, Young Adult Fiction, Biography &amp; Memoir, Current Affairs &amp; Politics, Genealogy, Geography, History, History of the Ancient World, History of Africa, History of Asia, History of Europe, History of North America, History of South America, Travel, Bibliographies, Children&#039;s Non-fiction, Computer Science, Encyclopedias, General Collections, Gift Books, Information Sciences, Journalism &amp; Publishing, Magazines &amp; Journals, Manuscripts &amp; Rare Books, Epistemology, Ethics, Logic, Metaphysics, Philosophy, Parapsychology &amp; Occultism, Psychology, Self-help, Bible, Comparative Religions, Natural Theory, Theology, Business, Customs &amp; Etiquette, Economics, Education, Finance, Gay &amp; Lesbian Non-Fiction, Gender Studies, Law, Political Science, Social Sciences, Social Services, Statistics, True Crime, English &amp; Old English, French, German, Greek, Italian, Language, Latin, Linguistics, Other Languages, Portugese, Spanish, Astronomy, Chemistry, Earth Sciences, Life Sciences, Mathematics, Paleontology &amp; Paleozoology, Physics, Plants Sciences, Zoology, Agriculture, Chemical Engineering, Engineering &amp; Opera?ons, Management, Manufacturing, Medical Sciences, Technology, Cooking &amp; Cookbooks, Gardening, Home Decorating, Home Economics, Parenting, Pets, Architecture, Design, Drawing, Fine Art, Gardening, Graphic Art, Music, Painting, Performing Arts, Photography, Sculpture, Games, Fitness, Health and Sports.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph. Leave as <c>null</c> to get the URL of the current page.</param>
        public OpenGraphBooksGenre(string title, OpenGraphImage mainImage, string canonicalName, string url = null)
            : base(title, mainImage, url)
        {
            this.CanonicalName = canonicalName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the URL's to the pages about the authors who wrote the books. This URL must contain books.book meta tags <see cref="OpenGraphBooksAuthor"/>.
        /// </summary>
        public IEnumerable<string> AuthorUrls { get; set; }

        /// <summary>
        /// Gets or sets the URL's to the pages about the books written by the author. This URL must contain books.book meta tags <see cref="OpenGraphBooksBook"/>.
        /// </summary>
        public IEnumerable<string> BookUrls { get; set; }

        /// <summary>
        /// Gets or sets the canonical name of the genre. Only one of the following names is allowed Adventure, Children's Fiction, Drama, Erotica, Essays, Fantasy, Gay &amp; Lesbian, Graphic Novels, Historical Fiction, Horror, Fiction &amp; Literature, Mystery, Mythology &amp; Folklore, Poetry, Religious &amp; Inspiratonal, Rhetoric &amp; Critcism, Romance, Satire &amp; Humor, Science Fiction, Thrillers &amp; Suspense, Westerns, Women&#039;s Fiction, Young Adult Fiction, Biography &amp; Memoir, Current Affairs &amp; Politics, Genealogy, Geography, History, History of the Ancient World, History of Africa, History of Asia, History of Europe, History of North America, History of South America, Travel, Bibliographies, Children&#039;s Non-fiction, Computer Science, Encyclopedias, General Collections, Gift Books, Information Sciences, Journalism &amp; Publishing, Magazines &amp; Journals, Manuscripts &amp; Rare Books, Epistemology, Ethics, Logic, Metaphysics, Philosophy, Parapsychology &amp; Occultism, Psychology, Self-help, Bible, Comparative Religions, Natural Theory, Theology, Business, Customs &amp; Etiquette, Economics, Education, Finance, Gay &amp; Lesbian Non-Fiction, Gender Studies, Law, Political Science, Social Sciences, Social Services, Statistics, True Crime, English &amp; Old English, French, German, Greek, Italian, Language, Latin, Linguistics, Other Languages, Portugese, Spanish, Astronomy, Chemistry, Earth Sciences, Life Sciences, Mathematics, Paleontology &amp; Paleozoology, Physics, Plants Sciences, Zoology, Agriculture, Chemical Engineering, Engineering &amp; Opera?ons, Management, Manufacturing, Medical Sciences, Technology, Cooking &amp; Cookbooks, Gardening, Home Decorating, Home Economics, Parenting, Pets, Architecture, Design, Drawing, Fine Art, Gardening, Graphic Art, Music, Painting, Performing Arts, Photography, Sculpture, Games, Fitness, Health and Sports.
        /// </summary>
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
    }
}
