using System.Text;

namespace Smusdi.RegexTools;

public sealed class StringPartsExtractorValidator
{
    private static readonly string ExpressionPropertyName = nameof(StringPartsExtractor.Expression).ToCamelCase();
    private static readonly string PartsPropertyName = nameof(StringPartsExtractor.Parts).ToCamelCase();

    private readonly List<Action<StringPartsExtractor, StringPartsExtractorValidationResult>> validators;

    private readonly List<string> extractedGroupNames = new();

    public StringPartsExtractorValidator()
    {
        this.validators = new()
        {
            MustNotBeEmpty,
            MustBeValidRegex,
            this.MustHaveDefinedGroupNames,
            this.MustDeclaredPartsBeSameAsDefinedOnes,
            MustEachDefinedPartBeValid,
        };
    }

    public StringPartsExtractorValidationResult Validate(StringPartsExtractor extractor)
    {
        var validationResult = new StringPartsExtractorValidationResult();
        foreach (var validator in this.validators)
        {
            validator(extractor, validationResult);
            if (!validationResult.IsValid)
            {
                break;
            }
        }

        return validationResult;
    }

    private static void MustNotBeEmpty(StringPartsExtractor extractor, StringPartsExtractorValidationResult validationResult)
    {
        if (string.IsNullOrWhiteSpace(extractor.Expression))
        {
            validationResult.AddError(ExpressionPropertyName, $"{ExpressionPropertyName} must be set.");
        }
    }

    private static void MustBeValidRegex(StringPartsExtractor extractor, StringPartsExtractorValidationResult validationResult)
    {
        var regexValidationResult = RegexHelpers.Validate(extractor.Expression);
        if (!regexValidationResult.IsValid)
        {
            validationResult.AddError(ExpressionPropertyName, regexValidationResult.Error ?? string.Empty);
        }
    }

    private static void MustEachDefinedPartBeValid(StringPartsExtractor extractor, StringPartsExtractorValidationResult validationResult)
    {
        var index = 0;
        foreach (var part in extractor.Parts)
        {
            if (part.Type == ExtractedPartType.Date && string.IsNullOrWhiteSpace(part.Format))
            {
                validationResult.AddError($"{PartsPropertyName}[{index}]", $"For part of type '{ExtractedPartType.Date}', the format must be set.");
            }

            index++;
        }
    }

    private void MustHaveDefinedGroupNames(StringPartsExtractor extractor, StringPartsExtractorValidationResult validationResult)
    {
        var groups = RegexHelpers.GetDefinedGroupNames(extractor.Expression);
        if (groups.Count == 0)
        {
            validationResult.AddError(ExpressionPropertyName, "At least one named group must be set.");
        }

        this.extractedGroupNames.AddRange(groups);
    }

    private void MustDeclaredPartsBeSameAsDefinedOnes(StringPartsExtractor extractor, StringPartsExtractorValidationResult validationResult)
    {
        var declaredNames = new HashSet<string>(extractor.Parts.Select(p => p.Name));
        var notSet = new List<string>();
        foreach (var found in this.extractedGroupNames)
        {
            if (declaredNames.Contains(found))
            {
                declaredNames.Remove(found);
            }
            else
            {
                notSet.Add(found);
            }
        }

        var messageBuilder = new StringBuilder();
        if (notSet.Count > 0)
        {
            messageBuilder.Append($"Not found parts: {string.Join(',', notSet)}.");
        }

        if (declaredNames.Count > 0)
        {
            messageBuilder.Append($"Missing parts: {string.Join(',', declaredNames)}.");
        }

        if (messageBuilder.Length > 0)
        {
            validationResult.AddError(PartsPropertyName, messageBuilder.ToString());
        }
    }
}
