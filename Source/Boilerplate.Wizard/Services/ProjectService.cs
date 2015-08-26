namespace Boilerplate.Wizard.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public class ProjectService : IProjectService
    {
        private readonly IFileSystemService fileSystemService;
        private readonly string projectFilePath;
        private readonly string projectDirectoryPath;

        #region Constructors

        public ProjectService(IFileSystemService fileSystemService, string projectFilePath)
        {
            this.fileSystemService = fileSystemService;
            this.projectFilePath = projectFilePath;
            this.projectDirectoryPath = Path.GetDirectoryName(this.projectFilePath);
        } 

        #endregion

        #region Public Methods

        public async Task DeleteDirectory(string relativeDirectoryPath)
        {
            string directoryPath = Path.Combine(this.projectDirectoryPath, relativeDirectoryPath);
            if (this.fileSystemService.DirectoryExists(directoryPath))
            {
                await this.fileSystemService.DirectoryDelete(directoryPath);
            }
        }

        public async Task DeleteFile(string relativeFilePath)
        {
            string filePath = Path.Combine(this.projectDirectoryPath, relativeFilePath);
            if (this.fileSystemService.FileExists(filePath))
            {
                await this.fileSystemService.FileDelete(filePath);
            }
        }

        public async Task DeleteComment(string commentName, DeleteCommentMode mode)
        {
            foreach (string filePath in await this.fileSystemService.DirectoryGetAllFiles(this.projectDirectoryPath))
            {
                await this.DeleteCommentInternal(commentName, filePath, mode);
            }
        }

        public async Task DeleteComment(string commentName, DeleteCommentMode mode, string relativeFilePath)
        {
            string filePath = Path.Combine(this.projectDirectoryPath, relativeFilePath);
            if (this.fileSystemService.FileExists(filePath))
            {
                await this.DeleteCommentInternal(commentName, filePath, mode);
            }
        }

        #endregion

        #region Private Methods

        private async Task DeleteCommentInternal(string commentName, string filePath, DeleteCommentMode mode)
        {
            string fileExtension = Path.GetExtension(filePath);
            Comment comment = Comment.GetComment(fileExtension);

            if (comment == null)
            {
                // We don't support this file extension.
                return;
            }

            NamedComment namedComment = new NamedComment(commentName, comment);

            string[] lines = await this.fileSystemService.FileReadAllLines(filePath);
            List<string> newLines = new List<string>(lines.Length);

            bool isUncommenting = false;
            foreach (string line in lines)
            {
                if (isUncommenting)
                {
                    if (line.Contains(namedComment.End))
                    {
                        isUncommenting = false;
                    }
                    else if (mode != DeleteCommentMode.StartEndCommentAndCode)
                    {
                        string newLine = line;

                        if (mode == DeleteCommentMode.StartEndCommentAndUncommentCode)
                        {
                            if (newLine.TrimStart().StartsWith(comment.Start))
                            {
                                int commentIndex = newLine.IndexOf(comment.Start);
                                newLine = newLine.Substring(0, commentIndex) +
                                    new string(' ', comment.Start.Length) +
                                    newLine.Substring(commentIndex + comment.Start.Length);
                            }

                            if (comment.HasEnd && newLine.TrimEnd().EndsWith(comment.End))
                            {
                                int commentIndex = newLine.LastIndexOf(comment.End);
                                newLine = newLine.Substring(0, commentIndex) +
                                    newLine.Substring(commentIndex + comment.End.Length);
                            }
                        }

                        newLines.Add(newLine);
                    }
                }
                else if (line.Contains(namedComment.Start))
                {
                    isUncommenting = true;
                }
                else
                {
                    newLines.Add(line);
                }
            }

            await this.fileSystemService.FileWriteAllLines(filePath, newLines);
        }

        #endregion

        #region Private Classes

        private class NamedComment
        {
            public NamedComment(string commentName, Comment comment)
            {
                string commentEndWithSpace = string.IsNullOrEmpty(comment.End) ? string.Empty : " " + comment.End;
                this.Start = $"{comment.Start} $Start-{commentName}${commentEndWithSpace}";
                this.End = $"{comment.Start} $End-{commentName}${commentEndWithSpace}";
            }

            public string Start { get; set; }

            public string End { get; set; }
        }

        private class Comment
        {
            public static readonly Comment Razor = new Comment("@*", "*@");
            public static readonly Comment Slash = new Comment("//");
            public static readonly Comment Text = new Comment("#");
            public static readonly Comment Xml = new Comment("<!--", "-->");

            public Comment(string startComment)
            {
                Start = startComment;
            }

            public Comment(string start, string end)
            {
                this.End = end;
                this.HasEnd = true;
                this.Start = start;
            }

            public bool HasEnd { get; set; }

            public string Start { get; set; }

            public string End { get; set; }

            public static Comment GetComment(string fileExtension)
            {
                if (string.Equals(fileExtension, ".cs", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".js", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".ts", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".json", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".css", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".scss", StringComparison.OrdinalIgnoreCase))
                {
                    return Slash;
                }
                else if (string.Equals(fileExtension, ".html", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".config", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".xproj", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".xml", StringComparison.OrdinalIgnoreCase))
                {
                    return Xml;
                }
                else if (string.Equals(fileExtension, ".cshtml", StringComparison.OrdinalIgnoreCase))
                {
                    return Razor;
                }
                else if (string.Equals(fileExtension, ".ini", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".txt", StringComparison.OrdinalIgnoreCase))
                {
                    return Text;
                }

                return null;
            }
        }

        #endregion
    }
}