
using System.Text.Json;
using System.Text.Json.Serialization;
using static CoffeeManagement.Exceptions.ApiException;

namespace CoffeeManagement.Filter
{
    public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
    {

        private const string Format = "HH:mm";

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                if (reader.TokenType != JsonTokenType.String)
                {
                    throw new BadRequestException($"Unexpected token parsing TimeOnly. Expected String, got {reader.TokenType}.");
                }

                var timeString = reader.GetString();
                if (!TimeOnly.TryParseExact(timeString, Format, out var time))
                {
                    throw new BadRequestException($"Invalid time format. Expected format is '{Format}' (e.g., '08:30').");
                }

                return time;
            }
            catch (BadRequestException ex)
            {
                // Re-throw the BadRequestException
                throw new BadRequestException("Error occurred during TimeOnly deserialization: " + ex.Message);
            }
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }
    }
}
