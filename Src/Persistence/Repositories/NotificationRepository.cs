using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Domain.AggregatesModel.NotificationAggregate;

namespace ProjectManagement.Persistence.Repositories;
public class NotificationRepository(PMDbContext context, IServiceProvider serviceProvider) : BaseRepository<Notification, long>(context), INotificationRepository
{
    public async Task<IEnumerable<Notification>> GetAll(string userId, CancellationToken cancellationToken)
    {
        using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<PMDbContext>();
        return await context.Notifications.Where(n => n.ReceiverId.Equals(userId) && !n.Visited.HasValue)
            .OrderByDescending(n => n.Id)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    }
}
