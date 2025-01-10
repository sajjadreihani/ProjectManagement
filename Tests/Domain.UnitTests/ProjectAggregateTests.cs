using ProjectManagement.Domain.AggregatesModel.ProjectAggregates;
using ProjectManagement.Shared.Enum;
using ProjectManagement.Domain.ValueObjects;

namespace Domain.UnitTests;
public class ProjectAggregateTests
{
    [Theory, AutoData]
    public void Create_Project_success(string testString, DateTime? date)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;

        //Act
        var testProject = new Project(testString, testString, testString, testDate, testDate, testDate);

        //Assert
        Assert.NotNull(testProject);
        Assert.Equal(testString, testProject.Title);
    }

    [Fact]
    public void Create_Project_NullTitle_ArgumentNullException()
    {
        //Act
        //Assert
        Assert.Throws<ArgumentNullException>(() => new Project(null, null, null, null, null, null));
    }

    [Theory, AutoData]
    public void Update_Project_success(string testString, DateTime? date, string newTestString)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;
        var testProject = new Project(testString, testString, testString, testDate, testDate, testDate);

        //Act
        testProject.Update(newTestString, newTestString, null, testDate, testDate);

        //Assert
        Assert.NotNull(testProject);
        Assert.Equal(newTestString, testProject.Title);
        Assert.Null(testProject.StartDate);
    }

    [Theory, AutoData]
    public void Update_Project_NullTitle_ArgumentNullException(string testString, DateTime? date, string newTestString)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;
        var testProject = new Project(testString, testString, testString, testDate, testDate, testDate);

        //Act
        //Assert
        Assert.Throws<ArgumentNullException>(() => testProject.Update(null, newTestString, testDate, testDate, testDate));
    }

    [Theory, AutoData]
    public void Set_ProjectManager_success(string testString, DateTime? date, string managerId)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;
        var testProject = new Project(null, testString, testString, testDate, testDate, testDate);

        //Act
        testProject.SetManager(managerId);

        //Assert
        Assert.Equal(managerId, testProject.ManagerId);
    }

    [Theory, AutoData]
    public void Update_ProjectStatus_success(string testString, DateTime? date, ProjectStatusEnum status)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;
        var testProject = new Project(null, testString, testString, testDate, testDate, testDate);

        //Act
        testProject.UpdateStatus(status);

        //Assert
        Assert.Equal(status, testProject.Status);
    }

    [Theory, AutoData]
    public void Update_ProjectProgress_success(string testString, DateTime? date, double progress)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;
        var testProject = new Project(null, testString, testString, testDate, testDate, testDate);

        //Act
        testProject.UpdateProgress(progress);

        //Assert
        Assert.Equal(progress, testProject.Progress);
    }

    [Theory, AutoData]
    public void Add_ProjectUser_success(string testString, DateTime? date)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;
        var testProject = new Project(null, testString, testString, testDate, testDate, testDate);

        //Act
        var result = testProject.AddUser(testString, testString);

        //Assert
        Assert.False(result.HasErrors);
        Assert.NotEmpty(testProject.Users);
        Assert.True(testProject.Users.All(u => u.UserId.Equals(testString)));
    }

    [Theory, AutoData]
    public void Add_ProjectUser_DuplicateUserId_HasError(string testString, DateTime? date)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;
        var testProject = new Project(null, testString, testString, testDate, testDate, testDate);
        testProject.AddUser(testString, testString);

        //Act
        var result = testProject.AddUser(testString, testString);

        //Assert
        Assert.True(result.HasErrors);
        Assert.NotEmpty(testProject.Users);
        Assert.Equal($"Duplicate User ({testString})", result.GetError());
    }

    [Theory, AutoData]
    public void Add_ProjectUser_NullUserId_ArgumentNullException(string testString, DateTime? date)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;
        var testProject = new Project(null, testString, testString, testDate, testDate, testDate);
        testProject.AddUser(testString, testString);

        //Act
        var result = testProject.AddUser(testString, testString);

        //Assert
        Assert.True(result.HasErrors);
        Assert.NotEmpty(testProject.Users);
        Assert.Equal($"Duplicate User ({testString})", result.GetError());
    }

    [Theory, AutoData]
    public void Create_ProjectComment_success(string testString, DateTime? date)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;
        var testProject = new Project(null, testString, testString, testDate, testDate, testDate);

        //Act
        var comment = new ProjectComment(testProject, testString);

        //Assert
        Assert.NotNull(comment);
        Assert.Equal(testString, comment.Comment);
        Assert.Equal(testProject.Id, comment.Project.Id);
    }


    [Theory, AutoData]
    public void Create_ProjectComment_NullProject_ArgumentNullException(string testString)
    {
        //Arrange
        //Act
        //Assert
        Assert.Throws<ArgumentNullException>(() => new ProjectComment(null, testString));
    }

    [Theory, AutoData]
    public void Create_ProjectReport_success(string testString, DateTime? date, double testDouble)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;
        var testProject = new Project(null, testString, testString, testDate, testDate, testDate);

        //Act
        var report = new ProjectReport(testProject, testString, testString, testDouble, []);

        //Assert
        Assert.NotNull(report);
        Assert.Equal(testString, report.Title);
        Assert.Equal(testProject.Id, report.Project.Id);
        Assert.Equal(ProjectReportStatusEnum.Pending, report.Status);
        Assert.Empty(report.Attachments);
    }


    [Theory, AutoData]
    public void Create_ProjectReport_NullProject_ArgumentNullException(string testString, double testDouble)
    {
        //Arrange
        //Act
        //Assert
        Assert.Throws<ArgumentNullException>(() => new ProjectReport(null, testString, testString, testDouble, []));
    }

    [Theory, AutoData]
    public void Update_ProjectReport_success(string testString, DateTime? date, double testDouble, string newTestString)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;
        var testProject = new Project(testString, testString, testString, testDate, testDate, testDate);
        var report = new ProjectReport(testProject, testString, testString, testDouble, []);

        //Act
        report.Update(newTestString, newTestString, testDouble);

        //Assert
        Assert.NotNull(testProject);
        Assert.Equal(newTestString, report.Title);
    }

    [Theory, AutoData]
    public void Update_ProjectReport_NullTitle_ArgumentNullException(string testString, DateTime? date, double testDouble, string newTestString)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;
        var testProject = new Project(testString, testString, testString, testDate, testDate, testDate);
        var report = new ProjectReport(testProject, testString, testString, testDouble, []);

        //Act
        //Assert
        Assert.Throws<ArgumentNullException>(() => report.Update(null, newTestString, testDouble));
    }


    [Theory, AutoData]
    public void Update_ProjectReportStatus_success(string testString, DateTime? date, double testDouble, ProjectReportStatusEnum status)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;
        var testProject = new Project(null, testString, testString, testDate, testDate, testDate);
        var report = new ProjectReport(testProject, testString, testString, testDouble, []);

        //Act
        report.UpdateStatus(status, testString);

        //Assert
        Assert.Equal(status, report.Status);
        Assert.Equal(testString, report.Comment);
    }

    [Theory, AutoData]
    public void Update_ProjectReportAttachments_success(string testString, DateTime? date, double testDouble, Attachment attachment)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;
        var testProject = new Project(null, testString, testString, testDate, testDate, testDate);
        var report = new ProjectReport(testProject, testString, testString, testDouble, []);

        //Act
        report.UpdateAttachments([attachment]);

        //Assert
        Assert.NotEmpty(report.Attachments);
    }

}
