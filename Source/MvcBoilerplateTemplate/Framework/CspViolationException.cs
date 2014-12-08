namespace $safeprojectname$.Framework
{
    using System;
    using NWebsec.Csp;

    [Serializable]
    public class CspViolationException : Exception
    {
        #region Constructors

        public CspViolationException(CspViolationReport cspViolationReport)
            : this(GetCspViolationReportString(cspViolationReport))
        {
        }

        public CspViolationException(CspViolationReport cspViolationReport, Exception inner)
            : this(GetCspViolationReportString(cspViolationReport), inner)
        {
        }

        public CspViolationException(string message)
            : base(message)
        {
        }

        public CspViolationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected CspViolationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region Private Static Methods

        private static string GetCspViolationReportString(CspViolationReport cspViolationReport)
        {
            cspViolationReport.ToString();
            return string.Format(
                "Content Security Policy (CSP) was violated. Either adjust your policy to allow the use of the specified resource or stop using the resource.\r\nUserAgent:<{0}>\r\nBlockedUri:<{1}>\r\nColumnNumber:<{2}>\r\nDocumentUri:<{3}>\r\nEffectiveDirective:<{4}>\r\nLineNumber:<{5}>\r\nOriginalPolicy:<{6}>\r\nReferrer:<{7}>\r\nScriptSample:<{8}>\r\nSourceFile:<{9}>\r\nStatusCode:<{10}>\r\nViolatedDirective:<{11}>",
                cspViolationReport.UserAgent,
                cspViolationReport.Details.BlockedUri,
                cspViolationReport.Details.ColumnNumber,
                cspViolationReport.Details.DocumentUri,
                cspViolationReport.Details.EffectiveDirective,
                cspViolationReport.Details.LineNumber,
                cspViolationReport.Details.OriginalPolicy,
                cspViolationReport.Details.Referrer,
                cspViolationReport.Details.ScriptSample,
                cspViolationReport.Details.SourceFile,
                cspViolationReport.Details.StatusCode,
                cspViolationReport.Details.ViolatedDirective);
        }

        #endregion
    }
}