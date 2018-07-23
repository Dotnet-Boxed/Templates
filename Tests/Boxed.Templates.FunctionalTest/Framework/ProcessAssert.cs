namespace Boxed.Templates.FunctionalTest
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public static class ProcessAssert
    {
        private enum ProcessResult
        {
            Succeeded,
            Failed,
            TimedOut,
        }

        public static async Task AssertStart(
            string workingDirectory,
            string fileName,
            string arguments,
            CancellationToken cancellationToken)
        {
            TestLogger.WriteLine($"Executing {fileName} {arguments} from {workingDirectory}");

            var output = new StringBuilder();
            var error = new StringBuilder();
            ProcessResult result;
            try
            {
                var exitCode = await StartProcess(
                    fileName,
                    arguments,
                    workingDirectory,
                    cancellationToken,
                    new StringWriter(output),
                    new StringWriter(error));
                result = exitCode == 0 ? ProcessResult.Succeeded : ProcessResult.Failed;
            }
            catch (TaskCanceledException)
            {
                TestLogger.WriteLine($"Timed Out {fileName} {arguments} from {workingDirectory}");
                result = ProcessResult.TimedOut;
            }

            var standardOutput = output.ToString();
            var standardError = error.ToString();

            var message = GetAndWriteMessage(
                fileName,
                arguments,
                workingDirectory,
                result,
                standardOutput,
                standardError);
            if (result != ProcessResult.Succeeded)
            {
                Assert.False(true, message);
            }
        }

        private static string GetAndWriteMessage(
            string fileName,
            string arguments,
            string workingDirectory,
            ProcessResult result,
            string standardOutput,
            string standardError)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Result: {result}");
            TestLogger.Write("Result: ");
            TestLogger.WriteLine(result.ToString(), result == ProcessResult.Succeeded ? ConsoleColor.Green : ConsoleColor.Red);

            if (!string.IsNullOrEmpty(standardError))
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"StandardError: {standardError}");
                TestLogger.WriteLine("StandardError: ");
                TestLogger.WriteLine(standardError, ConsoleColor.Red);
                TestLogger.WriteLine();
            }

            if (!string.IsNullOrEmpty(standardOutput))
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"StandardOutput: {standardOutput}");
                TestLogger.WriteLine();
                TestLogger.WriteLine($"StandardOutput: {standardOutput}");
            }

            return stringBuilder.ToString();
        }

        private static async Task<int> StartProcess(
            string filename,
            string arguments,
            string workingDirectory = null,
            CancellationToken cancellationToken = default,
            TextWriter outputTextWriter = null,
            TextWriter errorTextWriter = null)
        {
            var processStartInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                Arguments = arguments,
                FileName = filename,
                RedirectStandardOutput = outputTextWriter != null,
                RedirectStandardError = errorTextWriter != null,
                UseShellExecute = false,
                WorkingDirectory = workingDirectory,
            };

            try
            {
                using (var process = new Process() { StartInfo = processStartInfo })
                {
                    process.Start();

                    var tasks = new List<Task>(3) { process.WaitForExitAsync(cancellationToken) };
                    if (outputTextWriter != null)
                    {
                        tasks.Add(ReadAsync(
                            x =>
                            {
                                process.OutputDataReceived += x;
                                process.BeginOutputReadLine();
                            },
                            x => process.OutputDataReceived -= x,
                            outputTextWriter,
                            cancellationToken));
                    }

                    if (errorTextWriter != null)
                    {
                        tasks.Add(ReadAsync(
                            x =>
                            {
                                process.ErrorDataReceived += x;
                                process.BeginErrorReadLine();
                            },
                            x => process.ErrorDataReceived -= x,
                            errorTextWriter,
                            cancellationToken));
                    }

                    try
                    {
                        await Task.WhenAll(tasks);
                    }
                    catch
                    {
                        if (!process.HasExited)
                        {
                            process.Kill();
                        }

                        throw;
                    }

                    return process.ExitCode;
                }
            }
            catch (Exception exception)
            {
                throw new ProcessStartException(processStartInfo, exception);
            }
        }

        /// <summary>
        /// Waits asynchronously for the process to exit.
        /// </summary>
        /// <param name="process">The process to wait for cancellation.</param>
        /// <param name="cancellationToken">A cancellation token. If invoked, the task will return
        /// immediately as cancelled.</param>
        /// <returns>A Task representing waiting for the process to end.</returns>
        private static Task WaitForExitAsync(
            this Process process,
            CancellationToken cancellationToken = default)
        {
            process.EnableRaisingEvents = true;

            var taskCompletionSource = new TaskCompletionSource<object>();

            process.Exited += OnExited;
            if (cancellationToken != default)
            {
                cancellationToken.Register(
                    () =>
                    {
                        process.Exited -= OnExited;
                        taskCompletionSource.TrySetCanceled();
                    });
            }

            return taskCompletionSource.Task;

            void OnExited(object sender, EventArgs e)
            {
                process.Exited -= OnExited;
                taskCompletionSource.TrySetResult(null);
            }
        }

        /// <summary>
        /// Reads the data from the specified data received event and writes it to the
        /// <paramref name="textWriter"/>.
        /// </summary>
        /// <param name="addHandler">Adds the event handler.</param>
        /// <param name="removeHandler">Removes the event handler.</param>
        /// <param name="textWriter">The text writer.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private static Task ReadAsync(
            this Action<DataReceivedEventHandler> addHandler,
            Action<DataReceivedEventHandler> removeHandler,
            TextWriter textWriter,
            CancellationToken cancellationToken = default)
        {
            var taskCompletionSource = new TaskCompletionSource<object>();

            DataReceivedEventHandler handler = null;
            handler = new DataReceivedEventHandler(
                (sender, e) =>
                {
                    if (e.Data == null)
                    {
                        removeHandler(handler);
                        taskCompletionSource.TrySetResult(null);
                    }
                    else
                    {
                        textWriter.WriteLine(e.Data);
                    }
                });

            addHandler(handler);

            if (cancellationToken != default)
            {
                cancellationToken.Register(
                    () =>
                    {
                        removeHandler(handler);
                        taskCompletionSource.TrySetCanceled();
                    });
            }

            return taskCompletionSource.Task;
        }
    }
}
