namespace Smusdi.RegexTools;

public sealed class StringPartsExtractor
{
    public StringPartsExtractor(string expression, IEnumerable<ExtractedPart> parts)
    {
        this.Expression = expression;
        this.Parts = parts ?? new List<ExtractedPart>();
    }

    public string Expression { get; }

    public IEnumerable<ExtractedPart> Parts { get; }

    public StringPartsExtractorValidationResult Validate()
    {
        var vaildator = new StringPartsExtractorValidator();
        return vaildator.Validate(this);
    }
}
