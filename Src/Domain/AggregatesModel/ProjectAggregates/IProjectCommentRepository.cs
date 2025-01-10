using ProjectManagement.Domain.SeedWork;
using ProjectManagement.Shared.ViewModels.Common;

namespace ProjectManagement.Domain.AggregatesModel.ProjectAggregates;
public interface IProjectCommentRepository : IRepository<ProjectComment, Guid>
{
    Task<IEnumerable<CommentModel>> GetAll(Guid projectId, CancellationToken cancellationToken);
}
