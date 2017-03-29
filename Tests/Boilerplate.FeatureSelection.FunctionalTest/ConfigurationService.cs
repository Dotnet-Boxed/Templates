namespace Boilerplate.FeatureSelection.FunctionalTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public static class ConfigurationService
    {
        public static string ProjectFilePath
        {
            get
            {
                var dllPath = new Uri(typeof(ConfigurationService).Assembly.CodeBase).AbsolutePath;

                var directory = new DirectoryInfo(dllPath);
                while (!string.Equals(directory.Name, "Tests"))
                {
                    directory = directory.Parent;
                }

                var projectFilePath = directory
                    .Parent
                    .GetFiles("MVC.csproj", SearchOption.AllDirectories)
                    .Where(x => !IsInObjDirectory(x.Directory))
                    .First()
                    .FullName;
                return projectFilePath;
            }
        }

        public static string TempDirectoryPath
        {
            get
            {
                var drivePath = DriveInfo
                    .GetDrives()
                    .Where(x => x.DriveType == DriveType.Fixed)
                    .OrderByDescending(x => string.Equals(x.Name, @"D:\"))
                    .First()
                    .Name;
                return Path.Combine(drivePath, "Temp");
            }
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
