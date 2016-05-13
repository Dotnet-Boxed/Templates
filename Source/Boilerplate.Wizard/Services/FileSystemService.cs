namespace Boilerplate.Wizard.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public class FileSystemService : IFileSystemService
    {
        private ConcurrentDictionary<string, string> files;

        public FileSystemService()
        {
            this.files = new ConcurrentDictionary<string, string>();
        }

        public void DirectoryDelete(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                this.files.AddOrUpdate(directoryPath, (string)null, (x, y) => y);
            }
        }

        public bool DirectoryExists(string directoryPath)
        {
            string text;
            if (files.TryGetValue(directoryPath, out text))
            {
                return text != null;
            }

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

        public void FileDelete(string filePath)
        {
            this.files.AddOrUpdate(filePath, (string)null, (x, y) => y);
        }

        public bool FileExists(string filePath)
        {
            string text;
            if (files.TryGetValue(filePath, out text))
            {
                return text != null;
            }

            return File.Exists(filePath);
        }

        public async Task<string[]> FileReadAllLines(string filePath)
        {
            var text = await this.FileReadAllText(filePath);
            var lines = new List<string>();
            using (var stringReader = new StringReader(text))
            {
                string line;
                while ((line = stringReader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines.ToArray();
        }

        public async Task<string> FileReadAllText(string filePath)
        {
            string text;
            if (!files.TryGetValue(filePath, out text))
            {
                using (StreamReader streamReader = File.OpenText(filePath))
                {
                    text = await streamReader.ReadToEndAsync();
                }
            }

            return text;
        }

        public void FileWriteAllLines(string filePath, IEnumerable<string> lines)
        {
            this.FileWriteAllText(filePath, string.Join(Environment.NewLine, lines));
        }

        public void FileWriteAllText(string filePath, string text)
        {
            this.files.AddOrUpdate(filePath, text, (x, y) => y);
        }

        public async Task SaveAll()
        {
            foreach (var keyValuePair in this.files)
            {
                var filePath = keyValuePair.Key;
                var text = keyValuePair.Value;

                if (File.Exists(filePath))
                {
                    if (text == null)
                    {
                        await Task.Run(() => File.Delete(filePath));
                    }
                    else
                    {
                        using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter(fileStream))
                            {
                                await streamWriter.WriteAsync(text);
                            }
                        }
                    }
                }
                else if (Directory.Exists(filePath))
                {
                    if (text == null)
                    {
                        await Task.Run(() => Directory.Delete(filePath, true));
                    }
                }
            }

            this.files.Clear();
        }
    }
}