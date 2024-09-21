using System;
using System.Collections.Generic;
using System.Globalization;

namespace StarWars.Infrastructure.Services
{
    public static class ValuePresentationService
    {
        public static string ToStringOrNoData(object? value)
        {
            return value?.ToString() ?? "N/D";
        }

        public static string EnumerableToString<T>(IEnumerable<T>? enumerable)
        {
            return string.Join(", ", enumerable ?? Array.Empty<T>());
        }

        public static string GetPresenter(object? value)
        {
            return ToStringOrNoData(value);
        }

        public static string GetPresenter(string? value)
        {
            return string.IsNullOrEmpty(value)
                ? ToStringOrNoData(null)
                : value;
        }

        public static string GetPresenter(DateTime? value)
        {
            return value is not null
                ? value.Value.ToString(CultureInfo.InvariantCulture)
                : ToStringOrNoData(value);
        }

        public static string GetPresenter<T>(IEnumerable<T>? value)
        {
            return EnumerableToString(value);
        }
    }
}
