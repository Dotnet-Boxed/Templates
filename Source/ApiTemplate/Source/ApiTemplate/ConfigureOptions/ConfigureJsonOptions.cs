namespace ApiTemplate.ConfigureOptions;

using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

public class ConfigureJsonOptions : IConfigureOptions<JsonOptions>
{
    private readonly IWebHostEnvironment webHostEnvironment;

    public ConfigureJsonOptions(IWebHostEnvironment webHostEnvironment) =>
        this.webHostEnvironment = webHostEnvironment;

    public void Configure(JsonOptions options)
    {
        var jsonSerializerOptions = options.JsonSerializerOptions;
        jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        jsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;

        // Pretty print the JSON in development for easier debugging.
        jsonSerializerOptions.WriteIndented = this.webHostEnvironment.IsDevelopment() ||
            this.webHostEnvironment.IsEnvironment(Constants.EnvironmentName.Test);
#if Controllers

        jsonSerializerOptions.AddContext<AppJsonSerializerContext>();
#endif
    }
}
