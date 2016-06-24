namespace Boilerplate.FeatureSelection.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFileSystemService
    {
        void DirectoryDelete(string directoryPath);

        bool DirectoryExists(string directoryPath);

        Task<string[]> DirectoryGetAllFiles(string directoryPath);

        Task<string[]> DirectoryGetAllFiles(string directoryPath, string searchPattern);

        void FileDelete(string filePath);

        bool FileExists(string filePath);

        Task<string[]> FileReadAllLines(string filePath);

        Task<string> FileReadAllText(string filePath);

        void FileWriteAllLines(string filePath, IEnumerable<string> lines);

        void FileWriteAllText(string filePath, string text);

        Task SaveAll();
    }
}
