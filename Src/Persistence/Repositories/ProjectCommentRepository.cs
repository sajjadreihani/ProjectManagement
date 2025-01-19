
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Domain.AggregatesModel.ProjectAggregates;

namespace ProjectManagement.Persistence.Repositories;
public class ProjectCommentRepository(PMDbContext context, IServiceProvider serviceProvider) : BaseRepository<ProjectComment, Guid>(context), IProjectCommentRepository
{
    public async Task<IEnumerable<ProjectComment>> GetAll(Guid projectId, CancellationToken cancellationToken)
    {
        using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<PMDbContext>();
        return await context.ProjectComments
                .Where(c => EF.Property<Guid>(c, "ProjectId") == projectId)
                .OrderByDescending(o => o.Created)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

    }
}
