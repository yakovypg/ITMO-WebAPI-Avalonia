using System;
using System.Collections.Generic;
using System.Linq;

namespace IpManager.Server.Services;

public static class RequestService
{
    public static Dictionary<string, string> ExtractParametersFromUrl(string url)
    {
        if (string.IsNullOrEmpty(url))
            return [];

        int dataStartIndex = url.IndexOf('?') + 1;

        if (dataStartIndex <= 0 || dataStartIndex == url.Length)
            return [];

        string data = url[dataStartIndex..];
        string[] pairsData = data.Split('&');

        IEnumerable<(string Key, string Value)> pairs = pairsData.Select(t =>
        {
            string[] values = t.Split('=');

            return values.Length == 2
                ? (values[0], values[1])
                : throw new ArgumentException("Parameters have incorrect format", nameof(url));
        });

        return pairs.ToDictionary(t => t.Key, t => t.Value);
    }
}
