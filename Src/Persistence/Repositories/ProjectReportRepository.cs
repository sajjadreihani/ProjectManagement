using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Domain.AggregatesModel.ProjectAggregates;

namespace ProjectManagement.Persistence.Repositories;
public class ProjectReportRepository(PMDbContext context, IServiceProvider serviceProvider) : BaseRepository<ProjectReport, Guid>(context), IProjectReportRepository
{
    public async Task<IEnumerable<ProjectReport>> GetAll(Guid projectId, CancellationToken cancellationToken)
    {
        using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<PMDbContext>();
        return await context.ProjectReports
                .Where(c => EF.Property<Guid>(c, "ProjectId") == projectId)
                .OrderByDescending(o => o.Created)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

    }
}
