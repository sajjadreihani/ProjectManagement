using ProjectManagement.Domain.Handlers;
using ProjectManagement.Domain.AggregatesModel.TaskAggregates;
using ProjectManagement.Shared.Enum;
using ProjectManagement.Domain.SeedWork;

namespace ProjectManagement.Domain.AggregatesModel.ProjectAggregates;
public class Project : AuditableEntity
{
    private Project()
    {
    }

    public Project(string managerId, string title, string description, DateOnly? startDate, DateOnly? endDate, DateOnly? deadline)
    {
        Id = Guid.NewGuid();
        Title = title ?? throw new ArgumentNullException(nameof(title));
        ManagerId = managerId;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Deadline = deadline;
        Status = ProjectStatusEnum.Active;
        Progress = 0;

        Users = [];
    }

    public string ManagerId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public ProjectStatusEnum Status { get; private set; }
    public DateOnly? StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public DateOnly? Deadline { get; private set; }
    public double Progress { get; private set; }

    public void SetManager(string managerId) => ManagerId = managerId;

    public void UpdateStatus(ProjectStatusEnum status) => Status = status;

    public void UpdateProgress(double progress) => Progress = progress;

    public void Update(string title, string description, DateOnly? startDate, DateOnly? endDate, DateOnly? deadLine)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Deadline = deadLine;
    }

    public ICollection<ProjectUser> Users { get; private init; }
    public IStatusHandler AddUser(string userId, string role)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);

        var status = new StatusHandler();
        if (Users is null)
        {
            status.AddError(
                "Must first retrieve the User's list",
                nameof(Users));
            return status;
        }

        if (Users.Any(u => u.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase)))
        {
            status.AddError(
                "Duplicate User",
                userId.ToString());
            return status;

        }

        Users.Add(new ProjectUser(userId, role));
        return status;
    }

    public IStatusHandler RemoveUser(string userId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);

        var status = new StatusHandler();
        if (Users is null)
        {
            status.AddError(
                "Must first retrieve the User's list",
                nameof(Users));
            return status;
        }

        var user = Users.FirstOrDefault(u => u.UserId.Equals(u.UserId, StringComparison.OrdinalIgnoreCase));

        Users.Remove(user);

        return status;
    }

    public ICollection<ProjectReport> Reports { get; }
    public ICollection<ProjectTask> Tasks { get; }
}
