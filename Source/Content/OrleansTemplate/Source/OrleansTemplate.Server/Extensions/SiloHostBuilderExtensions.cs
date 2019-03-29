namespace OrleansTemplate.Server
{
    using System;
    using Orleans.Hosting;

    public static class SiloHostBuilderExtensions
    {
        /// <summary>
        /// Executes the specified action if the specified <paramref name="condition"/> is <c>true</c> which can be
        /// used to conditionally add to the host builder.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <param name="condition">If set to <c>true</c> the action is executed.</param>
        /// <param name="action">The action used to add to the host builder.</param>
        /// <returns>The same host builder.</returns>
        public static ISiloHostBuilder UseIf(
            this ISiloHostBuilder hostBuilder,
            bool condition,
            Func<ISiloHostBuilder, ISiloHostBuilder> action)
        {
            if (hostBuilder == null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            if (action == null)
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
        public static ISiloHostBuilder UseIf(
            this ISiloHostBuilder hostBuilder,
            Func<ISiloHostBuilder, bool> condition,
            Func<ISiloHostBuilder, ISiloHostBuilder> action)
        {
            if (hostBuilder == null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (condition(hostBuilder))
            {
                hostBuilder = action(hostBuilder);
            }

            return hostBuilder;
        }
    }
}
