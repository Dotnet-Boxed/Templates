namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    /// <summary>
    /// The word that appears before the title.
    /// </summary>
    public enum OpenGraphDeterminer
    {
        /// <summary>
        /// There is no word, it is blank.
        /// </summary>
        Blank,

        /// <summary>
        /// Use the word 'A'.
        /// </summary>
        A,

        /// <summary>
        /// Use the word 'An'.
        /// </summary>
        An,

        /// <summary>
        /// Use the word 'The'.
        /// </summary>
        The,

        /// <summary>
        /// Automatically select the word for you.
        /// </summary>
        Auto
    }
}
