namespace MvcBoilerplate
{
    public partial class Startup
    {
        /// <summary>
        /// Configures the settings by binding the contents of the config.json file to the specified Plain Old CLR 
        /// Objects (POCO) and adding <see cref="IOptions{}"/> objects to the services collection.
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        private static void ConfigureOptions(IServiceCollection services)
        {
            // Adds IOptions<AppSettings> to the services container.
            services.Configure<AppSettings>(this.Configuration.GetConfigurationSection(nameof(AppSettings)));
        }
    }
}