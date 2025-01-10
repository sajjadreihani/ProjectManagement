namespace ProjectManagement.Domain.SeedWork;
public class AuditableEntity : BaseEntity
{
    public DateTime Modified { get; }
    public string ModifiedBy { get; }
}
