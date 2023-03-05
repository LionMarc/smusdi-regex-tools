namespace Smusdi.RegexTools;

public sealed class StringPartsExtractionResult
{
    public StringPartsExtractionResult(int expectedPartsCount, IDictionary<string, string>? errors = null, IDictionary<string, object>? extractedParts = null)
    {
        this.ExpectedPartsCount = expectedPartsCount;
        this.Errors = errors ?? new Dictionary<string, string>();
        this.ExtractedParts = extractedParts ?? new Dictionary<string, object>();
    }

    public bool IsValid => this.Errors.Count == 0 && this.ExtractedParts.Count == this.ExpectedPartsCount;

    public int ExpectedPartsCount { get; }

    public IDictionary<string, string> Errors { get; }

    public IDictionary<string, object> ExtractedParts { get; }

    public void AddError(string name, string error)
    {
        this.Errors[name] = error;
    }

    public void AddExtractedPart(string name, object value)
    {
        this.ExtractedParts[name] = value;
    }
}
