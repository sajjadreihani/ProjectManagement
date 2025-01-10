using ProjectManagement.Domain.SeedWork;

namespace ProjectManagement.Domain.AggregatesModel.TaskAggregates;
public interface ITaskCommentRepository : IRepository<TaskComment, Guid>
{
    Task<IEnumerable<TaskComment>> GetAll(Guid taskId, CancellationToken cancellationToken);
}
