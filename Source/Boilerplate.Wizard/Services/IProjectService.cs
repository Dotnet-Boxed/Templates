namespace Boilerplate.Wizard.Services
{
    using System.Threading.Tasks;

    public interface IProjectService
    {
        Task DeleteDirectory(string relativeDirectoryPath);

        Task DeleteFile(string relativeFilePath);

        Task EditComment(string commentName, EditCommentMode mode);

        Task EditComment(string commentName, EditCommentMode mode, string relativeFilePath);

        Task Replace(string oldValue, string newValue);

        Task Replace(string oldValue, string newValue, string relativeFilePath);

        Task RegexReplace(string pattern, string replacement);

        Task RegexReplace(string pattern, string replacement, string relativeFilePath);
    }
}
