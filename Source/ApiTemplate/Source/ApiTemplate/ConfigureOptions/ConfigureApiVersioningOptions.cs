namespace ApiTemplate.ConfigureOptions;

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;

public class ConfigureApiVersioningOptions :
    IConfigureOptions<ApiVersioningOptions>,
    IConfigureOptions<ApiExplorerOptions>
{
    public void Configure(ApiVersioningOptions options)
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    }

    public void Configure(ApiExplorerOptions options) =>
        // Version format: 'v'major[.minor][-status]
        options.GroupNameFormat = "'v'VVV";
}
