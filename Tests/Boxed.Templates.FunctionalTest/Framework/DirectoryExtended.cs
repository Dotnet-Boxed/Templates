namespace Boxed.Templates.FunctionalTest
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;

    public static class DirectoryExtended
    {
        public static void Copy(string sourceDirectoryPath, string destinationDirectoryPath)
        {
            sourceDirectoryPath = sourceDirectoryPath.TrimEnd('\\');

            foreach (var sourceSubDirectoryPath in Directory.GetDirectories(
                sourceDirectoryPath,
                "*",
                SearchOption.AllDirectories))
            {
                var destinationSubDirectoryPath = sourceSubDirectoryPath.Replace(
                    sourceDirectoryPath,
                    destinationDirectoryPath);
                CheckCreate(destinationSubDirectoryPath);
            }

            foreach (var sourceFilePath in Directory.GetFiles(
                sourceDirectoryPath,
                "*.*",
                SearchOption.AllDirectories))
            {
                var destinationFilePath = sourceFilePath.Replace(sourceDirectoryPath, destinationDirectoryPath);
                new FileInfo(sourceFilePath).CopyTo(destinationFilePath, true);
            }
        }

        public static void CheckCreate(string directoryPath)
        {
            var destinationSubDirectory = new DirectoryInfo(directoryPath);
            if (!destinationSubDirectory.Exists)
            {
                destinationSubDirectory.Create();
            }
        }

        public static string GetTempDirectoryPath() =>
            Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

        public static string GetCurrentDirectory() =>
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static async Task<bool> TryDeleteDirectory(
           string directoryPath,
           int maxRetries = 10,
           int millisecondsDelay = 30)
        {
            if (directoryPath == null)
            {
                throw new ArgumentNullException(directoryPath);
            }

            if (maxRetries < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(maxRetries));
            }

            if (millisecondsDelay < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(millisecondsDelay));
            }

            for (var i = 0; i < maxRetries; ++i)
            {
                try
                {
                    if (Directory.Exists(directoryPath))
                    {
                        Directory.Delete(directoryPath, true);
                    }

                    return true;
                }
                catch (IOException)
                {
                    await Task.Delay(millisecondsDelay);
                }
                catch (UnauthorizedAccessException)
                {
                    await Task.Delay(millisecondsDelay);
                }
            }

            return false;
        }
    }
}
