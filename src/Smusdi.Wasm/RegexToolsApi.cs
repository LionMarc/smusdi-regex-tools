using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json;

namespace Smusdi.Wasm;

/// <summary>
/// Defines the list of methods that can be called from javascript.
/// </summary>
[SupportedOSPlatformAttribute("browser")]
public partial class RegexToolsApi
{
    [JSExport]
    public static string ValidateStringPartsExtractor(string input)
    {
        var extractor = JsonSerializer.Deserialize(input, SmusdiSerializationContext.Default.StringPartsExtractor);
        if (extractor == null)
        {
            throw new ArgumentException("Invalid input extractor", nameof(input));
        }

        var validationResult = extractor.Validate();
        return JsonSerializer.Serialize(validationResult, SmusdiSerializationContext.Default.StringPartsExtractorValidationResult);
    }
}
