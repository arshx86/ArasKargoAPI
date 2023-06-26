using System;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

namespace ArasKargoAPI
{
    public class DateTimeConverter : JsonConverter<DateTime?>
    {
        private readonly string format = "dd.MM.yyyy HH:mm:ss";

        public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            if (reader.TokenType == JsonToken.String && reader.Value is string dateStr)
            {
                if (DateTime.TryParseExact(dateStr, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                    return parsedDate;
            }

            // handle
            return null;
        }

        public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
        {
            if (value.HasValue)
                writer.WriteValue(value.Value.ToString(format));
            else
                writer.WriteNull();
        }
    }
}