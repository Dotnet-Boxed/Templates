namespace Boilerplate.Web.Mvc
{
    using System;

    [Serializable]
    public class CspViolationException : Exception
    {
        private const string MessagePrefix = "Content Security Policy(CSP) was violated. Either adjust your policy to allow the use of the specified resource or stop using the resource. You can also consider setting CSP to report-only mode in which errors are logged but nothing is blocked by the browser. ";

        #region Constructors

        public CspViolationException(string message)
            : base(MessagePrefix + message)
        {
        }

        public CspViolationException(string message, Exception inner)
            : base(MessagePrefix + message, inner)
        {
        }

        protected CspViolationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}