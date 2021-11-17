namespace GraphQLTemplate.Options;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// The caching options for the application.
/// </summary>
#pragma warning disable CA1710 // Identifiers should have correct suffix
public class CacheProfileOptions : Dictionary<string, CacheProfile>
#pragma warning restore CA1710 // Identifiers should have correct suffix
{
}
