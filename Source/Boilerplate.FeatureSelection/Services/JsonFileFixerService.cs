namespace Boilerplate.FeatureSelection.Services
{
    using System.Text;
    using System.Text.RegularExpressions;

    public class JsonFileFixerService : IFileFixerService
    {
        private static Regex CloseBraceCommaCloseBrace = new Regex("},(\\s)+}", RegexOptions.Compiled | RegexOptions.Multiline);
        private static Regex DoubleQuoteCommaCloseBrace = new Regex("\",(\\s)+}", RegexOptions.Compiled | RegexOptions.Multiline);

        public string[] FileExtensions { get; } = new string[] { ".json" };

        public string Fix(string content)
        {
            var stringBuilder = new StringBuilder(content);

            RemoveComma(stringBuilder, CloseBraceCommaCloseBrace);
            RemoveComma(stringBuilder, DoubleQuoteCommaCloseBrace);

            return stringBuilder.ToString();
        }

        private static void RemoveComma(StringBuilder stringBuilder, Regex regex)
        {
            var match = regex.Match(stringBuilder.ToString());
            while (match.Success)
            {
                var index = match.Index + match.Value.IndexOf(',');
                stringBuilder.Remove(index, 1);

                match = regex.Match(stringBuilder.ToString());
            }
        }
    }
}
