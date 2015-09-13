namespace Boilerplate.Wizard.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public class FileSystemService : IFileSystemService
    {
        public Task DirectoryDelete(string directoryPath)
        {
            return Task.Run(() => Directory.Delete(directoryPath, true));
        }

        public bool DirectoryExists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        public Task<string[]> DirectoryGetAllFiles(string directoryPath)
        {
            return DirectoryGetAllFiles(directoryPath, "*");
        }

        public Task<string[]> DirectoryGetAllFiles(string directoryPath, string searchPattern)
        {
            return Task.Run(() => Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories));
        }

        public Task FileDelete(string filePath)
        {
            return Task.Run(() => File.Delete(filePath));
        }

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public async Task<string[]> FileReadAllLines(string filePath)
        {
            List<string> lines = new List<string>();

            using (StreamReader streamReader = File.OpenText(filePath))
            {
                while (!streamReader.EndOfStream)
                {
                    string line = await streamReader.ReadLineAsync();
                    lines.Add(line);
                }
            }

            return lines.ToArray();
        }

        public async Task<string> FileReadAllText(string filePath)
        {
            using (StreamReader streamReader = File.OpenText(filePath))
            {
                return await streamReader.ReadToEndAsync();
            }
        }

        public async Task FileWriteAllLines(string filePath, IEnumerable<string> lines)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    foreach (string line in lines)
                    {
                        await streamWriter.WriteLineAsync(line);
                    }
                }
            }
        }

        public async Task FileWriteAllText(string filePath, string text)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    await streamWriter.WriteAsync(text);
                }
            }
        }
    }
}