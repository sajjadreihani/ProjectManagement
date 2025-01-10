using ProjectManagement.Domain.SeedWork;

namespace ProjectManagement.Domain.AggregatesModel.NotificationAggregate;
public interface INotificationRepository : IRepository<Notification, long>
{
    Task<IEnumerable<Notification>> GetAll(string userId, CancellationToken cancellationToken);
}
