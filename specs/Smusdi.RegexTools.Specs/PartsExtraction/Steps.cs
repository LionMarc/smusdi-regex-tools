using System.Text.Json;
using System.Text.Json.JsonDiffPatch;
using System.Text.Json.Nodes;
using AwesomeAssertions;
using Reqnroll;

namespace Smusdi.RegexTools.Specs.PartsExtraction;

[Binding]
public class Steps
{
    private StringPartsExtractorValidationResult? validationResult;
    private StringPartsExtractor? extractor;
    private StringPartsExtractionResult? extracted;

    [Given(@"the string part extractor with expression ""(.*)"" and parts")]
    public void GivenTheStringPartExtractorWithExpressionAndParts(string expression, Table table)
    {
        this.extractor = new StringPartsExtractor(expression, table.CreateSet<ExtractedPart>());
    }

    [When(@"I validate an extractor with expression ""([^""]*)"" and parts")]
    public void WhenIValidateAnExtractorWithExpressionAndParts(string expression, Table table)
    {
        this.extractor = new StringPartsExtractor(expression, table.CreateSet<ExtractedPart>());
        this.validationResult = this.extractor.Validate();
    }

    [When(@"I extract parts from string ""(.*)""")]
    public void WhenIExtractPartsFromString(string testString)
    {
        this.extracted = this.extractor?.ExtractPartsFrom(testString);
    }

    [Then(@"I get the validation errors")]
    public void ThenIGetTheValidationErrors(string multilineText)
    {
        this.validationResult.Should().NotBeNull();
        this.validationResult?.IsValid.Should().BeFalse();

        var expectedErrors = JsonSerializer.Deserialize<IDictionary<string, List<string>>>(multilineText);
        this.validationResult?.Errors.Should().BeEquivalentTo(expectedErrors);
    }

    [Then(@"I get the extraction result")]
    public void ThenIGetTheExtractionResult(string multilineText)
    {
        var expected = JsonNode.Parse(multilineText);
        var current = JsonNode.Parse(JsonSerializer.Serialize(this.extracted));
        var diff = current.Diff(expected);
        diff.Should().BeNull();
    }
}
