namespace TemplateJsonToMarkdown
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            //var filePath = args[0];
            //var filePath = @"C:\Git\Templates\Source\Boilerplate.Templates\content\ApiTemplate\.template.config\template.json";
            var filePath = @"C:\GitHub\Templates\Source\Boilerplate.Templates\Content\StaticFilesTemplate\.template.config\template.json";
            var json = File.ReadAllText(filePath);

            var stringBuilder = new StringBuilder();

            var root = JObject.Parse(json);
            var symbols = (JObject)root.Property("symbols").Value;
            foreach (var symbol in symbols
                .Properties()
                .Select(x => x.Value)
                .Cast<JObject>()
                .Where(x => string.Equals(x.Property("type").Value.ToString(), "parameter", StringComparison.OrdinalIgnoreCase)))
            {
                var name = ((JProperty)symbol.Parent).Name;
                var dataType = symbol.Property("datatype").Value.ToString();
                var defaultValue = symbol.Property("defaultValue")?.Value?.ToString();
                var description = symbol.Property("description").Value.ToString();

                switch (dataType)
                {
                    case "string":
                        stringBuilder.AppendLine($"- **{name}** - {description}");
                        break;
                    case "bool":
                        var onOff = bool.Parse(defaultValue) ? "On" : "Off";
                        stringBuilder.AppendLine($"- **{name}** (Default={onOff}) - {description}");
                        break;
                    case "choice":
                        stringBuilder.AppendLine($"- **{name}** - {description}");
                        var choices = (JArray)symbol.Property("choices").Value;
                        foreach (var choice in choices.Cast<JObject>())
                        {
                            var choiceName = choice.Property("choice").Value.ToString();
                            var choiceDescription = choice.Property("description").Value.ToString();
                            var isDefault = string.Equals(choiceName, defaultValue, StringComparison.OrdinalIgnoreCase);
                            var defaultString = isDefault ? " (Default)" : string.Empty;
                            stringBuilder.AppendLine($"  - **{choiceName}**{defaultString} - {choiceDescription}");
                        }
                        break;
                }

            }

            Console.WriteLine(stringBuilder.ToString());
            Console.Read();
        }
    }
}
