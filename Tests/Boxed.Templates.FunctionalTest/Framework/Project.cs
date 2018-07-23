namespace Boxed.Templates.FunctionalTest
{
    public class Project
    {
        public Project(
            string name,
            string filePath,
            string directoryPath,
            string publishDirectoryPath)
        {
            this.Name = name;
            this.FilePath = filePath;
            this.DirectoryPath = directoryPath;
            this.PublishDirectoryPath = publishDirectoryPath;
        }

        public string DirectoryPath { get; }

        public string FilePath { get; }

        public string Name { get; }

        public string PublishDirectoryPath { get; }
    }
}
