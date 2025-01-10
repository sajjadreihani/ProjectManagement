using ProjectManagement.Domain.SeedWork;

namespace ProjectManagement.Domain.AggregatesModel.ProjectAggregates;
public interface IProjectCommentRepository : IRepository<ProjectComment, Guid>
{
    Task<IEnumerable<ProjectComment>> GetAll(Guid projectId, CancellationToken cancellationToken);
}
