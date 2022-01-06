namespace GraphQLTemplate.ConfigureOptions;

using GraphQLTemplate.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

public class ConfigureAuthorizationOptions : IConfigureOptions<AuthorizationOptions>
{
    public void Configure(AuthorizationOptions options) =>
        options.AddPolicy(AuthorizationPolicyName.Admin, x => x.RequireAuthenticatedUser());
}
