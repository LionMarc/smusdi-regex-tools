namespace Smusdi.RegexTools;

public sealed class StringPartsExtractorValidationResult
{
    private readonly Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

    public StringPartsExtractorValidationResult(bool isValid = true)
    {
        this.IsValid = isValid;
    }

    public bool IsValid { get; private set; }

    public IDictionary<string, List<string>> Errors => this.errors;

    public void AddError(string propertyName, string error)
    {
        if (!this.errors.TryGetValue(propertyName, out var list))
        {
            list = new List<string>();
            this.errors[propertyName] = list;
        }

        list.Add(error);
        this.IsValid = false;
    }
}
