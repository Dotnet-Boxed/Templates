namespace Boilerplate.FeatureSelection.Services
{
    public enum EditCommentMode
    {
        /// <summary>
        /// Delete only the start and end comment.
        /// </summary>
        LeaveCodeUnchanged,

        /// <summary>
        /// Delete the start and end comment, as well as the code between them.
        /// </summary>
        DeleteCode,

        /// <summary>
        /// Delete the start and end comment and uncomment the code between them.
        /// </summary>
        UncommentCode
    }
}
