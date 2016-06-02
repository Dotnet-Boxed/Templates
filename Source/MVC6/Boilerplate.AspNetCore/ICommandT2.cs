namespace Boilerplate.AspNetCore
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Executes a single command with two parameters and returns a result.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    public interface ICommand<T1, T2>
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter1">The first parameter.</param>
        /// <param name="parameter2">The second parameter.</param>
        /// <returns>
        /// The result of the command.
        /// </returns>
        IActionResult Execute(T1 parameter1, T2 parameter2);
    }
}
