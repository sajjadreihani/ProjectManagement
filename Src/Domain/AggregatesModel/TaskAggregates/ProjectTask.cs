using ProjectManagement.Domain.AggregatesModel.ProjectAggregates;
using ProjectManagement.Shared.Enum;
using ProjectManagement.Domain.ValueObjects;
using ProjectManagement.Domain.SeedWork;

namespace ProjectManagement.Domain.AggregatesModel.TaskAggregates;
public class ProjectTask : AuditableEntity
{
    private ProjectTask()
    {
    }

    public ProjectTask(Project project, string assignedTo, string title, IEnumerable<Attachment> attachments)
    {
        Id = Guid.NewGuid();
        Project = project ?? throw new ArgumentNullException(nameof(project));
        Title = title ?? throw new ArgumentNullException(nameof(title));
        AssignedTo = assignedTo ?? throw new ArgumentNullException(nameof(assignedTo));
        Status = TaskStatusEnum.Pending;
        Attachments = attachments;
    }

    public Project Project { get; private init; }
    public string AssignedTo { get; private init; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public TaskStatusEnum Status { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }

    public void UpdateStatus(TaskStatusEnum status) => Status = status;

    public void Update(string title, string description, DateTime? startDate, DateTime? endDate)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
    }

    public IEnumerable<Attachment> Attachments { get; private set; }
    public void UpdateAttachments(IEnumerable<Attachment> attachments) => Attachments = attachments;
}
