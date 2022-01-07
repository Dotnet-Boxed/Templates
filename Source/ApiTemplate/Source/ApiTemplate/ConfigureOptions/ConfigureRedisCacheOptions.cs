namespace ApiTemplate.ConfigureOptions;

using ApiTemplate.Options;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;

/// <summary>
/// Configures Redis based distributed caching for the application.
/// </summary>
public class ConfigureRedisCacheOptions : IConfigureOptions<RedisCacheOptions>
{
    private readonly RedisOptions redisOptions;

    public ConfigureRedisCacheOptions(RedisOptions redisOptions) =>
        this.redisOptions = redisOptions;

    public void Configure(RedisCacheOptions options) =>
        options.ConfigurationOptions = this.redisOptions.ConfigurationOptions;
}
