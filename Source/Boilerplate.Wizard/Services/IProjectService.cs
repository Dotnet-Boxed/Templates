namespace Boilerplate.Wizard.Services
{
    using System.Threading.Tasks;

    public interface IProjectService
    {
        Task DeleteDirectory(string relativeDirectoryPath);

        Task DeleteFile(string relativeFilePath);

        Task DeleteComment(string commentName, DeleteCommentMode deleteCommentMode);

        Task DeleteComment(string commentName, DeleteCommentMode deleteCommentMode, string relativeFilePath);
    }
}
