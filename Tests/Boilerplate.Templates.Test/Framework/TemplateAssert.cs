namespace Boilerplate.Templates.Test
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

        public static Task DotnetNewInstall<T>(string projectName) =>
            DotnetNewInstall(Path.GetDirectoryName(GetProjectFilePath(typeof(T).GetTypeInfo().Assembly, projectName)));

        public static Task DotnetNewInstall(Assembly assembly, string projectName) =>
            DotnetNewInstall(Path.GetDirectoryName(GetProjectFilePath(assembly, projectName)));

        public static Task DotnetNewInstall(string source, TimeSpan? timeout = null) =>
            ProcessAssert.AssertStart(
                DirectoryExtended.GetCurrentDirectory(),
                "dotnet",
                $"new --install \"{source}\"",
                timeout ?? TimeSpan.FromSeconds(20));

        private static string GetProjectFilePath(Assembly assembly, string projectName)
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
