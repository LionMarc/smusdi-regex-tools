namespace Smusdi.RegexTools;

public sealed class StringPartsExtractionResult(int expectedPartsCount, IDictionary<string, string>? errors = null, IDictionary<string, object>? extractedParts = null)
{
    public bool IsValid => this.Errors.Count == 0 && this.ExtractedParts.Count == this.ExpectedPartsCount;

    public int ExpectedPartsCount { get; } = expectedPartsCount;

    public IDictionary<string, string> Errors { get; } = errors ?? new Dictionary<string, string>();

    public IDictionary<string, object> ExtractedParts { get; } = extractedParts ?? new Dictionary<string, object>();

    public void AddError(string name, string error)
    {
        this.Errors[name] = error;
    }

    public void AddExtractedPart(string name, object value)
    {
        this.ExtractedParts[name] = value;
    }
}
