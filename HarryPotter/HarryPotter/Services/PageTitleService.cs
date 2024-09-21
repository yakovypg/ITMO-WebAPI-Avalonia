using System.Text.RegularExpressions;

namespace HarryPotter.Services;

public static partial class PageTitleService
{
    public static string GetTitle(object? page)
    {
        if (page is null)
            return string.Empty;

        string fullName = page.GetType().Name;

        string name = fullName
            .Replace("Page", string.Empty)
            .Replace("View", string.Empty);

        return SplitCamelCaseRegex().Replace(name, " $1").Trim();
    }

    [GeneratedRegex("([A-Z])", RegexOptions.Compiled)]
    private static partial Regex SplitCamelCaseRegex();
}
