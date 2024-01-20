using System.Text.Json.Serialization;

namespace Smusdi.RegexTools;

[JsonConverter(typeof(JsonStringEnumConverter<ExtractedPartType>))]
public enum ExtractedPartType
{
    Text,
    Date,
    Time,
    Number,
}
