namespace ApiTemplate.ViewModels;

using System.Text.Json.Serialization;

/// <summary>
/// Enables faster serialization and de-serialization with fewer allocations by generating source code.
/// </summary>
#if Controllers
[JsonSerializable(typeof(Car[]))]
[JsonSerializable(typeof(Connection<Car>[]))]
[JsonSerializable(typeof(SaveCar[]))]
#else
[JsonSerializable(typeof(object[]))]
#endif
internal partial class CustomJsonSerializerContext : JsonSerializerContext
{
}
