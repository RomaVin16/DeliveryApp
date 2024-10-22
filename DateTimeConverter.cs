using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DateTimeConverter : JsonConverter<DateTime>
{
    private readonly string _dateTimeFormat;

    public DateTimeConverter(string dateTimeFormat)
    {
        _dateTimeFormat = dateTimeFormat;
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String) throw new JsonException();
        var dateTimeString = reader.GetString();

        return DateTime.ParseExact(dateTimeString, _dateTimeFormat, null);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_dateTimeFormat));
    }
}