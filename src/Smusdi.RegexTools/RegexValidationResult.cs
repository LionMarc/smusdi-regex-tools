namespace Smusdi.RegexTools;

public sealed class RegexValidationResult(bool isValid, string? error = null)
{
    public bool IsValid { get; } = isValid;

    public string? Error { get; } = error;
}
