namespace Boilerplate.Templates.Test
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Loader;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyModel;
    using Xunit;

    public static class TemplateAssert
    {
        private static readonly string[] DefaultUrls = new string[] { "http://localhost", "https://localhost" };
        private static string tempDirectoryPath;

        public static string TempDirectoryPath
        {
            get => tempDirectoryPath;
            set
            {
                tempDirectoryPath = string.IsNullOrEmpty(value) ?
                    Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()) :
                    value;
                DirectoryExtended.CheckCreate(tempDirectoryPath);
            }
        }

        public static Task DotnetNewInstall(string source, TimeSpan? timeout = null) =>
            ProcessAssert.AssertStart(TempDirectoryPath, "dotnet", $"new --install \"{source}\"", timeout ?? TimeSpan.FromSeconds(20));

        public static async Task<Project> DotnetNew(string templateName, string name = null, TimeSpan? timeout = null)
        {
            await ProcessAssert.AssertStart(TempDirectoryPath, "dotnet", $"new {templateName} --name \"{name}\"", timeout ?? TimeSpan.FromSeconds(20));
            var projectDirectoryPath = Path.Combine(TempDirectoryPath, name);
            var projectFilePath = Path.Combine(projectDirectoryPath, name + ".csproj");
            return new Project(projectDirectoryPath, projectFilePath);
        }

        public static Task DotnetRestore(string projectDirectoryPath, TimeSpan? timeout = null) =>
            ProcessAssert.AssertStart(projectDirectoryPath, "dotnet", "restore", timeout ?? TimeSpan.FromSeconds(20));

        public static Task DotnetBuild(string projectDirectoryPath, TimeSpan? timeout = null) =>
            ProcessAssert.AssertStart(projectDirectoryPath, "dotnet", "build", timeout ?? TimeSpan.FromSeconds(20));

        public static Task DotnetPublish(string projectDirectoryPath, string framework, TimeSpan? timeout = null) =>
            ProcessAssert.AssertStart(projectDirectoryPath, "dotnet", $"publish --framework {framework}", timeout ?? TimeSpan.FromSeconds(20));

        public static string GetProjectDirectoryPath(Assembly assembly, string projectName) =>
            Path.GetDirectoryName(GetProjectFilePath(assembly, projectName));

        public static string GetProjectFilePath(Assembly assembly, string projectName)
        {
            string projectFilePath = null;

            var dllPath = new Uri(assembly.CodeBase).AbsolutePath;

            for (var directory = new DirectoryInfo(dllPath); directory.Parent != null; directory = directory.Parent)
            {
                projectFilePath = directory
                    .Parent
                    .GetFiles(projectName, SearchOption.AllDirectories)
                    .Where(x => !IsInObjDirectory(x.Directory))
                    .FirstOrDefault()
                    ?.FullName;
                if (projectFilePath != null)
                {
                    break;
                }
            }

            return projectFilePath;
        }

        public static Task NpmInstall(string projectDirectoryPath, TimeSpan? timeout = null) =>
            ProcessAssert.AssertStart(projectDirectoryPath, GetNpmFilePath(), "install", timeout ?? TimeSpan.FromMinutes(5));

        public static async Task Gulp(string projectDirectoryPath, string command = null)
        {
            var gulpFilePath = Path.Combine(projectDirectoryPath, @"node_modules\.bin\gulp.cmd");
            await ProcessAssert.AssertStart(projectDirectoryPath, gulpFilePath, command, TimeSpan.FromSeconds(20));
        }

        public static async Task DotnetRun(
            string projectDirectoryPath,
            Func<TestServer, Task> action,
            string configuration = "Debug",
            string framework = "netcoreapp1.1",
            string startupTypeName = "Startup")
        {
            var projectName = Path.GetFileName(projectDirectoryPath);
            var assemblyDirectoryPath = Path.Combine(
                projectDirectoryPath,
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

        public class Project : IDisposable
        {
            public Project(string directoryPath, string filePath)
            {
                this.DirectoryPath = directoryPath;
                this.FilePath = filePath;
            }

            public string DirectoryPath { get; set; }

            public string FilePath { get; set; }

            public void Dispose() =>
                Directory.Delete(this.DirectoryPath, true);
        }

        private static bool IsInObjDirectory(DirectoryInfo directoryInfo)
        {
            if (directoryInfo == null)
            {
                return false;
            }
            else if (string.Equals(directoryInfo.Name, "obj", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return IsInObjDirectory(directoryInfo.Parent);
        }

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
