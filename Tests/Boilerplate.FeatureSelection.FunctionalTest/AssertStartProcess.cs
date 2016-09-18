namespace Boilerplate.FeatureSelection.FunctionalTest
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Xunit;

    public static class ProcessAssert
    {
        public static async Task AssertStart(
            string workingDirectory,
            string fileName,
            string arguments,
            TimeSpan timeout)
        {
            var showConsole = Debugger.IsAttached;

            using (var process = Process.Start(
                new ProcessStartInfo()
                {
                    CreateNoWindow = !showConsole,
                    FileName = fileName,
                    Arguments = arguments,
                    RedirectStandardError = !showConsole,
                    UseShellExecute = false,
                    WorkingDirectory = workingDirectory
                }))
            {
                var timedOut = !process.WaitForExit((int)timeout.TotalMilliseconds);

                var standardError = string.Empty;
                if (!showConsole)
                {
                    standardError = await process.StandardError.ReadToEndAsync();
                }

                var result = timedOut ? "Timed Out" : process.ExitCode == 0 ? "Succeeded" : "Failed";
                var message = $"Executing {fileName} {arguments} {result}.\r\n\r\nStandardError:\r\n{standardError}";
                if (timedOut || process.ExitCode != 0)
                {
                    Assert.False(true, message);
                }
                else
                {
                    Debug.WriteLine(message);
                }
            }
        }
    }
}
