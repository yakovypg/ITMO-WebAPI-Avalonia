using System;

namespace StarWars.Infrastructure.Services
{
    public static class UrlService
    {
        public static string AddIdToUrl(string url, int id)
        {
            ArgumentNullException.ThrowIfNull(url, nameof(url));
            return Combine(url, $"{id}");
        }

        public static string AddPageToUrl(string url, int page)
        {
            ArgumentNullException.ThrowIfNull(url, nameof(url));
            return Combine(url, $"?page={page}");
        }

        public static string Combine(string head, string tail)
        {
            ArgumentNullException.ThrowIfNull(head, nameof(head));
            ArgumentNullException.ThrowIfNull(tail, nameof(tail));

            return head.Length > 0 && head[^1] == '/'
                ? $"{head}{tail}"
                : $"{head}/{tail}";
        }
    }
}
