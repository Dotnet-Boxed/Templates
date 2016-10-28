namespace Boilerplate.FeatureSelection.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FileSystemService : IFileSystemService
    {
        private readonly IEnumerable<IFileFixerService> fileFixerServices;
        private readonly ConcurrentDictionary<string, string> files;

        public FileSystemService(IEnumerable<IFileFixerService> fileFixerServices)
        {
            this.fileFixerServices = fileFixerServices;
            this.files = new ConcurrentDictionary<string, string>();
        }

        public void DirectoryDelete(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                this.files.AddOrUpdate(directoryPath, (string)null, (x, y) => (string)null);
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
            this.files.AddOrUpdate(filePath, (string)null, (x, y) => (string)null);
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

                files.AddOrUpdate(filePath, text, (x, y) => text);
            }

            return text;
        }

        public void FileWriteAllLines(string filePath, IEnumerable<string> lines)
        {
            this.FileWriteAllText(filePath, string.Join(Environment.NewLine, lines));
        }

        public void FileWriteAllText(string filePath, string text)
        {
            this.files.AddOrUpdate(filePath, text, (x, y) => text);
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
                        var fileExtension = Path.GetExtension(filePath);
                        foreach (IFileFixerService fileFixerService in this.fileFixerServices
                            .Where(x => x.FileExtensions.Contains(fileExtension)))
                        {
                            text = fileFixerService.Fix(text);
                        }

                        var fileName = Path.GetFileName(filePath);
                        var encoding = GetEncoding(fileName);

                        using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter(fileStream, encoding))
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
                        await DeleteDirectory(filePath);
                    }
                }
            }

            this.files.Clear();
        }

        private static async Task DeleteDirectory(string directoryPath)
        {
            await Task.Run(
                async () =>
                {
                    for (int i = 0; i < 20; ++i)
                    {
                        try
                        {
                            Directory.Delete(directoryPath, true);
                            break;
                        }
                        catch (IOException)
                        {
                            await Task.Delay(50);
                        }
                    }
                });
        }

        private static Encoding GetEncoding(string fileName)
        {
            var encoding = Encoding.UTF8;

            if (string.Equals(fileName, "nginx.conf", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(fileName, "mime.types", StringComparison.OrdinalIgnoreCase))
            {
                encoding = new UTF8Encoding(false);
            }

            return encoding;
        }
    }
}