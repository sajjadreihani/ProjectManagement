using ProjectManagement.Domain.SeedWork;
using ProjectManagement.Shared.ViewModels.Projects;

namespace ProjectManagement.Domain.AggregatesModel.ProjectAggregates;
public interface IProjectReportRepository : IRepository<ProjectReport, Guid>
{
    Task<IEnumerable<ProjectReportModel>> GetAll(Guid projectId, CancellationToken cancellationToken);
}
