using System.Text;
using FluentValidation;
using FluentValidation.Results;

namespace Smusdi.RegexTools;

public sealed class StringPartsExtractor
{
    public StringPartsExtractor(string expression, IEnumerable<ExtractedPart> parts)
    {
        this.Expression = expression;
        this.Parts = parts;
    }

    public string Expression { get; }

    public IEnumerable<ExtractedPart> Parts { get; }

    public ValidationResult Validate()
    {
        var validator = new StringPartsExtractorValidator();
        return validator.Validate(this);
    }
}

public sealed class StringPartsExtractorValidator : AbstractValidator<StringPartsExtractor>
{
    public StringPartsExtractorValidator()
    {
        this.RuleFor(e => e.Expression)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage($"{nameof(StringPartsExtractor.Expression).ToCamelCase()} must be set.")
            .Must((_, expression, context) =>
            {
                var validationResult = RegexHelpers.Validate(expression);
                if (!validationResult.IsValid)
                {
                    context.MessageFormatter.AppendArgument("regexError", validationResult.Error);
                    return false;
                }

                var groups = RegexHelpers.GetDefinedGroupNames(expression);
                if (groups.Count == 0)
                {
                    context.MessageFormatter.AppendArgument("regexError", "At least one named group must be set.");
                    return false;
                }

                context.RootContextData["ExtractedGroupBames"] = groups;
                return true;
            })
            .WithMessage("{regexError}")
            .OverridePropertyName(nameof(StringPartsExtractor.Expression).ToCamelCase())
            .DependentRules(() =>
            {
                this.RuleFor(e => e.Parts)
                    .Must((e, p, context) =>
                    {
                        var declaredNames = new HashSet<string>(p.Select(p => p.Name));
                        var notSet = new List<string>();
                        foreach (var found in context.RootContextData["ExtractedGroupBames"] as List<string> ?? new List<string>())
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
                            context.MessageFormatter.AppendArgument("partsError", messageBuilder.ToString());
                            return false;
                        }

                        return true;
                    })
                    .WithMessage("{partsError}")
                    .OverridePropertyName(nameof(StringPartsExtractor.Parts).ToCamelCase())
                    .DependentRules(() =>
                    {
                        this.RuleForEach(e => e.Parts)
                            .ChildRules(part =>
                            {
                                part.RuleFor(p => p.Format).NotEmpty().When(p => p.Type == ExtractedPartType.Date);
                            });
                    });
            });
    }
}
