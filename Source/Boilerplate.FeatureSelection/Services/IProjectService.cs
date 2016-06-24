namespace Boilerplate.FeatureSelection.Services
{
    using System.Threading.Tasks;

    /// <summary>
    /// Edit or delete files or directories in a project.
    /// </summary>
    public interface IProjectService
    {
        string ProjectDirectoryPath { get; }

        string ProjectFileName { get; }

        string ProjectFilePath { get; }

        string ProjectName { get; }

        /// <summary>
        /// Deletes the specified directory in the project.
        /// </summary>
        /// <param name="relativeDirectoryPath">The path to the directory to delete, relative to the project.</param>
        void DeleteDirectory(string relativeDirectoryPath);

        /// <summary>
        /// Deletes the specified file in the project.
        /// </summary>
        /// <param name="relativeFilePath">The path to the file to delete, relative to the project.</param>
        void DeleteFile(string relativeFilePath);

        Task EditComment(string commentName, EditCommentMode mode);

        Task EditCommentInFile(string commentName, EditCommentMode mode, string relativeFilePath);

        Task EditCommentByPattern(string commentName, EditCommentMode mode, string searchPattern);

        Task Replace(string oldValue, string newValue);

        Task ReplaceInFile(string oldValue, string newValue, string relativeFilePath);

        Task ReplaceByPattern(string oldValue, string newValue, string searchPattern);

        Task RegexReplace(string pattern, string replacement);

        Task RegexReplaceInFile(string pattern, string replacement, string relativeFilePath);

        Task RegexReplaceByPattern(string pattern, string replacement, string searchPattern);
    }
}
