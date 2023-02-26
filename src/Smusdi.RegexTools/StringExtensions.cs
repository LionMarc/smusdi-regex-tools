using System.Text.Json;

namespace Smusdi.RegexTools;

public static class StringExtensions
{
    public static string ToCamelCase(this string value) => JsonNamingPolicy.CamelCase.ConvertName(value);
}
