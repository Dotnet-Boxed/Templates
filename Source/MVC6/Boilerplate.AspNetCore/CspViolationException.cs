namespace Boilerplate.AspNetCore
{
    using System;

    /// <summary>
    /// Represents a Content Security Policy (CSP) violation.
    /// </summary>
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

        #endregion
    }
}