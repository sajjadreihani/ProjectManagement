using ProjectManagement.Domain.SeedWork;

namespace ProjectManagement.Domain.ValueObjects;
public class Attachment(string fileName, string url) : ValueObject
{
    public string FileName { get; private init; } = fileName;
    public string Url { get; private init; } = url;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FileName;
        yield return Url;
    }
}
