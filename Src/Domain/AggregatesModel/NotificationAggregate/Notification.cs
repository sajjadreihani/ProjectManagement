using ProjectManagement.Shared.Enum;

namespace ProjectManagement.Domain.AggregatesModel.NotificationAggregate;
public class Notification
{
    private Notification()
    {
    }

    public Notification(string receiverId, string title, string message, Guid projectId, Guid referenceId, ReferenceTypeEnum referenceType)
    {
        ReceiverId = receiverId ?? throw new ArgumentNullException(nameof(receiverId));
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Message = message ?? throw new ArgumentNullException(nameof(message));
        ProjectId = projectId;
        ReferenceId = referenceId;
        ReferenceType = referenceType;
        Created = DateTime.Now;
    }

    public long Id { get; }
    public string ReceiverId { get; private init; }
    public string Title { get; private init; }
    public string Message { get; private init; }
    public Guid ProjectId { get; private init; }
    public Guid ReferenceId { get; private init; }
    public ReferenceTypeEnum ReferenceType { get; private init; }
    public DateTime Created { get; private init; }
    public DateTime? Visited { get; private set; }

    public void SetVisited() => Visited = DateTime.Now;
}
