namespace Boilerplate.AspNetCore
{
    using System;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// <see cref="IConfiguration"/>
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Gets a configuration sub-section with the specified key and binds it with the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the configuration section to bind to.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="key">The section key. If <c>null</c>, the name of the type <typeparamref name="T"/> is used.</param>
        /// <returns>The bound object.</returns>
        public static T GetSection<T>(this IConfiguration configuration, string key = null)
            where T : new()
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (key == null)
            {
                key = typeof(T).Name;
            }

            var section = new T();
            configuration.GetSection(key).Bind(section);
            return section;
        }
    }
}
