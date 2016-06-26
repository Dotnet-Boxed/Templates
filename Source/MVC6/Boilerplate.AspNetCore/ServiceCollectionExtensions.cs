namespace Boilerplate.AspNetCore
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Executes the specified action if the specified <paramref name="condition"/> is <c>true</c> which can be
        /// used to conditionally configure the MVC services.
        /// </summary>
        /// <param name="services">The MVC services.</param>
        /// <param name="condition">If set to <c>true</c> the action is executed.</param>
        /// <param name="action">The action used to configure the MVC services.</param>
        /// <returns>The MVC services.</returns>
        public static IServiceCollection AddIf(
            this IServiceCollection services,
            bool condition,
            Func<IServiceCollection, IServiceCollection> action)
        {
            if (condition)
            {
                services = action(services);
            }

            return services;
        }

        /// <summary>
        /// Executes the specified <paramref name="ifAction"/> if the specified <paramref name="condition"/> is
        /// <c>true</c>, otherwise executes the <paramref name="elseAction"/>. This can be used to conditionally
        /// configure the MVC services.
        /// </summary>
        /// <param name="services">The MVC services.</param>
        /// <param name="condition">If set to <c>true</c> the <paramref name="ifAction"/> is executed, otherwise the
        /// <paramref name="elseAction"/> is executed.</param>
        /// <param name="ifAction">The action used to configure the MVC services if the condition is <c>true</c>.</param>
        /// <param name="elseAction">The action used to configure the MVC services if the condition is <c>false</c>.</param>
        /// <returns>The MVC services.</returns>
        public static IServiceCollection AddIfElse(
            this IServiceCollection services,
            bool condition,
            Func<IServiceCollection, IServiceCollection> ifAction,
            Func<IServiceCollection, IServiceCollection> elseAction)
        {
            if (condition)
            {
                services = ifAction(services);
            }
            else
            {
                services = elseAction(services);
            }

            return services;
        }
    }
}
