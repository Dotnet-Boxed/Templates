namespace Boxed.Templates.FunctionalTest
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    public static class TemplateAssert
    {
        public static TempDirectory GetTempDirectory() =>
            new TempDirectory(DirectoryExtended.GetTempDirectoryPath());

        public static Task DotnetNewInstall<T>(string fileName) =>
            DotnetNewInstall(typeof(T).GetTypeInfo().Assembly, fileName);

        public static Task DotnetNewInstall(Assembly assembly, string fileName)
        {
            var projectFilePath = Path.GetDirectoryName(GetFilePath(assembly, fileName));
            if (projectFilePath == null)
            {
                throw new FileNotFoundException($"{fileName} not found.");
            }

            return DotnetNewInstall(projectFilePath);
        }

        public static Task DotnetNewInstall(string source, TimeSpan? timeout = null) =>
            ProcessAssert.AssertStart(
                DirectoryExtended.GetCurrentDirectory(),
                "dotnet",
                $"new --install \"{source}\"",
                CancellationTokenFactory.GetCancellationToken(timeout));

        private static string GetFilePath(Assembly assembly, string projectName)
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
    }
}
