using System.Text.RegularExpressions;

namespace Smusdi.RegexTools;

/// <summary>
/// Some helpers to check or to process regular expressions.
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

    public static bool IsMatch(string pattern, string input)
    {
        try
        {
            return Regex.IsMatch(input, pattern);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static ICollection<string> GetDefinedGroupNames(string pattern)
    {
        var names = new Regex(pattern).GetGroupNames().Where(n => !int.TryParse(n, out _)).ToList();
        return names;
    }
}
