using System.Text.Json;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Smusdi.RegexTools.Specs.PartsExtraction;

[Binding]
public class Steps
{
    private StringPartsExtractorValidationResult? validationResult;

    [When(@"I validate an extractor with expression ""([^""]*)"" and parts")]
    public void WhenIValidateAnExtractorWithExpressionAndParts(string expression, Table table)
    {
        var extractor = new StringPartsExtractor(expression, table.CreateSet<ExtractedPart>());
        this.validationResult = extractor.Validate();
    }

    [Then(@"I get the validation errors")]
    public void ThenIGetTheValidationErrors(string multilineText)
    {
        this.validationResult.Should().NotBeNull();
        this.validationResult?.IsValid.Should().BeFalse();

        var expectedErrors = JsonSerializer.Deserialize<IDictionary<string, List<string>>>(multilineText);
        this.validationResult?.Errors.Should().BeEquivalentTo(expectedErrors);
    }
}
