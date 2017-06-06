namespace Boilerplate.Templates.Test
{
    using System;
    using System.Diagnostics;
    using System.Runtime.Serialization;

#if NET461
    [Serializable]
#endif
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

#if NET461
        protected ProcessStartException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
#endif

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
