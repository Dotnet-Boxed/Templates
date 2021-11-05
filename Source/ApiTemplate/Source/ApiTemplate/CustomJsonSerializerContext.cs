namespace ApiTemplate
{
    using System.Text.Json.Serialization;
    using ApiTemplate.ViewModels;

    /// <summary>
    /// Enables faster serialization and de-serialization with fewer allocations by generating source code.
    /// </summary>
    [JsonSerializable(typeof(Car[]))]
    [JsonSerializable(typeof(Connection<Car>[]))]
    [JsonSerializable(typeof(SaveCar[]))]
    public partial class CustomJsonSerializerContext : JsonSerializerContext
    {
    }
}
