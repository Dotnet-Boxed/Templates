namespace GraphQLTemplate.Options;

using System.ComponentModel.DataAnnotations;
using StackExchange.Redis;

public class RedisOptions
{
    [Required]
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
