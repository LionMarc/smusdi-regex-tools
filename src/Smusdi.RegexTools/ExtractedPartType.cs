using System.Text.Json.Serialization;

namespace Smusdi.RegexTools;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ExtractedPartType
{
    Text,
    Date,
    Time,
    Number,
}
