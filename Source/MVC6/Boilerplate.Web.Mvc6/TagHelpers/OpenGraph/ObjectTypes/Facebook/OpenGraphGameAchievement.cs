namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// This object type represents a specific achievement in a game. An app must be in the 'Games' category in App Dashboard to be able to use this 
    /// object type. Every achievement has a game:points value associate with it. This is not related to the points the user has scored in the game, but 
    /// is a way for the app to indicate the relative importance and scarcity of different achievements: * Each game gets a total of 1,000 points to 
    /// distribute across its achievements * Each game gets a maximum of 1,000 achievements * Achievements which are scarcer and have higher point 
    /// values will receive more distribution in Facebook's social channels. For example, achievements which have point values of less than 10 will get 
    /// almost no distribution. Apps should aim for between 50-100 achievements consisting of a mix of 50 (difficult), 25 (medium), and 10 (easy) point 
    /// value achievements Read more on how to use achievements in this guide. This object type is not part of the Open Graph standard but is used by 
    /// Facebook.
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/game.achievement/
    /// </summary>
    [HtmlTargetElement(
        "open-graph-game-achievement", 
        Attributes = TitleAttributeName + "," + MainImageAttributeName + "," + PointsAttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphGameAchievement : OpenGraphMetadata
    {
        #region Constants

        private const string IsSecretAttributeName = "secret";
        private const string PointsAttributeName = "points";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this achievement is secret and should be hidden from display on Facebook.
        /// </summary>
        [HtmlAttributeName(IsSecretAttributeName)]
        public bool? IsSecret { get; set; }

        /// <summary>
        /// Gets or sets the relative importance and scarcity of the achievement.
        /// </summary>
        [HtmlAttributeName(PointsAttributeName)]
        public int Points { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "game: http://ogp.me/ns/game#"; } }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.GameAchievement; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaPropertyContent("game:points", this.Points);
            stringBuilder.AppendMetaPropertyContentIfNotNull("game:secret", this.IsSecret);
        }

        #endregion
    }
}
