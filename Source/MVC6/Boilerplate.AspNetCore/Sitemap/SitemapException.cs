namespace Boilerplate.AspNetCore.Sitemap
{
    using System;

    /// <summary>
    /// Represents errors that occur during sitemap creation.
    /// </summary>
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

        #endregion
    }
}