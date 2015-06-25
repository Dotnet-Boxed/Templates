namespace Boilerplate.Web.Mvc.Sitemap
{
    using System;
#if DNX451
    using System.Runtime.Serialization;
#endif

    /// <summary>
    /// Represents errors that occur during sitemap creation.
    /// </summary>
#if DNX451
    [Serializable]
#endif
    public class SitemapException : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SitemapException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public SitemapException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SitemapException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public SitemapException(string message, Exception inner)
            : base(message, inner)
        {
        }

#if DNX451
        /// <summary>
        /// Initializes a new instance of the <see cref="SitemapException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the 
        /// serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains 
        /// contextual information about the source or destination.</param>
        protected SitemapException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
#endif

        #endregion
    }
}