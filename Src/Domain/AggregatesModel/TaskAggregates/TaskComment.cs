using ProjectManagement.Domain.SeedWork;

namespace ProjectManagement.Domain.AggregatesModel.TaskAggregates;
public class TaskComment : BaseEntity
{
    public TaskComment(ProjectTask task, string comment)
    {
        Id = Guid.NewGuid();
        Task = task ?? throw new ArgumentNullException(nameof(task));
        Comment = comment ?? throw new ArgumentNullException(nameof(comment));
    }

    private TaskComment()
    {
    }

    public ProjectTask Task { get; private init; }
    public string Comment { get; private init; }

}
