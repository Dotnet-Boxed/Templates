namespace Boilerplate.FeatureSelection.FunctionalTest
{
    using System.IO;
    using System.Runtime.InteropServices;

    public static class DirectoryExtended
    {
        public static void Copy(string sourceDirectoryPath, string destinationDirectoryPath)
        {
            sourceDirectoryPath = sourceDirectoryPath.TrimEnd('\\');

            foreach (string directoryPath in Directory.GetDirectories(
                sourceDirectoryPath,
                "*",
                SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(directoryPath.Replace(sourceDirectoryPath, destinationDirectoryPath));
            }

            foreach (string newPath in Directory.GetFiles(
                sourceDirectoryPath,
                "*.*",
                SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourceDirectoryPath, destinationDirectoryPath), true);
            }
        }

        public static void Delete(string directoryPath)
        {
            DeleteFile(directoryPath);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool DeleteFile(string path);
    }
}
