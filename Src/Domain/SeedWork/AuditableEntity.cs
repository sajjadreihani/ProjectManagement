namespace ProjectManagement.Domain.SeedWork;
public class AuditableEntity : BaseEntity
{
    public DateTime LastModified { get; }
    public string LastModifiedBy { get; }
}
