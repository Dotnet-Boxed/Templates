namespace Boxed.Templates.FunctionalTest
{
    using System;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    [Serializable]
    public class ProcessStartException : Exception
    {
        public ProcessStartException()
        {
        }

        public ProcessStartException(ProcessStartInfo processStartInfo)
            : base(GetMessage(processStartInfo))
        {
        }

        public ProcessStartException(ProcessStartInfo processStartInfo, Exception inner)
            : base(GetMessage(processStartInfo), inner)
        {
        }

        protected ProcessStartException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }

        private static string GetMessage(ProcessStartInfo processStartInfo)
        {
            if (processStartInfo == null)
            {
                throw new ArgumentNullException(nameof(processStartInfo));
            }

            return $"Process start failed. Filename:<{processStartInfo.FileName}> Arguments:<{processStartInfo.Arguments}> WorkingDirectory:<{processStartInfo.WorkingDirectory}>.";
        }
    }
}
