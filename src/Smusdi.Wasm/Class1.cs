using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation.Results;
using Smusdi.RegexTools;

namespace Smusdi.Wasm;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(RegexValidationResult))]
[JsonSerializable(typeof(StringPartsExtractor))]
[JsonSerializable(typeof(ValidationResult))]
public partial class SmusdiSerializationContext : JsonSerializerContext
{
}

public partial class regexToolsAppi
{
    [JSExport]
    public static string ValidateStringPartsExtractor(string input)
    {
        var extractor = JsonSerializer.Deserialize<StringPartsExtractor>(input);
        var validationResult = extractor.Validate();
        return JsonSerializer.Serialize(validationResult);
    }
}
