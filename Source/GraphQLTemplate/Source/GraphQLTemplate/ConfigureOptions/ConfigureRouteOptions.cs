namespace GraphQLTemplate.ConfigureOptions;

using Microsoft.Extensions.Options;

public class ConfigureRouteOptions : IConfigureOptions<RouteOptions>
{
    public void Configure(RouteOptions options) => options.LowercaseUrls = true;
}
