namespace Boilerplate.Templates.Test
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Xunit;

    public static class ProjectExtensions
    {
        private static readonly string[] DefaultUrls = new string[] { "http://localhost", "https://localhost" };

        public static Task DotnetRestore(this Project project, TimeSpan? timeout = null) =>
            ProcessAssert.AssertStart(
                project.DirectoryPath,
                "dotnet",
                "restore",
                CancellationTokenFactory.GetCancellationToken(timeout));

        public static Task DotnetBuild(this Project project, bool? noRestore = true, TimeSpan? timeout = null)
        {
            var noRestoreArgument = noRestore == null ? null : "--no-restore";
            return ProcessAssert.AssertStart(
                project.DirectoryPath,
                "dotnet",
                $"build {noRestoreArgument}",
                CancellationTokenFactory.GetCancellationToken(timeout));
        }

        public static Task DotnetPublish(
            this Project project,
            string framework = null,
            string runtime = null,
            bool? noRestore = true,
            TimeSpan? timeout = null)
        {
            var frameworkArgument = framework == null ? null : $"--framework {framework}";
            var runtimeArgument = runtime == null ? null : $"--self-contained --runtime {runtime}";
            var noRestoreArgument = noRestore == null ? null : "--no-restore";
            DirectoryExtended.CheckCreate(project.PublishDirectoryPath);
            return ProcessAssert.AssertStart(
                project.DirectoryPath,
                "dotnet",
                $"publish {noRestoreArgument} {frameworkArgument} {runtimeArgument} --output {project.PublishDirectoryPath}",
                CancellationTokenFactory.GetCancellationToken(timeout));
        }

        public static async Task DotnetRun(this Project project, Func<Task> action, TimeSpan? delay = null)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var task = ProcessAssert.AssertStart(
                project.DirectoryPath,
                "dotnet",
                "run",
                cancellationTokenSource.Token);
            await Task.Delay(delay ?? TimeSpan.FromSeconds(3));

            Exception unhandledException = null;
            try
            {
                await action();
            }
            catch (Exception exception)
            {
                unhandledException = exception;
            }

            cancellationTokenSource.Cancel();

            try
            {
                await task;
            }
            catch (ProcessStartException exception) when (exception.GetBaseException() is TaskCanceledException)
            {
            }

            if (unhandledException != null)
            {
                Assert.False(true, unhandledException.ToString());
            }
        }

        public static async Task DotnetRun2(
            this Project project,
            Func<TestServer, Task> action,
            string environmentName = "Development",
            string startupTypeName = "Startup")
        {
            var projectName = Path.GetFileName(project.DirectoryPath);
            var directoryPath = project.PublishDirectoryPath;
            var assemblyFilePath = Path.Combine(directoryPath, $"{projectName}.dll");

            if (string.IsNullOrEmpty(assemblyFilePath))
            {
                Assert.False(true, $"Project assembly {assemblyFilePath} not found.");
            }
            else
            {
                var assembly = new AssemblyLoader2(directoryPath).LoadFromAssemblyPath(assemblyFilePath);
                var startupType = assembly.ExportedTypes
                    .FirstOrDefault(x => string.Equals(x.Name, startupTypeName, StringComparison.Ordinal));
                if (startupType == null)
                {
                    Assert.False(true, $"Startup type '{startupTypeName}' not found.");
                }

                var webHostBuilder = new WebHostBuilder()
                    .UseEnvironment(environmentName)
                    .UseStartup(startupType)
                    .UseUrls(DefaultUrls);
                using (var testServer = new TestServer(webHostBuilder))
                {
                    var response = testServer.CreateClient().GetAsync("/");
                    await action(testServer);
                }

                // TODO: Unload startupType when supported: https://github.com/dotnet/corefx/issues/14724
            }
        }
    }
}
