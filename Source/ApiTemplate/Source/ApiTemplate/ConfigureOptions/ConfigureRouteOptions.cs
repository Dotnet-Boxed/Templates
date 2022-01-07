namespace ApiTemplate.ConfigureOptions;

using Microsoft.Extensions.Options;

/// <summary>
/// Configures custom routing settings which determines how URL's are generated.
/// </summary>
public class ConfigureRouteOptions : IConfigureOptions<RouteOptions>
{
    public void Configure(RouteOptions options) => options.LowercaseUrls = true;
}
