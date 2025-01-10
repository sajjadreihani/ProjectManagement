using ProjectManagement.Domain.SeedWork;
using ProjectManagement.Shared.ViewModels.Projects;
using ProjectManagement.Shared.Enum;

namespace ProjectManagement.Domain.AggregatesModel.ProjectAggregates;
public interface IProjectRepository : IRepository<Project, Guid>
{
    Task<bool> IsUserValid(string userId, Guid projectId, CancellationToken cancellationToken);

    Task<Project> GetWithUsers(Guid id, CancellationToken cancellationToken);
    Task<Project> GetWithUsersAsNoTracking(Guid id, CancellationToken cancellationToken);
    Task<Project> GetAsNoTracking(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<PagedProjectModel>> GetAll(string name, string managerId, ProjectStatusEnum? status, int pageNumber, int rowCount, CancellationToken cancellationToken);

    Task<int> Count(string title, string managerId, ProjectStatusEnum? status, CancellationToken cancellationToken);

    void Delete(ProjectReport report);
}
