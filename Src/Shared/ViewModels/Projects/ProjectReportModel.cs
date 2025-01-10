using ProjectManagement.Shared.Enum;
using ProjectManagement.Shared.ViewModels.Common;

namespace ProjectManagement.Shared.ViewModels.Projects;
public record ProjectReportModel(Guid Id, string UserId, string Title, string Report, string Comment, ProjectReportStatusEnum Status
    , double Progress, IEnumerable<AttachmentModel> Attachments, DateTime Created);
