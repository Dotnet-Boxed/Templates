namespace GraphQLTemplate.ConfigureOptions;

using GraphQLTemplate.Constants;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;

public class ConfigureCorsOptions : IConfigureOptions<CorsOptions>
{
    public void Configure(CorsOptions options) =>
        // Create named CORS policies here which you can consume using application.UseCors("PolicyName")
        // or a [EnableCors("PolicyName")] attribute on your controller or action.
        options.AddPolicy(
            CorsPolicyName.AllowAny,
            x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
}
