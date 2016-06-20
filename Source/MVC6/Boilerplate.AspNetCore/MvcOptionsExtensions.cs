namespace Boilerplate.AspNetCore
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// <see cref="MvcOptions"/> extension methods
    /// </summary>
    public static class MvcOptionsExtensions
    {
        /// <summary>
        /// Executes the specified action if the specified <paramref name="condition"/> is <c>true</c> which can be
        /// used to conditionally configure the MVC options.
        /// </summary>
        /// <param name="options">The MVC options.</param>
        /// <param name="condition">If set to <c>true</c> the action is executed.</param>
        /// <param name="action">The action used to configure the MVC options.</param>
        /// <returns>The MVC options.</returns>
        public static MvcOptions AddIf(
            this MvcOptions options,
            bool condition,
            Func<MvcOptions, MvcOptions> action)
        {
            if (condition)
            {
                options = action(options);
            }

            return options;
        }

        /// <summary>
        /// Executes the specified <paramref name="ifAction"/> if the specified <paramref name="condition"/> is
        /// <c>true</c>, otherwise executes the <paramref name="elseAction"/>. This can be used to conditionally
        /// configure the MVC options.
        /// </summary>
        /// <param name="options">The MVC options.</param>
        /// <param name="condition">If set to <c>true</c> the <paramref name="ifAction"/> is executed, otherwise the
        /// <paramref name="elseAction"/> is executed.</param>
        /// <param name="ifAction">The action used to configure the MVC options if the condition is <c>true</c>.</param>
        /// <param name="elseAction">The action used to configure the MVC options if the condition is <c>false</c>.</param>
        /// <returns>The MVC options.</returns>
        public static MvcOptions AddIfElse(
            this MvcOptions options,
            bool condition,
            Func<MvcOptions, MvcOptions> ifAction,
            Func<MvcOptions, MvcOptions> elseAction)
        {
            if (condition)
            {
                options = ifAction(options);
            }
            else
            {
                options = elseAction(options);
            }

            return options;
        }
    }
}
