using System.Text.RegularExpressions;

namespace Smusdi.RegexTools;

public sealed class StringPartsExtractor
{
    private readonly Lazy<Regex> expressionRegex;
    private readonly List<ExtractedPart> parts;

    public StringPartsExtractor(string expression, IEnumerable<ExtractedPart> parts)
    {
        this.Expression = expression;
        this.parts = [.. parts ?? []];
        this.expressionRegex = new Lazy<Regex>(() => new Regex(this.Expression));
    }

    public string Expression { get; }

    public IEnumerable<ExtractedPart> Parts => this.parts;

    public StringPartsExtractorValidationResult Validate()
    {
        var vaildator = new StringPartsExtractorValidator();
        return vaildator.Validate(this);
    }

    public StringPartsExtractionResult ExtractPartsFrom(string input)
    {
        var result = new StringPartsExtractionResult(this.parts.Count);
        var match = this.expressionRegex.Value.Match(input);
        if (match.Success)
        {
            this.parts.ForEach(part =>
            {
                try
                {
                    var extractedPart = part.ExtractPart(match);
                    result.AddExtractedPart(part.Name, extractedPart);
                }
                catch (Exception ex)
                {
                    result.AddError(part.Name, ex.Message);
                }
            });
        }

        return result;
    }
}
