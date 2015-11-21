namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.AspNet.Mvc.Rendering;
    using Microsoft.AspNet.Mvc.ViewFeatures;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// The Open Graph protocol enables any web page to become a rich object in a social graph. 
    /// For instance, this is used on Facebook to allow any web page to have the same functionality 
    /// as any other object on Facebook.
    /// See http://ogp.me for the official Open Graph specification documentation.
    /// See https://developers.facebook.com/docs/sharing/opengraph for Facebook Open Graph documentation.
    /// See https://www.facebook.com/login.php?next=https%3A%2F%2Fdevelopers.facebook.com%2Ftools%2Fdebug%2F for the Open Graph debugging tool to test and verify your Open Graph implementation.
    /// </summary>
    public abstract class OpenGraphMetadata : TagHelper
    {
        #region Constants

        private const string OgNamespace = "og: http://ogp.me/ns# ";
        private const string FacebookNamespace = "fb: http://ogp.me/ns/fb# ";

        protected const string AlternateLocalesAttributeName = "alternate-locales";
        protected const string DescriptionAttributeName = "description";
        protected const string DeterminerAttributeName = "determiner";
        protected const string FacebookAdministratorsAttributeName = "facebook-administrators";
        protected const string FacebookApplicationIdAttributeName = "facebook-application-id";
        protected const string FacebookProfileIdAttributeName = "facebook-profile-id";
        protected const string LocaleAttributeName = "locale";
        protected const string MainImageAttributeName = "main-image";
        protected const string MediaAttributeName = "media";
        protected const string SeeAlsoAttributeName = "see-also";
        protected const string SiteNameAttributeName = "site-name";
        protected const string TitleAttributeName = "title";
        protected const string UrlAttributeName = "url";

        #endregion
        
        #region Public Properties

        /// <summary>
        /// Gets or sets the collection of alternate locales this page is available in.
        /// </summary>
        [HtmlAttributeName(AlternateLocalesAttributeName)]
        public IEnumerable<string> AlternateLocales { get; set; }

        /// <summary>
        /// Gets the audio files which should represent your object within the graph. Use the Media property to add an audio file.
        /// </summary>
        public IEnumerable<OpenGraphAudio> Audio
        {
            get { return this.Media == null ? null : this.Media.OfType<OpenGraphAudio>(); }
        }

        /// <summary>
        /// Gets or sets a one to two sentence description of your object. Only set this value if it is different from the <![CDATA[<meta name="description" content="blah blah">]]> meta tag. Right now Facebook only displays the first 300 characters of a description.
        /// </summary>
        [HtmlAttributeName(DescriptionAttributeName)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the word that appears before this object's title in a sentence. An enum of (a, an, the, "", auto). 
        /// If auto is chosen, the consumer of your data should chose between "a" or "an". Default is "" (blank).
        /// </summary>
        [HtmlAttributeName(DeterminerAttributeName)]
        public OpenGraphDeterminer Determiner { get; set; }

        /// <summary>
        /// Gets or sets the list of Facebook ID's of the administrators. 
        /// Use this or <see cref="FacebookAdministrators"/>, not both. <see cref="FacebookAdministrators"/> is the preferred method.
        /// </summary>
        [HtmlAttributeName(FacebookAdministratorsAttributeName)]
        public IEnumerable<string> FacebookAdministrators { get; set; }

        /// <summary>
        /// Gets or sets the Facebook application identifier that identifies your website to Facebook. 
        /// Use this or <see cref="FacebookAdministrators"/>, not both. Go to https://developers.facebook.com/ to
        /// create a developer account, go to the apps tab to create a new app, which will give you this ID.
        /// </summary>
        [HtmlAttributeName(FacebookApplicationIdAttributeName)]
        public string FacebookApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the Facebook profile for the current object.
        /// </summary>
        [HtmlAttributeName(FacebookProfileIdAttributeName)]
        public string FacebookProfileId { get; set; }

        /// <summary>
        /// Gets the images which should represent your object within the graph. Use the Media property to add an image.
        /// </summary>
        public IEnumerable<OpenGraphImage> Images
        {
            get { return this.Media == null ? null : this.Media.OfType<OpenGraphImage>(); }
        }

        /// <summary>
        /// Gets or sets the locale these tags are marked up in. Of the format language_TERRITORY. Default is en_US.
        /// </summary>
        [HtmlAttributeName(LocaleAttributeName)]
        public string Locale { get; set; }

        /// <summary>
        /// Gets or sets the main image which should represent your object within the graph. This is a required property.
        /// </summary>
        [HtmlAttributeName(MainImageAttributeName)]
        public OpenGraphImage MainImage { get; set; }

        /// <summary>
        /// Gets or sets the images, videos or audio which should represent your object within the graph.
        /// </summary>
        [HtmlAttributeName(MediaAttributeName)]
        public ICollection<OpenGraphMedia> Media { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public abstract string Namespace { get; }

        /// <summary>
        /// Gets or sets the list of URL's used to supply an additional link that shows related content to the object. This property is not part of the 
        /// Open Graph standard but is used by Facebook.
        /// </summary>
        [HtmlAttributeName(SeeAlsoAttributeName)]
        public IEnumerable<string> SeeAlso { get; set; }

        /// <summary>
        /// Gets or sets the name of the site. if your object is part of a larger web site, the name which should be displayed 
        /// for the overall site. e.g. "IMDb".
        /// </summary>
        [HtmlAttributeName(SiteNameAttributeName)]
        public string SiteName { get; set; }

        /// <summary>
        /// Gets or sets the title of your object as it should appear within the graph, e.g. "The Rock".
        /// </summary>
        [HtmlAttributeName(TitleAttributeName)]
        public string Title { get; set; }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public abstract OpenGraphType Type { get; }

        /// <summary>
        /// Gets or sets the canonical URL of your object that will be used as its permanent ID in the graph, 
        /// e.g. "http://www.imdb.com/title/tt0117500/". Leave as <c>null</c> to get the URL of the current page.
        /// </summary>
        [HtmlAttributeName(UrlAttributeName)]
        public string Url { get; set; }

        /// <summary>
        /// Gets the videos which should represent your object within the graph. Use the Media property to add a video file.
        /// </summary>
        public IEnumerable<OpenGraphVideo> Videos
        {
            get { return this.Media == null ? null : this.Media.OfType<OpenGraphVideo>(); }
        }

        // Workaround for context.Items not working across _Layout.cshtml and Index.cshtml using ViewContext.
        // https://github.com/aspnet/Mvc/issues/3233 and https://github.com/aspnet/Razor/issues/564
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the space delimited namespaces to be added to the prefix attribute of the head element in the HTML 
        /// document. It contains the namespaces for the Open Graph object type used on the page, as well as the 
        /// Facebook namespaces if Facebook Administrators, Application ID's or Profile ID's are supplied.
        /// </summary>
        /// <returns>A <see cref="string"/> containing Open Graph namespaces.</returns>
        public string GetNamespaces()
        {
            string namespaces;

            if ((this.FacebookAdministrators == null) &&
                (this.FacebookApplicationId == null) &&
                (this.FacebookProfileId == null))
            {
                namespaces = OgNamespace + this.Namespace;
            }
            else
            {
                namespaces = OgNamespace + FacebookNamespace + this.Namespace;
            }

            return namespaces;
        }

        /// <summary>
        /// Synchronously executes the <see cref="TagHelper"/> with the given <paramref name="context"/> and
        /// <paramref name="output"/>.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Workaround for context.Items not working across _Layout.cshtml and Index.cshtml using ViewContext.
            // https://github.com/aspnet/Mvc/issues/3233 and https://github.com/aspnet/Razor/issues/564
            this.ViewContext.ViewData[nameof(OpenGraphPrefixTagHelper)] = this.GetNamespaces();
            
            // context.Items[typeof(OpenGraphMetadata)] = this.GetNamespaces();

            output.Content.SetHtmlContent(this.ToString());
            output.TagName = null;
        }

        /// <summary>
        /// Returns a HTML-encoded <see cref="System.String" /> that represents this instance containing the Open Graph meta tags.
        /// </summary>
        /// <returns>
        /// A HTML-encoded <see cref="System.String" /> that represents this instance containing the Open Graph meta tags.
        /// </returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            this.ToString(stringBuilder);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public virtual void ToString(StringBuilder stringBuilder)
        {
            this.Validate();

            // Three required tags.
            stringBuilder.AppendMetaPropertyContent("og:title", this.Title);
            if (this.Type != OpenGraphType.Website)
            {
                // The type property is also required but if absent the page will be treated as type website.
                stringBuilder.AppendMetaPropertyContent("og:type", this.Type.ToLowercaseString());
            }

            if (this.Url == null)
            {
                this.Url = Context.HttpContext.Request.Path.ToString();
            }

            stringBuilder.AppendMetaPropertyContent("og:url", this.Url);

            // Add image, video and audio tags.
            this.MainImage.ToString(stringBuilder);
            if (this.Media != null)
            {
                foreach (OpenGraphMedia media in this.Media)
                {
                    media.ToString(stringBuilder);
                }
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("og:description", this.Description);
            stringBuilder.AppendMetaPropertyContentIfNotNull("og:site_name", this.SiteName);

            if (this.Determiner != OpenGraphDeterminer.Blank)
            {
                stringBuilder.AppendMetaPropertyContent("og:determiner", this.Determiner.ToLowercaseString());
            }

            if (this.Locale != null)
            {
                stringBuilder.AppendMetaPropertyContent("og:locale", this.Locale);

                if (this.AlternateLocales != null)
                {
                    foreach (string locale in this.AlternateLocales)
                    {
                        stringBuilder.AppendMetaPropertyContent("og:locale:alternate", locale);
                    }
                }
            }

            if (this.SeeAlso != null)
            {
                foreach (string seeAlso in this.SeeAlso)
                {
                    stringBuilder.AppendMetaPropertyContent("og:see_also", seeAlso);
                }
            }

            if (this.FacebookAdministrators != null)
            {
                foreach (string facebookAdministrator in this.FacebookAdministrators)
                {
                    stringBuilder.AppendMetaPropertyContentIfNotNull("fb:admins", facebookAdministrator);
                }
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("fb:app_id", this.FacebookApplicationId);
            stringBuilder.AppendMetaPropertyContentIfNotNull("fb:profile_id", this.FacebookProfileId);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Checks that this instance is valid and throws exceptions if not valid.
        /// </summary>
        protected virtual void Validate()
        {
            if (this.Title == null) { throw new ArgumentNullException(nameof(this.Title)); }
            if (this.MainImage == null) { throw new ArgumentNullException(nameof(this.MainImage)); }
        }

        #endregion
    }
}
