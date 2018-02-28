namespace Boilerplate.Templates.Test
{
    using System;
    using System.IO;

    public class Project : IDisposable
    {
        public Project(
            string name,
            string filePath,
            string directoryPath)
        {
            this.Name = name;
            this.FilePath = filePath;
            this.DirectoryPath = directoryPath;
        }

        public string DirectoryPath { get; }

        public string FilePath { get; }

        public string Name { get; }

        public void Dispose() =>
            DirectoryExtended.SafeDelete(this.DirectoryPath);

        public string GetPublishDirectoryPath(string framework) =>
            Path.Combine(this.DirectoryPath, "bin", "publish", framework);
    }
}
