namespace Boilerplate.Web.Mvc.Twitter
{
    using System;
    using System.Text;

    /// <summary>
    /// With Twitter Cards, you can attach rich photos, videos and media experience to Tweets that drive traffic to your website. 
    /// Users who Tweet links to your content will have a "Card" added to the Tweet that's visible to all of their followers.
    /// See https://dev.twitter.com/cards/overview.
    /// Sign up for Twitter Card analytics to see who is sharing your site pages on Twitter.
    /// See https://analytics.twitter.com/about
    /// Validate your Twitter cards.
    /// See https://twitter.com/login?redirect_after_login=https%3A%2F%2Fcards-dev.twitter.com%2Fvalidator
    /// </summary>
    public abstract class TwitterCard
    {
        private readonly string username;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterCard"/> class.
        /// </summary>
        /// <param name="username">The Twitter username associated with the page e.g. @RehanSaeedUK. This is a required property.</param>
        /// <exception cref="System.ArgumentNullException">username is <c>null</c>.</exception>
        public TwitterCard(string username)
        {
            if (username == null) { throw new ArgumentNullException("username"); }

            this.username = username;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the type of the Twitter card.
        /// </summary>
        public abstract TwitterCardType Type { get; }

        /// <summary>
        /// Gets the Twitter username associated with the page e.g. @RehanSaeedUK. This is a required property.
        /// </summary>
        public string Username { get { return this.username; } }

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Returns a HTML-encoded <see cref="System.String" /> that represents this instance containing the Twitter card meta tags.
        /// </summary>
        /// <returns>
        /// A HTML-encoded <see cref="System.String" /> that represents this instance containing the Twitter card meta tags.
        /// </returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            this.ToString(stringBuilder);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Twitter card meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public virtual void ToString(StringBuilder stringBuilder)
        {
            if (this.Type != TwitterCardType.Summary)
            {
                stringBuilder.AppendMetaNameContent("twitter:card", this.Type.ToTwitterString());
            }

            stringBuilder.AppendMetaNameContent("twitter:site", this.Username);
        }

        #endregion
    }
}
