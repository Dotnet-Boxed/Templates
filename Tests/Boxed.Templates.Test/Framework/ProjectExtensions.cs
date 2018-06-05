namespace Boxed.Templates.Test
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
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

        public static async Task DotnetRun(
            this Project project,
            Func<HttpClient, HttpClient, Task> action,
            Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> validateCertificate = null,
            TimeSpan? delay = null)
        {
            var httpPort = PortHelper.GetFreeTcpPort();
            var httpsPort = PortHelper.GetFreeTcpPort();
            var httpUrl = $"http://localhost:{httpPort}";
            var httpsUrl = $"https://localhost:{httpsPort}";

            var dotnetRun = await DotnetRunInternal(project.DirectoryPath, delay, httpUrl, httpsUrl);

            var httpClientHandler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = validateCertificate ?? DefaultValidateCertificate,
            };
            var httpClient = new HttpClient(httpClientHandler) { BaseAddress = new Uri(httpUrl) };
            var httpsClient = new HttpClient(httpClientHandler) { BaseAddress = new Uri(httpsUrl) };

            Exception unhandledException = null;
            try
            {
                await action(httpClient, httpsClient);
            }
            catch (Exception exception)
            {
                unhandledException = exception;
            }

            httpClient.Dispose();
            httpsClient.Dispose();
            httpClientHandler.Dispose();
            dotnetRun.Dispose();

            if (unhandledException != null)
            {
                Assert.False(true, unhandledException.ToString());
            }
        }

        public static async Task DotnetRunInMemory(
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

        private static async Task<IDisposable> DotnetRunInternal(
            string directoryPath,
            TimeSpan? delay = null,
            params string[] urls)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var urlsParameter = string.Join(';', urls);
            var task = ProcessAssert.AssertStart(
                directoryPath,
                "dotnet",
                $"run --urls {urlsParameter}",
                cancellationTokenSource.Token);
            await Task.Delay(delay ?? TimeSpan.FromSeconds(2));

            return new DisposableAction(
                () =>
                {
                    cancellationTokenSource.Cancel();

                    try
                    {
                        task.Wait();
                    }
                    catch (AggregateException exception)
                    when (exception.GetBaseException().GetBaseException() is TaskCanceledException)
                    {
                    }
                });
        }

        private static bool DefaultValidateCertificate(
            HttpRequestMessage request,
            X509Certificate2 certificate,
            X509Chain chain,
            SslPolicyErrors errors) => true;
    }
}
