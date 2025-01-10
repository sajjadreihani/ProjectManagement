using ProjectManagement.Shared.Enum;

namespace ProjectManagement.Shared.ViewModels.Tasks;
public record ProjectTaskModel(Guid Id, string UserId, string Title, TaskStatusEnum Status, DateTime? StartDate, DateTime? EndDate, DateTime Created);
