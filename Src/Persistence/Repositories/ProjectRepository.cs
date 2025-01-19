using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Domain.AggregatesModel.ProjectAggregates;
using ProjectManagement.Shared.Enum;

namespace ProjectManagement.Persistence.Repositories;
public class ProjectRepository(PMDbContext context, IServiceProvider serviceProvider) : BaseRepository<Project, Guid>(context), IProjectRepository
{
    public async Task<IEnumerable<Project>> GetAll(string title, string userId, ProjectStatusEnum? status, int pageNumber, int rowCount, CancellationToken cancellationToken)
    {
        using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<PMDbContext>();
        return await context.Projects
            .Where(c => (string.IsNullOrWhiteSpace(title) || c.Title.Contains(title))
                && (string.IsNullOrWhiteSpace(userId) || c.ManagerId.Equals(userId) || c.Users.Any(user => user.UserId.Equals(userId)))
                && (!status.HasValue || c.Status == status.Value))
            .OrderByDescending(o => o.LastModified)
            .Skip(pageNumber * rowCount).Take(rowCount)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<int> Count(string title, string userId, ProjectStatusEnum? status, CancellationToken cancellationToken)
    {
        using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<PMDbContext>();
        return await context.Projects
                .Where(c => (string.IsNullOrWhiteSpace(title) || c.Title.Contains(title))
                    && (string.IsNullOrWhiteSpace(userId) || c.ManagerId.Equals(userId) || c.Users.Any(user => user.UserId.Equals(userId)))
                    && (!status.HasValue || c.Status == status.Value))
                .CountAsync(cancellationToken);
    }

    public async Task<Project> GetAsNoTracking(Guid id, CancellationToken cancellationToken)
    {
        using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<PMDbContext>();
        return await context.Projects.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Project> GetWithUsers(Guid id, CancellationToken cancellationToken)
    {
        return await context.Projects.Include(p => p.Users).FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Project> GetWithUsersAsNoTracking(Guid id, CancellationToken cancellationToken)
    {
        using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<PMDbContext>();
        return await context.Projects.Include(p => p.Users).FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<bool> IsUserValid(string userId, Guid projectId, CancellationToken cancellationToken)
    {
        using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<PMDbContext>();
        return await context.Projects
                .AnyAsync(c => c.Id == projectId
                    && (c.ManagerId.Equals(userId) || c.Users.Any(user => user.UserId.Equals(userId)))
                    , cancellationToken);
    }
}
