namespace Boilerplate.Web.Mvc
{
    using System;
#if NET451
    using System.Runtime.Serialization;
#endif

    /// <summary>
    /// Represents a Content Security Policy (CSP) violation.
    /// </summary>
#if NET451
    [Serializable]
#endif
    public class CspViolationException : Exception
    {
        private const string MessagePrefix = "Content Security Policy(CSP) was violated. Either adjust your policy to allow the use of the specified resource or stop using the resource. You can also consider setting CSP to report-only mode in which errors are logged but nothing is blocked by the browser. ";

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CspViolationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CspViolationException(string message)
            : base(MessagePrefix + message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CspViolationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public CspViolationException(string message, Exception inner)
            : base(MessagePrefix + message, inner)
        {
        }

#if NET451
        /// <summary>
        /// Initializes a new instance of the <see cref="CspViolationException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the 
        /// serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains 
        /// contextual information about the source or destination.</param>
        protected CspViolationException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
#endif

        #endregion
    }
}