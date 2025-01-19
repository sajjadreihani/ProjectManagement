using ProjectManagement.Domain.SeedWork;

namespace ProjectManagement.Domain.AggregatesModel.TaskAggregates;
public interface ITaskRepository : IRepository<ProjectTask, Guid>
{
    Task<ProjectTask> GetAsNoTracking(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<ProjectTask>> GetAll(Guid projectId, CancellationToken cancellationToken);

}
