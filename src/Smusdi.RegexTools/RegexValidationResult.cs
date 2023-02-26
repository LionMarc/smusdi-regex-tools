using FluentValidation;
using FluentValidation.Results;

namespace Smusdi.RegexTools;

public sealed class RegexValidationResult
{
    public RegexValidationResult(bool isValid, string? error = null)
    {
        this.IsValid = isValid;
        this.Error = error;
    }

    public bool IsValid { get; }

    public string? Error { get; }
}
