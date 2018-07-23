namespace Boxed.Templates.FunctionalTest
{
    using System;
    using System.Diagnostics;

    public class TempDirectory : IDisposable
    {
        public TempDirectory(string directoryPath)
        {
            this.DirectoryPath = directoryPath;
            DirectoryExtended.CheckCreate(this.DirectoryPath);
        }

        public string DirectoryPath { get; }

        public void Dispose()
        {
            var task = DirectoryExtended.TryDeleteDirectory(this.DirectoryPath);
            task.Wait();
            if (!task.Result)
            {
                Debug.WriteLine($"Failed to delete directory {this.DirectoryPath}");
            }
        }
    }
}
