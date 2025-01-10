using ProjectManagement.Domain.SeedWork;

namespace ProjectManagement.Domain.AggregatesModel.ProjectAggregates;
public interface IProjectReportRepository : IRepository<ProjectReport, Guid>
{
    Task<IEnumerable<ProjectReport>> GetAll(Guid projectId, CancellationToken cancellationToken);
}
