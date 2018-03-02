namespace Boilerplate.Templates.Test
{
    using System;

    public class TempDirectory : IDisposable
    {
        public TempDirectory(string directoryPath)
        {
            this.DirectoryPath = directoryPath;
            DirectoryExtended.CheckCreate(this.DirectoryPath);
        }

        public string DirectoryPath { get; }

        public void Dispose() =>
            DirectoryExtended.SafeDelete(this.DirectoryPath);
    }
}
