namespace Boilerplate.Wizard.Services
{
    public interface IProjectService
    {
        void DeleteDirectory(string relativeDirectoryPath);

        void DeleteFile(string relativeFilePath);

        void DeleteComment(string commentName, DeleteCommentMode deleteCommentMode);

        void DeleteComment(string commentName, DeleteCommentMode deleteCommentMode, string relativeFilePath);
    }
}
