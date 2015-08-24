namespace Boilerplate.Wizard.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class ProjectService : IProjectService
    {
        private readonly string projectFilePath;
        private readonly string projectDirectoryPath;

        public ProjectService(string projectFilePath)
        {
            this.projectFilePath = projectFilePath;
            this.projectDirectoryPath = Path.GetDirectoryName(this.projectFilePath);
        }

        #region Public Methods

        public void DeleteDirectory(string relativeDirectoryPath)
        {
            string directoryPath = Path.Combine(this.projectDirectoryPath, relativeDirectoryPath);
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }

        public void DeleteFile(string relativeFilePath)
        {
            string filePath = Path.Combine(this.projectDirectoryPath, relativeFilePath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public void DeleteComment(string commentName, DeleteCommentMode deleteCommentMode)
        {
            foreach (string filePath in Directory.GetFiles(this.projectDirectoryPath, "*", SearchOption.AllDirectories))
            {
                this.DeleteCommentInternal(commentName, filePath, deleteCommentMode);
            }
        }

        public void DeleteComment(string commentName, DeleteCommentMode deleteCommentMode, string relativeFilePath)
        {
            string filePath = Path.Combine(this.projectDirectoryPath, relativeFilePath);
            if (File.Exists(filePath))
            {
                this.DeleteCommentInternal(commentName, filePath, deleteCommentMode);
            }
        }

        #endregion

        #region Private Methods

        private void DeleteCommentInternal(string commentName, string filePath, DeleteCommentMode deleteCommentMode)
        {
            string fileExtension = Path.GetExtension(filePath);
            Comment comment = Comment.GetComment(fileExtension);

            if (comment == null)
            {
                // We don't support this file extension.
                return;
            }

            NamedComment namedComment = new NamedComment(commentName, comment);

            string[] lines = File.ReadAllLines(filePath);
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
                    else if (deleteCommentMode != DeleteCommentMode.StartEndCommentAndCode)
                    {
                        string newLine = line;

                        if (deleteCommentMode == DeleteCommentMode.StartEndCommentAndUncommentCode)
                        {
                            if (line.Trim().StartsWith(comment.Start))
                            {
                                int commentIndex = line.IndexOf(comment.Start);
                                newLine = line.Substring(0, commentIndex) +
                                    new string(' ', comment.Start.Length) +
                                    line.Substring(commentIndex + comment.Start.Length);
                            }

                            if (comment.HasEnd && line.Trim().EndsWith(comment.End))
                            {
                                int commentIndex = line.LastIndexOf(comment.End);
                                newLine = line.Substring(0, commentIndex) +
                                    line.Substring(commentIndex + comment.End.Length);
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

            File.WriteAllLines(filePath, newLines);
        }

        #endregion

        #region Private Classes

        private class NamedComment
        {
            public NamedComment(string commentName, Comment comment)
            {
                string commentEndWithSpace = string.IsNullOrEmpty(comment.End) ? string.Empty : " " + comment.End;
                this.Start = $"{comment.Start} $Start-{commentName}${commentEndWithSpace}";
                this.End = $"{comment} $End-{commentName}${commentEndWithSpace}";
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