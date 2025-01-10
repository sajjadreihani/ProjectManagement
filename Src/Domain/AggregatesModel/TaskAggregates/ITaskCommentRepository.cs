using ProjectManagement.Domain.SeedWork;
using ProjectManagement.Shared.ViewModels.Common;

namespace ProjectManagement.Domain.AggregatesModel.TaskAggregates;
public interface ITaskCommentRepository : IRepository<TaskComment, Guid>
{
    Task<IEnumerable<CommentModel>> GetAll(Guid taskId, CancellationToken cancellationToken);
}
