namespace Boilerplate.Web.Mvc.Sitemap
{
    using System;

    [Serializable]
    public class SitemapException : Exception
    {
        #region Constructors

        public SitemapException(string message)
            : base(message)
        {
        }

        public SitemapException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected SitemapException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}