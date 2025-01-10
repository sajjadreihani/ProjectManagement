using ProjectManagement.Domain.SeedWork;

namespace ProjectManagement.Domain.AggregatesModel.ProjectAggregates;
public class ProjectComment : BaseModel
{
    private ProjectComment()
    {
    }

    public ProjectComment(Project project, string comment)
    {
        Id = Guid.NewGuid();
        Project = project ?? throw new ArgumentNullException(nameof(project));
        Comment = comment ?? throw new ArgumentNullException(nameof(comment));
    }

    public Project Project { get; private init; }
    public string Comment { get; private init; }
}
