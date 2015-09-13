namespace Boilerplate.Wizard.Services
{
    using System.Threading.Tasks;

    public interface IProjectService
    {
        Task DeleteDirectory(string relativeDirectoryPath);

        Task DeleteFile(string relativeFilePath);

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
