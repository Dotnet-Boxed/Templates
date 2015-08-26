namespace Boilerplate.Wizard.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFileSystemService
    {
        Task DirectoryDelete(string directoryPath);

        bool DirectoryExists(string directoryPath);

        Task<string[]> DirectoryGetAllFiles(string directoryPath);

        Task FileDelete(string filePath);

        bool FileExists(string filePath);

        Task<string[]> FileReadAllLines(string filePath);

        Task FileWriteAllLines(string filePath, IEnumerable<string> lines);
    }
}
