namespace Boilerplate.Wizard.Services
{
    public enum DeleteCommentMode
    {
        /// <summary>
        /// Delete only the start and end comment.
        /// </summary>
        StartEndComment,

        /// <summary>
        /// Delete the start and end comment, as well as the code between them.
        /// </summary>
        StartEndCommentAndCode,

        /// <summary>
        /// Delete the start and end comment and uncomment the code between them.
        /// </summary>
        StartEndCommentAndUncommentCode
    }
}
