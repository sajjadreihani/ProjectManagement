namespace ProjectManagement.Domain.AggregatesModel.ProjectAggregates;
public class ProjectUser
{
    private ProjectUser()
    {
    }

    public ProjectUser(string userId, string role)
    {
        UserId = userId ?? throw new ArgumentNullException(nameof(userId));
        Role = role ?? throw new ArgumentNullException(nameof(role));
    }

    public string UserId { get; private init; }
    public string Role { get; private set; }

    public void Update(string role) => Role = role;

}
