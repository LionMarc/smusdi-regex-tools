namespace Smusdi.RegexTools;

public sealed class ExtractedPart
{
    public ExtractedPart(string name, ExtractedPartType type, string? format)
    {
        this.Name = name;
        this.Type = type;
        this.Format = format;
    }

    public string Name { get; }

    public ExtractedPartType Type { get; }

    public string? Format { get; }
}
