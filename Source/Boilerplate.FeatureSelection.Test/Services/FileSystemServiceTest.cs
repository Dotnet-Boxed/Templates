namespace Boilerplate.FeatureSelection.Test.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;
    using Xunit;

    public class FileSystemServiceTest : IDisposable
    {
        private readonly string directoryPath;
        private readonly string filePath;
        private readonly IFileSystemService fileSystemService;

        public FileSystemServiceTest()
        {
            var tempDirectoryPath = Path.GetTempPath();

            this.directoryPath = Path.Combine(tempDirectoryPath, Guid.NewGuid().ToString());
            Directory.CreateDirectory(this.directoryPath);

            this.filePath = Path.Combine(tempDirectoryPath, Guid.NewGuid().ToString());
            File.WriteAllText(filePath, "Hello");

            this.fileSystemService = new FileSystemService(new List<IFileFixerService>());
        }

        [Fact]
        public async Task DirectoryDelete_DirectoryDoesNotExists_NothingHappens()
        {
            var directoryPath = Path.Combine(Path.GetTempPath(), "DirectoryDoesNotExist");

            fileSystemService.DirectoryDelete(directoryPath);
            await fileSystemService.SaveAll();
        }

        [Fact]
        public async Task DirectoryDelete_DirectoryExists_Deleted()
        {
            fileSystemService.DirectoryDelete(this.directoryPath);

            Assert.True(Directory.Exists(this.directoryPath));

            await fileSystemService.SaveAll();

            Assert.False(Directory.Exists(this.directoryPath));
        }

        [Fact]
        public async Task FileDelete_FileExists_Deleted()
        {
            fileSystemService.FileDelete(this.filePath);
            Assert.True(File.Exists(this.filePath));

            await fileSystemService.SaveAll();

            Assert.False(File.Exists(this.filePath));
        }

        [Fact]
        public void DirectoryExists_DirectoryExists_True()
        {
            bool exists = fileSystemService.DirectoryExists(this.directoryPath);

            Assert.True(exists);
        }

        [Fact]
        public void DirectoryExists_DirectoryDoesNotExist_False()
        {
            bool exists = fileSystemService.DirectoryExists(@"C:\DoesNotExist");

            Assert.False(exists);
        }

        [Fact]
        public void DirectoryExists_DirectoryDeleted_False()
        {
            fileSystemService.DirectoryDelete(this.directoryPath);

            bool exists = fileSystemService.DirectoryExists(this.directoryPath);

            Assert.False(exists);
        }

        [Fact]
        public void FileExists_FileExists_True()
        {
            bool exists = fileSystemService.FileExists(this.filePath);

            Assert.True(exists);
        }

        [Fact]
        public void FileExists_FileDoesNotExist_False()
        {
            bool exists = fileSystemService.FileExists(@"C:\DoesNotExist");

            Assert.False(exists);
        }

        [Fact]
        public void FileExists_FileDeleted_False()
        {
            fileSystemService.FileDelete(this.filePath);

            bool exists = fileSystemService.FileExists(this.filePath);

            Assert.False(exists);
        }

        [Fact]
        public async Task FileReadAllText_NoChange_ReturnsReadFile()
        {
            var text = await fileSystemService.FileReadAllText(this.filePath);

            Assert.Equal("Hello", text);
        }

        [Fact]
        public async Task SaveAll_FileWriteAllTextThenFileReadAllText_FileOnlyChangedOnSaveAll()
        {
            fileSystemService.FileWriteAllText(this.filePath, "World");
            var text = await fileSystemService.FileReadAllText(this.filePath);

            Assert.Equal("World", text);
            Assert.Equal("Hello", File.ReadAllText(this.filePath));

            await fileSystemService.SaveAll();

            Assert.Equal("World", File.ReadAllText(this.filePath));
        }

        [Fact]
        public async Task SaveAll_FileWriteAllLinesThenFileReadAllLines_FileOnlyChangedOnSaveAll()
        {
            fileSystemService.FileWriteAllLines(this.filePath, new string[] { "Line 1", "Line 2" });
            var lines = await fileSystemService.FileReadAllLines(this.filePath);

            Assert.Equal(new string[] { "Line 1", "Line 2" }, lines);
            Assert.Equal(new string[] { "Hello" }, File.ReadAllLines(this.filePath));

            await fileSystemService.SaveAll();

            Assert.Equal(new string[] { "Line 1", "Line 2" }, File.ReadAllLines(this.filePath));
        }

        public void Dispose()
        {
            if (Directory.Exists(this.directoryPath))
            {
                Directory.Delete(this.directoryPath);
            }

            if (File.Exists(this.filePath))
            {
                File.Delete(this.filePath);
            }
        }
    }
}
