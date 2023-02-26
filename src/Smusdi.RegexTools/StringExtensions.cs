using System.Text.Json;
using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;

namespace Smusdi.RegexTools;

public static class StringExtensions
{
    public static string ToCamelCase(this string value) => JsonNamingPolicy.CamelCase.ConvertName(value);
}
