namespace ApiTemplate.Options
{
    using StackExchange.Redis;

    public class RedisOptions
    {
        public string? ConnectionString { get; set; }

        public ConfigurationOptions ConfigurationOptions
        {
            get
            {
                var options = ConfigurationOptions.Parse(this.ConnectionString);
                options.ClientName = AssemblyInformation.Current.Product;
                return options;
            }
        }
    }
}
