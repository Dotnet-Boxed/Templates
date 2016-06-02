namespace Boilerplate.AspNetCore
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Executes a single command with one parameter and returns a result.
    /// </summary>
    /// <typeparam name="T">The type of the parameter.</typeparam>
    public interface ICommand<T>
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// The result of the command.
        /// </returns>
        IActionResult Execute(T parameter);
    }
}
