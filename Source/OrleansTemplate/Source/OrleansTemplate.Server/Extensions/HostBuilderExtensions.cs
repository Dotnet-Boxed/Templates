namespace OrleansTemplate.Server
{
    using System;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// <see cref="IHostBuilder"/> extension methods.
    /// </summary>
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Executes the specified action if the specified <paramref name="condition"/> is <c>true</c> which can be
        /// used to conditionally add to the host builder.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <param name="condition">If set to <c>true</c> the action is executed.</param>
        /// <param name="action">The action used to add to the host builder.</param>
        /// <returns>The same host builder.</returns>
        public static IHostBuilder UseIf(
            this IHostBuilder hostBuilder,
            bool condition,
            Func<IHostBuilder, IHostBuilder> action)
        {
            if (hostBuilder is null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (condition)
            {
                hostBuilder = action(hostBuilder);
            }

            return hostBuilder;
        }

        /// <summary>
        /// Executes the specified action if the specified <paramref name="condition"/> is <c>true</c> which can be
        /// used to conditionally add to the host builder.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <param name="condition">If <c>true</c> is returned the action is executed.</param>
        /// <param name="action">The action used to add to the host builder.</param>
        /// <returns>The same host builder.</returns>
        public static IHostBuilder UseIf(
            this IHostBuilder hostBuilder,
            Func<IHostBuilder, bool> condition,
            Func<IHostBuilder, IHostBuilder> action)
        {
            if (hostBuilder is null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (condition(hostBuilder))
            {
                hostBuilder = action(hostBuilder);
            }

            return hostBuilder;
        }

        /// <summary>
        /// Executes the specified <paramref name="ifAction"/> if the specified <paramref name="condition"/> is
        /// <c>true</c>, otherwise executes the <paramref name="elseAction"/>. This can be used to conditionally add to
        /// the host builder.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <param name="condition">If set to <c>true</c> the <paramref name="ifAction"/> is executed, otherwise the
        /// <paramref name="elseAction"/> is executed.</param>
        /// <param name="ifAction">The action used to add to the host builder if the condition is <c>true</c>.</param>
        /// <param name="elseAction">The action used to add to the host builder if the condition is <c>false</c>.</param>
        /// <returns>The same host builder.</returns>
        public static IHostBuilder UseIfElse(
            this IHostBuilder hostBuilder,
            bool condition,
            Func<IHostBuilder, IHostBuilder> ifAction,
            Func<IHostBuilder, IHostBuilder> elseAction)
        {
            if (hostBuilder is null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            if (ifAction is null)
            {
                throw new ArgumentNullException(nameof(ifAction));
            }

            if (elseAction is null)
            {
                throw new ArgumentNullException(nameof(elseAction));
            }

            if (condition)
            {
                hostBuilder = ifAction(hostBuilder);
            }
            else
            {
                hostBuilder = elseAction(hostBuilder);
            }

            return hostBuilder;
        }

        /// <summary>
        /// Executes the specified <paramref name="ifAction"/> if the specified <paramref name="condition"/> is
        /// <c>true</c>, otherwise executes the <paramref name="elseAction"/>. This can be used to conditionally add to
        /// the host builder.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <param name="condition">If <c>true</c> is returned the <paramref name="ifAction"/> is executed, otherwise the
        /// <paramref name="elseAction"/> is executed.</param>
        /// <param name="ifAction">The action used to add to the host builder if the condition is <c>true</c>.</param>
        /// <param name="elseAction">The action used to add to the host builder if the condition is <c>false</c>.</param>
        /// <returns>The same host builder.</returns>
        public static IHostBuilder UseIfElse(
            this IHostBuilder hostBuilder,
            Func<IHostBuilder, bool> condition,
            Func<IHostBuilder, IHostBuilder> ifAction,
            Func<IHostBuilder, IHostBuilder> elseAction)
        {
            if (hostBuilder is null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (ifAction is null)
            {
                throw new ArgumentNullException(nameof(ifAction));
            }

            if (elseAction is null)
            {
                throw new ArgumentNullException(nameof(elseAction));
            }

            if (condition(hostBuilder))
            {
                hostBuilder = ifAction(hostBuilder);
            }
            else
            {
                hostBuilder = elseAction(hostBuilder);
            }

            return hostBuilder;
        }
    }
}
