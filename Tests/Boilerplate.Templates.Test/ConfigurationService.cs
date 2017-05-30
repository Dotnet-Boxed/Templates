namespace Boilerplate.Templates.Test
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public class ConfigurationService
    {
        public string GetProjectDirectoryPath(string projectName) =>
            Path.GetDirectoryName(this.GetProjectFilePath(projectName));

        public string GetProjectFilePath(string projectName)
        {
            string projectFilePath = null;

            var dllPath = new Uri(typeof(ConfigurationService).GetTypeInfo().Assembly.CodeBase).AbsolutePath;

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

        public string GetTempDirectoryPath()
        {
            // Don't want to overwork my SSD drive, so use the D drive where available.
            var drivePath = DriveInfo
                .GetDrives()
                .Where(x => x.DriveType == DriveType.Fixed)
                .OrderByDescending(x => string.Equals(x.Name, @"D:\"))
                .FirstOrDefault()
                ?.Name;
            if (drivePath == null)
            {
                return Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            }

            return Path.Combine(drivePath, "Temp", Guid.NewGuid().ToString());
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
