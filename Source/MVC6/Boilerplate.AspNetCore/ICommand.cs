namespace Boilerplate.AspNetCore
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Executes a single command and returns a result.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <returns>The result of the command.</returns>
        IActionResult Execute();
    }
}
