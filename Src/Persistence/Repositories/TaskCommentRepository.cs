using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Domain.AggregatesModel.TaskAggregates;

namespace ProjectManagement.Persistence.Repositories;
public class TaskCommentRepository(PMDbContext context, IServiceProvider serviceProvider) : BaseRepository<TaskComment, Guid>(context), ITaskCommentRepository
{
    public async Task<IEnumerable<TaskComment>> GetAll(Guid taskId, CancellationToken cancellationToken)
    {
        using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<PMDbContext>();
        return await context.TaskComments
                .Where(c => EF.Property<Guid>(c, "TaskId") == taskId)
                .OrderByDescending(o => o.Created)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

    }
}
