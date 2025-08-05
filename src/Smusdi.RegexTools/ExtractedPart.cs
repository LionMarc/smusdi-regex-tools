using System.Globalization;
using System.Text.RegularExpressions;

namespace Smusdi.RegexTools;

public sealed class ExtractedPart
{
    public ExtractedPart(string name, ExtractedPartType type, string? format)
    {
        this.Name = name;
        this.Type = type;
        this.Format = format;
    }

    public string Name { get; }

    public ExtractedPartType Type { get; }

    public string? Format { get; }

    public object ExtractPart(Match match)
    {
        var value = match.Groups[this.Name].Value;

        return this.Type switch
        {
            ExtractedPartType.Text => value,
            ExtractedPartType.Number => double.Parse(value, CultureInfo.InvariantCulture),
            ExtractedPartType.Date => DateOnly.ParseExact(value, this.Format!, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces),
            ExtractedPartType.Time => TimeOnly.Parse(value, CultureInfo.InvariantCulture),
            _ => value,
        };
    }
}
