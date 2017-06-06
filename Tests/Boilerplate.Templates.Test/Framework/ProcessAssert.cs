namespace Boilerplate.Templates.Test
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
            TimedOut
        }

        public static async Task AssertStart(
            string workingDirectory,
            string fileName,
            string arguments,
            TimeSpan timeout)
        {
            var output = new StringBuilder();
            var error = new StringBuilder();
            ProcessResult result;
            try
            {
                var exitCode = await StartProcess(
                    fileName,
                    arguments,
                    workingDirectory,
                    (int)timeout.TotalMilliseconds,
                    new StringWriter(output),
                    new StringWriter(error));
                result = exitCode == 0 ? ProcessResult.Succeeded : ProcessResult.Failed;
            }
            catch (TaskCanceledException)
            {
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
            Debug.WriteLine(message);
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

            var message = $"Executing {fileName} {arguments} from {workingDirectory}";
            stringBuilder.AppendLine(message);
            Console.WriteLine(message);

            stringBuilder.AppendLine($"Result: {result}");
            Console.Write("Result: ");
            using (new ConsoleColorScope(result == ProcessResult.Succeeded ? ConsoleColor.Green : ConsoleColor.Red))
            {
                Console.WriteLine(result);
            }

            if (!string.IsNullOrEmpty(standardError))
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"StandardError: {standardError}");
                Console.WriteLine("StandardError: ");
                using (new ConsoleColorScope(ConsoleColor.Red))
                {
                    Console.WriteLine(standardError);
                    Console.WriteLine();
                }
            }

            if (!string.IsNullOrEmpty(standardOutput))
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"StandardOutput: {standardOutput}");
                Console.WriteLine();
                Console.WriteLine($"StandardOutput: {standardOutput}");
            }

            return stringBuilder.ToString();
        }

        private static async Task<int> StartProcess(
            string filename,
            string arguments,
            string workingDirectory = null,
            int? timeout = null,
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
                WorkingDirectory = workingDirectory
            };

            try
            {
                using (var process = new Process() { StartInfo = processStartInfo })
                {
                    process.Start();
                    var cancellationTokenSource = timeout.HasValue ?
                        new CancellationTokenSource(timeout.Value) :
                        new CancellationTokenSource();

                    var tasks = new List<Task>(3) { process.WaitForExitAsync(cancellationTokenSource.Token) };
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
                            cancellationTokenSource.Token));
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
                            cancellationTokenSource.Token));
                    }

                    await Task.WhenAll(tasks);
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
            CancellationToken cancellationToken = default(CancellationToken))
        {
            process.EnableRaisingEvents = true;

            var taskCompletionSource = new TaskCompletionSource<object>();

            EventHandler handler = null;
            handler = (sender, args) =>
            {
                process.Exited -= handler;
                taskCompletionSource.TrySetResult(null);
            };
            process.Exited += handler;

            if (cancellationToken != default(CancellationToken))
            {
                cancellationToken.Register(
                    () =>
                    {
                        process.Exited -= handler;
                        taskCompletionSource.TrySetCanceled();
                    });
            }

            return taskCompletionSource.Task;
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
            CancellationToken cancellationToken = default(CancellationToken))
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

            if (cancellationToken != default(CancellationToken))
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
