using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

namespace HarryPotter.Infrastructure.Converters;

public class HarryPotterDateTimeConverter : IsoDateTimeConverter
{
    public HarryPotterDateTimeConverter()
    {
        DateTimeFormat = "dd-MM-yyyy";
    }

    public override object? ReadJson(
        JsonReader reader,
        Type objectType,
        object? existingValue,
        JsonSerializer serializer)
    {
        try
        {
            string? value = reader.Value as string;

            if (string.IsNullOrEmpty(value))
                return null;

            IFormatProvider formatProvider = CultureInfo.InvariantCulture;

            return DateTime.ParseExact(
                value,
                DateTimeFormat!,
                formatProvider);
        }
        catch
        {
            return null;
        }
    }
}
