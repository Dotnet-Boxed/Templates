namespace Boilerplate.Templates.Test
{
    using System.IO;

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
    }
}
