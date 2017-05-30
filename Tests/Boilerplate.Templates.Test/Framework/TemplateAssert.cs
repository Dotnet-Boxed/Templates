namespace Boilerplate.Templates.Test
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Runtime.Loader;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyModel;
    using Xunit;

    public class TemplateAssert : IDisposable
    {
        private static readonly string[] DefaultUrls = new string[] { "http://localhost", "https://localhost" };
        private readonly string tempDirectoryPath;
        private string projectDirectoryPath;

        public TemplateAssert(string tempDirectoryPath = null)
        {
            if (tempDirectoryPath == null)
            {
                this.tempDirectoryPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            }
            else
            {
                this.tempDirectoryPath = tempDirectoryPath;
            }

            DirectoryExtended.CheckCreate(this.tempDirectoryPath);
        }

        public Task DotnetNewInstall(string source, TimeSpan? timeout = null) =>
            ProcessAssert.AssertStart(this.tempDirectoryPath, "dotnet", $"new --install \"{source}\"", timeout ?? TimeSpan.FromSeconds(20));

        public async Task DotnetNew(string templateName, string name = null, TimeSpan? timeout = null)
        {
            await ProcessAssert.AssertStart(this.tempDirectoryPath, "dotnet", $"new {templateName} --name \"{name}\"", timeout ?? TimeSpan.FromSeconds(20));
            this.projectDirectoryPath = Path.Combine(this.tempDirectoryPath, name);
        }

        public Task DotnetRestore(TimeSpan? timeout = null) =>
            ProcessAssert.AssertStart(this.projectDirectoryPath, "dotnet", "restore", timeout ?? TimeSpan.FromSeconds(20));

        public Task DotnetBuild(TimeSpan? timeout = null) =>
            ProcessAssert.AssertStart(this.projectDirectoryPath, "dotnet", "build", timeout ?? TimeSpan.FromSeconds(20));

        public Task DotnetPublish(string framework, TimeSpan? timeout = null) =>
            ProcessAssert.AssertStart(this.projectDirectoryPath, "dotnet", $"publish --framework {framework}", timeout ?? TimeSpan.FromSeconds(20));

        public Task NpmInstall(TimeSpan? timeout = null) =>
            ProcessAssert.AssertStart(this.projectDirectoryPath, GetNpmFilePath(), "install", timeout ?? TimeSpan.FromMinutes(5));

        public async Task Gulp(string command = null)
        {
            var gulpFilePath = Path.Combine(this.projectDirectoryPath, @"node_modules\.bin\gulp.cmd");
            await ProcessAssert.AssertStart(this.projectDirectoryPath, gulpFilePath, command, TimeSpan.FromSeconds(20));
        }

        public async Task DotnetRun(
            Func<TestServer, Task> action,
            string configuration = "Debug",
            string framework = "netcoreapp1.1",
            string startupTypeName = "Startup")
        {
            var projectName = Path.GetFileName(this.projectDirectoryPath);
            var assemblyDirectoryPath = Path.Combine(
                this.projectDirectoryPath,
                $@"bin\{configuration}\{framework}\publish");
            var assemblyFilePath = Path.Combine(assemblyDirectoryPath, $"{projectName}.dll");

            if (string.IsNullOrEmpty(assemblyFilePath))
            {
                Assert.False(true, $"Project assembly {assemblyFilePath} not found.");
            }
            else
            {
                var assemblyName = AssemblyLoadContext.GetAssemblyName(assemblyFilePath);
                var assembly = new AssemblyLoader(assemblyDirectoryPath).LoadFromAssemblyName(assemblyName);
                var startupType = assembly.ExportedTypes
                    .FirstOrDefault(x => string.Equals(x.Name, startupTypeName, StringComparison.Ordinal));
                if (startupType == null)
                {
                    Assert.False(true, $"Startup type '{startupTypeName}' not found.");
                }

                var webHostBuilder = new WebHostBuilder().UseStartup(startupType).UseUrls(DefaultUrls);
                using (var testServer = new TestServer(webHostBuilder))
                {
                    await action(testServer);
                }

                // TODO: Unload startupType when supported: https://github.com/dotnet/corefx/issues/14724
            }
        }

        public class AssemblyLoader : AssemblyLoadContext
        {
            private readonly string directoryPath;

            public AssemblyLoader(string directoryPath) =>
                this.directoryPath = directoryPath;

            protected override Assembly Load(AssemblyName assemblyName)
            {
                var deps = DependencyContext.Default;
                var res = deps.CompileLibraries.Where(x => x.Name.Contains(assemblyName.Name)).ToList();
                if (res.Count > 0)
                {
                    return Assembly.Load(new AssemblyName(res.First().Name));
                }
                else
                {
                    var file = new FileInfo($"{this.directoryPath}{Path.DirectorySeparatorChar}{assemblyName.Name}.dll");
                    if (File.Exists(file.FullName))
                    {
                        var asemblyLoader = new AssemblyLoader(file.DirectoryName);
                        return asemblyLoader.LoadFromAssemblyPath(file.FullName);
                    }
                }

                return Assembly.Load(assemblyName);
            }
        }

        public void Dispose() =>
            Directory.Delete(this.tempDirectoryPath, true);

        private static string GetNpmFilePath() =>
            Environment
                .GetEnvironmentVariable("PATH")
                .Split(';')
                .Where(x => x.Contains("nodejs") && Directory.Exists(x))
                .SelectMany(x => Directory
                    .GetFiles(x, "*", SearchOption.AllDirectories)
                    .Where(y => string.Equals(Path.GetFileName(y), "npm.cmd", StringComparison.OrdinalIgnoreCase)))
                .First();
    }
}
