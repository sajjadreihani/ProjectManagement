using ProjectManagement.Shared.Enum;

namespace ProjectManagement.Shared.ViewModels.Projects;
public record PagedProjectModel(Guid Id, string ManagerId, string Title, string Description, string Phase, ProjectStatusEnum Status, DateTime? StartDate, DateTime? Deadline, double Progress);
