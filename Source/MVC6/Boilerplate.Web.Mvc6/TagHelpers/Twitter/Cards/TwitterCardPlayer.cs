namespace Boilerplate.Web.Mvc.TagHelpers.Twitter
{
    using System;
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// Video clips and audio streams have a special place on the Twitter platform thanks to the Player Card. By implementing a few HTML meta tags to 
    /// your website and following the Twitter Rules of the Road, you can deliver your rich media to users across the globe.
    /// Twitter must approve the use of the player card, find out more below.
    /// See https://dev.twitter.com/cards/types/player
    /// </summary>
    [HtmlTargetElement(
        "twitter-card-player", 
        Attributes = UsernameAttributeName + "," + ImageAttributeName + "," + PlayerAttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class TwitterCardPlayer : TwitterCard
    {
        #region Constants

        private const string DescriptionAttributeName = "description";
        private const string ImageAttributeName = "image";
        private const string PlayerAttributeName = "player";
        private const string TitleAttributeName = "title";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the description that concisely summarizes the content of the page, as appropriate for 
        /// presentation within a Tweet. Do not re-use the title text as the description, or use this field 
        /// to describe the general services provided by the website. Description text will be truncated at 
        /// the word to 200 characters. If you are using Facebook's Open Graph og:description, do not use this
        /// unless you want a different description.
        /// </summary>
        [HtmlAttributeName(DescriptionAttributeName)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the image to be displayed in place of the player on platforms that don’t support iframes or 
        /// inline players. You should make this image the same dimensions as your player. Images with fewer than 
        /// 68,600 pixels (a 262x262 square image, or a 350x196 16:9 image) will cause the player card not to render. 
        /// Image must be less than 1MB in size.
        /// </summary>
        [HtmlAttributeName(ImageAttributeName)]
        public TwitterImage Image { get; set; }

        /// <summary>
        /// Gets or sets the video player. If the iframe is wider than 435px, the iframe player will be resized to fit 
        /// a max width of 435px, maintaining the original aspect ratio.
        /// </summary>
        [HtmlAttributeName(PlayerAttributeName)]
        public TwitterPlayer Player { get; set; }

        /// <summary>
        /// Gets or sets the title of the summary. Title should be concise and will be truncated at 70 characters.
        /// If you are using Facebook's Open Graph og:title, do not use this unless you want a different title.
        /// </summary>
        [HtmlAttributeName(TitleAttributeName)]
        public string Title { get; set; }

        /// <summary>
        /// Gets the type of the Twitter card.
        /// </summary>
        public override TwitterCardType Type { get { return TwitterCardType.Player; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Twitter card meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaNameContentIfNotNull("twitter:title", this.Title);
            stringBuilder.AppendMetaNameContentIfNotNull("twitter:description", this.Description);
            stringBuilder.AppendMetaNameContent("twitter:image", this.Image.ImageUrl);
            stringBuilder.AppendMetaNameContentIfNotNull("twitter:image:height", this.Image.Height);
            stringBuilder.AppendMetaNameContentIfNotNull("twitter:image:width", this.Image.Width);
            stringBuilder.AppendMetaNameContent("twitter:player", this.Player.PlayerUrl);
            stringBuilder.AppendMetaNameContent("twitter:player:width", this.Player.Width);
            stringBuilder.AppendMetaNameContent("twitter:player:height", this.Player.Height);
            stringBuilder.AppendMetaNameContentIfNotNull("twitter:player:stream", this.Player.StreamUrl);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Checks that this instance is valid and throws exceptions if not valid.
        /// </summary>
        protected override void Validate()
        {
            base.Validate();

            if (this.Image == null) { throw new ArgumentNullException(nameof(this.Image)); }
            if (this.Player == null) { throw new ArgumentNullException(nameof(this.Player)); }
        }

        #endregion
    }
}