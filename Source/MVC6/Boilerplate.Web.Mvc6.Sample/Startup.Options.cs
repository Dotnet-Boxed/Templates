namespace MvcBoilerplate
{
    using Microsoft.Framework.Configuration;
    using Microsoft.Framework.DependencyInjection;

    public partial class Startup
    {
        /// <summary>
        /// Configures the settings by binding the contents of the config.json file to the specified Plain Old CLR 
        /// Objects (POCO) and adding <see cref="IOptions{}"/> objects to the services collection.
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        private static void ConfigureOptions(IServiceCollection services, IConfiguration configuration)
        {
            // Adds IOptions<AppSettings> to the services container.
            services.Configure<AppSettings>(configuration.GetConfigurationSection(nameof(AppSettings)));
        }
    }
}