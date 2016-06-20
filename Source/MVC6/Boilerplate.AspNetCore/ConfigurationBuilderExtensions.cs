namespace Boilerplate.AspNetCore
{
    using System;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// <see cref="IConfigurationBuilder"/> extension methods
    /// </summary>
    public static class ConfigurationBuilderExtensions
    {
        /// <summary>
        /// Executes the specified action if the specified <paramref name="condition"/> is <c>true</c> which can be
        /// used to conditionally add to the configuration pipeline.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <param name="condition">If set to <c>true</c> the action is executed.</param>
        /// <param name="action">The action used to add to the request execution pipeline.</param>
        /// <returns>The configuration builder.</returns>
        public static IConfigurationBuilder AddIf(
            this IConfigurationBuilder configurationBuilder,
            bool condition,
            Func<IConfigurationBuilder, IConfigurationBuilder> action)
        {
            if (condition)
            {
                configurationBuilder = action(configurationBuilder);
            }

            return configurationBuilder;
        }

        /// <summary>
        /// Executes the specified <paramref name="ifAction"/> if the specified <paramref name="condition"/> is
        /// <c>true</c>, otherwise executes the <paramref name="elseAction"/>. This can be used to conditionally add to
        /// the configuration pipeline.
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder.</param>
        /// <param name="condition">If set to <c>true</c> the <paramref name="ifAction"/> is executed, otherwise the
        /// <paramref name="elseAction"/> is executed.</param>
        /// <param name="ifAction">The action used to add to the configuration pipeline if the condition is
        /// <c>true</c>.</param>
        /// <param name="elseAction">The action used to add to the configuration pipeline if the condition is
        /// <c>false</c>.</param>
        /// <returns>The configuration builder.</returns>
        public static IConfigurationBuilder AddIfElse(
            this IConfigurationBuilder configurationBuilder,
            bool condition,
            Func<IConfigurationBuilder, IConfigurationBuilder> ifAction,
            Func<IConfigurationBuilder, IConfigurationBuilder> elseAction)
        {
            if (condition)
            {
                configurationBuilder = ifAction(configurationBuilder);
            }
            else
            {
                configurationBuilder = elseAction(configurationBuilder);
            }

            return configurationBuilder;
        }
    }
}
