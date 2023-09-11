namespace OrleansTemplate.Server;

using System.Text.Json.Serialization;

/// <summary>
/// Enables faster serialization and de-serialization with fewer allocations by generating source code.
/// </summary>
[JsonSerializable(typeof(object))]
internal sealed partial class AppJsonSerializerContext : JsonSerializerContext
{
}
