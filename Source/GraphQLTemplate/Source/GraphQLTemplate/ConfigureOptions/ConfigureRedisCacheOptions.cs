namespace GraphQLTemplate.ConfigureOptions;

using GraphQLTemplate.Options;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;

public class ConfigureRedisCacheOptions : IConfigureOptions<RedisCacheOptions>
{
    private readonly RedisOptions redisOptions;

    public ConfigureRedisCacheOptions(RedisOptions redisOptions) =>
        this.redisOptions = redisOptions;

    public void Configure(RedisCacheOptions options) =>
        options.ConfigurationOptions = this.redisOptions.ConfigurationOptions;
}
