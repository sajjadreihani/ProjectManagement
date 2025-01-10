using ProjectManagement.Shared.Enum;
using ProjectManagement.Domain.SeedWork;
using ProjectManagement.Domain.ValueObjects;

namespace ProjectManagement.Domain.AggregatesModel.ProjectAggregates;
public class ProjectReport : AuditableEntity
{
    private ProjectReport()
    {
    }

    public ProjectReport(Project project, string title, string report, double progress, IEnumerable<Attachment> attachments)
    {
        Id = Guid.NewGuid();
        Project = project ?? throw new ArgumentNullException(nameof(project));
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Report = report ?? throw new ArgumentNullException(nameof(report));
        Progress = progress;
        Status = ProjectReportStatusEnum.Pending;

        Attachments = attachments;
    }

    public Project Project { get; private init; }
    public string Title { get; private set; }
    public string Report { get; private set; }
    public string Comment { get; private set; }
    public ProjectReportStatusEnum Status { get; private set; }
    public double Progress { get; private set; }

    public IEnumerable<Attachment> Attachments { get; private set; }
    public void UpdateAttachments(IEnumerable<Attachment> attachments) => Attachments = attachments;

    public void Update(string title, string report, double progress)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Report = report ?? throw new ArgumentNullException(nameof(report));
        Progress = progress;
    }

    public void UpdateStatus(ProjectReportStatusEnum status, string comment)
    {
        Status = status;
        Comment = comment;
    }

}
