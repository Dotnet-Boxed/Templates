namespace Boilerplate.FeatureSelection.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Edit or delete files or directories in a project.
    /// </summary>
    public class ProjectService : IProjectService
    {
        private readonly IFileSystemService fileSystemService;

        #region Constructors

        public ProjectService(IFileSystemService fileSystemService, string projectFilePath)
        {
            this.fileSystemService = fileSystemService;
            this.ProjectDirectoryPath = Path.GetDirectoryName(projectFilePath);
            this.ProjectFileName = Path.GetFileName(projectFilePath);
            this.ProjectFilePath = projectFilePath;
            this.ProjectName = Path.GetFileNameWithoutExtension(projectFilePath);
        }

        #endregion

        #region Public Properties

        public string ProjectDirectoryPath { get; }

        public string ProjectFileName { get; }

        public string ProjectFilePath { get; }

        public string ProjectName { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Deletes the specified directory in the project.
        /// </summary>
        /// <param name="relativeDirectoryPath">The path to the directory to delete, relative to the project.</param>
        public void DeleteDirectory(string relativeDirectoryPath)
        {
            string directoryPath = Path.Combine(this.ProjectDirectoryPath, relativeDirectoryPath);
            if (this.fileSystemService.DirectoryExists(directoryPath))
            {
                this.fileSystemService.DirectoryDelete(directoryPath);
            }
        }

        /// <summary>
        /// Deletes the specified file in the project.
        /// </summary>
        /// <param name="relativeFilePath">The path to the file to delete, relative to the project.</param>
        public void DeleteFile(string relativeFilePath)
        {
            string filePath = Path.Combine(this.ProjectDirectoryPath, relativeFilePath);
            if (this.fileSystemService.FileExists(filePath))
            {
                this.fileSystemService.FileDelete(filePath);
            }
        }

        public async Task EditComment(string commentName, EditCommentMode mode)
        {
            foreach (string filePath in await this.fileSystemService.DirectoryGetAllFiles(this.ProjectDirectoryPath))
            {
                await this.EditCommentInternal(commentName, mode, filePath);
            }
        }

        public async Task EditCommentInFile(string commentName, EditCommentMode mode, string relativeFilePath)
        {
            string filePath = Path.Combine(this.ProjectDirectoryPath, relativeFilePath);
            if (this.fileSystemService.FileExists(filePath))
            {
                await this.EditCommentInternal(commentName, mode, filePath);
            }
        }

        public async Task EditCommentByPattern(string commentName, EditCommentMode mode, string searchPattern)
        {
            foreach (string filePath in await this.fileSystemService.DirectoryGetAllFiles(this.ProjectDirectoryPath, searchPattern))
            {
                await this.EditCommentInternal(commentName, mode, filePath);
            }
        }

        public async Task Replace(string oldValue, string newValue)
        {
            foreach (string filePath in await this.fileSystemService.DirectoryGetAllFiles(this.ProjectDirectoryPath))
            {
                await this.ReplaceInFile(oldValue, newValue, filePath);
            }
        }

        public async Task ReplaceInFile(string oldValue, string newValue, string relativeFilePath)
        {
            string filePath = Path.Combine(this.ProjectDirectoryPath, relativeFilePath);
            if (this.fileSystemService.FileExists(filePath))
            {
                string text = await this.fileSystemService.FileReadAllText(filePath);
                text = text.Replace(oldValue, newValue);
                this.fileSystemService.FileWriteAllText(filePath, text);
            }
        }

        public async Task ReplaceByPattern(string oldValue, string newValue, string searchPattern)
        {
            foreach (string filePath in await this.fileSystemService.DirectoryGetAllFiles(this.ProjectDirectoryPath, searchPattern))
            {
                await this.ReplaceInFile(oldValue, newValue, filePath);
            }
        }

        public async Task RegexReplace(string pattern, string replacement)
        {
            foreach (string filePath in await this.fileSystemService.DirectoryGetAllFiles(this.ProjectDirectoryPath))
            {
                await this.RegexReplaceInFile(pattern, replacement, filePath);
            }
        }

        public async Task RegexReplaceInFile(string pattern, string replacement, string relativeFilePath)
        {
            string filePath = Path.Combine(this.ProjectDirectoryPath, relativeFilePath);
            if (this.fileSystemService.FileExists(filePath))
            {
                string text = await this.fileSystemService.FileReadAllText(filePath);
                text = Regex.Replace(text, pattern, replacement);
                this.fileSystemService.FileWriteAllText(filePath, text);
            }
        }

        public async Task RegexReplaceByPattern(string pattern, string replacement, string searchPattern)
        {
            foreach (string filePath in await this.fileSystemService.DirectoryGetAllFiles(this.ProjectDirectoryPath, searchPattern))
            {
                await this.RegexReplaceInFile(pattern, replacement, filePath);
            }
        }

        #endregion

        #region Private Methods

        private async Task EditCommentInternal(string commentName, EditCommentMode mode, string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);
            Comment[] comments = Comment.GetComments(fileExtension);

            if (comments.Length > 0)
            {
                string[] lines = await this.fileSystemService.FileReadAllLines(filePath);

                foreach (Comment comment in comments)
                {
                    NamedComment namedComment = new NamedComment(commentName, comment);
                    lines = EditCommentInternal(lines, namedComment, mode);
                }

                this.fileSystemService.FileWriteAllLines(filePath, lines);
            }
        }

        private string[] EditCommentInternal(string[] lines, NamedComment namedComment, EditCommentMode mode)
        {
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
                    else if (mode != EditCommentMode.DeleteCode)
                    {
                        string newLine = line;

                        if (mode == EditCommentMode.UncommentCode)
                        {
                            if (newLine.TrimStart().StartsWith(namedComment.Comment.Start))
                            {
                                int commentIndex = newLine.IndexOf(namedComment.Comment.Start);
                                newLine = newLine.Substring(0, commentIndex) +
                                    (newLine[commentIndex + namedComment.Comment.Start.Length] == ' ' ?
                                    newLine.Substring(commentIndex + namedComment.Comment.Start.Length + 1) :
                                    newLine.Substring(commentIndex + namedComment.Comment.Start.Length));
                            }

                            if (namedComment.Comment.HasEnd && newLine.TrimEnd().EndsWith(namedComment.Comment.End))
                            {
                                int commentIndex = newLine.LastIndexOf(namedComment.Comment.End);
                                newLine = newLine.Substring(0, commentIndex) +
                                    newLine.Substring(commentIndex + namedComment.Comment.End.Length);
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

            if (isUncommenting)
            {
                throw new InvalidOperationException($"No end comment was found. End:<{namedComment.End}>.");
            }

            return newLines.ToArray();
        }

        #endregion

        #region Private Classes

        /// <summary>
        /// Represents the start and end comment in the format [Comment-Start] $[Start|End]-[CommentName]$ [Comment-End].
        /// </summary>
        private class NamedComment
        {
            public NamedComment(string commentName, Comment comment)
            {
                string commentEndWithSpace = string.IsNullOrEmpty(comment.End) ? string.Empty : " " + comment.End;
                this.Comment = comment;
                this.Start = $"{comment.Start} $Start-{commentName}${commentEndWithSpace}";
                this.End = $"{comment.Start} $End-{commentName}${commentEndWithSpace}";
            }

            public Comment Comment { get; private set; }

            public string End { get; private set; }

            public string Start { get; private set; }
        }

        /// <summary>
        /// Represents the start and also optionally (depending on the language) the end of a comment e.g. '//' or '@* *@'.
        /// </summary>
        private class Comment
        {
            private static readonly Comment Razor = new Comment("@*", "*@");
            private static readonly Comment Slash = new Comment("//");
            private static readonly Comment Text = new Comment("#");
            private static readonly Comment Xml = new Comment("<!--", "-->");

            private Comment(string start)
            {
                this.Start = start;
            }

            private Comment(string start, string end)
            {
                this.End = end;
                this.Start = start;
            }

            public string End { get; private set; }

            public bool HasEnd { get { return this.End != null; } }

            public string Start { get; private set; }

            /// <summary>
            /// Gets a collection of Comment's allowed in files with the specified file extension.
            /// </summary>
            /// <param name="fileExtension">The file extension of the file.</param>
            /// <returns>A collection of comments allowed in the file.</returns>
            public static Comment[] GetComments(string fileExtension)
            {
                if (string.Equals(fileExtension, ".cs", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".js", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".ts", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".json", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".css", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".scss", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".jscsrc", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".jshintrc", StringComparison.OrdinalIgnoreCase))
                {
                    return new Comment[] { Slash };
                }
                else if (string.Equals(fileExtension, ".html", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".config", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".csproj", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".xml", StringComparison.OrdinalIgnoreCase))
                {
                    return new Comment[] { Xml };
                }
                else if (string.Equals(fileExtension, ".cshtml", StringComparison.OrdinalIgnoreCase))
                {
                    return new Comment[] { Slash, Xml, Razor };
                }
                else if (string.Equals(fileExtension, ".ini", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".txt", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(fileExtension, ".conf", StringComparison.OrdinalIgnoreCase))
                {
                    return new Comment[] { Text };
                }

                return new Comment[0];
            }
        }

        #endregion
    }
}