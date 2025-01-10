namespace ProjectManagement.Domain.SeedWork;
public class BaseEntity
{
    public Guid Id { get; internal init; }
    public DateTime Created { get; }
    public string CreatedBy { get; }
}
