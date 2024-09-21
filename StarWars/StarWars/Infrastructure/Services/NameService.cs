using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StarWars.Infrastructure.Services
{
    public static partial class NameService
    {
        public static string RemoveDoubleSpaces(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            while (text.Contains("  "))
            {
                text = text.Replace("  ", " ");
            }

            return text;
        }

        public static string SplitCamelCase(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            
            IEnumerable<string> convertedText = UppercaseRegex().Split(text).Select((t, i) =>
            {
                if (i == 0 || string.IsNullOrEmpty(t))
                    return t;

                char firstChar = char.ToLower(t[0]);
                return firstChar + t[1..];
            });

            return string.Join(' ', convertedText);
        }

        [GeneratedRegex(@"(?<!^)(?=[A-Z])")]
        private static partial Regex UppercaseRegex();
    }
}
