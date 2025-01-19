using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Domain.AggregatesModel.TaskAggregates;

namespace ProjectManagement.Persistence.Repositories;
public class TaskRepository(PMDbContext context, IServiceProvider serviceProvider) : BaseRepository<ProjectTask, Guid>(context), ITaskRepository
{
    public async Task<IEnumerable<ProjectTask>> GetAll(Guid projectId, CancellationToken cancellationToken)
    {
        using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<PMDbContext>();
        return await context.ProjectTasks
                .Where(c => EF.Property<Guid>(c, "ProjectId") == projectId)
                .OrderByDescending(o => o.Created)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
    }

    public async Task<ProjectTask> GetAsNoTracking(Guid id, CancellationToken cancellationToken)
    {
        using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<PMDbContext>();
        return await context.ProjectTasks.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public override async Task<ProjectTask> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await context.ProjectTasks.Include(p => p.Project).FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}
