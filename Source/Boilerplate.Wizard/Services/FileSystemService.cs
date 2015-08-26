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
            return Task.Run(() => Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories));
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
                string line = await streamReader.ReadLineAsync();
                lines.Add(line);
            }

            return lines.ToArray();
        }

        public async Task FileWriteAllLines(string filePath, IEnumerable<string> lines)
        {
            using (FileStream fileStream = File.OpenWrite(filePath))
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
    }
}