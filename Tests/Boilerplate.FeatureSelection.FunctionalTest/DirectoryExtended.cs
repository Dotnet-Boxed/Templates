namespace Boilerplate.FeatureSelection.FunctionalTest
{
    using System.IO;
    using ZetaLongPaths;

    public static class DirectoryExtended
    {
        public static void Copy(string sourceDirectoryPath, string destinationDirectoryPath)
        {
            sourceDirectoryPath = sourceDirectoryPath.TrimEnd('\\');

            foreach (string sourceSubDirectoryPath in Directory.GetDirectories(
                sourceDirectoryPath,
                "*",
                SearchOption.AllDirectories))
            {
                var destinationSubDirectoryPath = sourceSubDirectoryPath.Replace(
                    sourceDirectoryPath,
                    destinationDirectoryPath);
                new ZlpDirectoryInfo(destinationSubDirectoryPath).CheckCreate();
            }

            foreach (string sourceFilePath in Directory.GetFiles(
                sourceDirectoryPath,
                "*.*",
                SearchOption.AllDirectories))
            {
                var destinationFilePath = sourceFilePath.Replace(sourceDirectoryPath, destinationDirectoryPath);
                new ZlpFileInfo(sourceFilePath).CopyTo(destinationFilePath, true);
            }
        }

        public static void Delete(string directoryPath)
        {
            new ZlpDirectoryInfo(directoryPath).SafeDelete();
        }
    }
}
