using System.Text.RegularExpressions;

namespace Smusdi.RegexTools;

/// <summary>
/// Some helpers to check or to process regular expressions
/// </summary>
public static partial class RegexHelpers
{
    public static RegexValidationResult Validate(string pattern)
    {
        try
        {
            Regex.IsMatch(string.Empty, pattern);
            return new RegexValidationResult(true);
        }
        catch (Exception ex)
        {
            return new RegexValidationResult(false, ex.Message);
        }
    }

    public static ICollection<string> GetDefinedGroupNames(string pattern)
    {
        var regex = NamedGroupsFinder();
        var names = new List<string>();
        var matches = regex.Matches(pattern);
        if (matches.Count == 0)
        {
            return names;
        }

        foreach (Match match in matches)
        {
            names.AddRange(match.Groups.Values.Where(g => g.Name == "name").Select(g => g.Value));
        }

        return names;
    }

    [GeneratedRegex("(\\(\\?\\<(?<name>[^\\>]*)\\>)")]
    private static partial Regex NamedGroupsFinder();
}
