namespace Smusdi.RegexTools;

public sealed class StringPartsExtractorValidationResult(bool isValid = true)
{
    private readonly Dictionary<string, List<string>> errors = [];

    public bool IsValid { get; private set; } = isValid;

    public IDictionary<string, List<string>> Errors => this.errors;

    public void AddError(string propertyName, string error)
    {
        if (!this.errors.TryGetValue(propertyName, out var list))
        {
            list = [];
            this.errors[propertyName] = list;
        }

        list.Add(error);
        this.IsValid = false;
    }
}
