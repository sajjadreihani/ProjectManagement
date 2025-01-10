using ProjectManagement.Domain.AggregatesModel.ProjectAggregates;
using ProjectManagement.Domain.AggregatesModel.TaskAggregates;
using ProjectManagement.Shared.Enum;
using ProjectManagement.Domain.ValueObjects;

namespace Domain.UnitTests;
public class TaskAggregateTests
{
    [Theory, AutoData]
    public void Create_ProjectTask_success(string testString, DateTime? date)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;
        var testProject = new Project(testString, testString, testString, testDate, testDate, testDate);

        //Act
        var task = new ProjectTask(testProject, testString, testString, []);

        //Assert
        Assert.NotNull(task);
        Assert.Equal(testString, task.Title);
        Assert.Equal(testProject.Id, task.Project.Id);
    }

    [Theory, AutoData]
    public void Create_ProjectTask_NullProject_ArgumentNullException(string testString)
    {
        //Act
        //Assert
        Assert.Throws<ArgumentNullException>(() => new ProjectTask(null, testString, testString, []));
    }

    [Theory, AutoData]
    public void Update_ProjectTask_success(string testString, DateTime? date, string newTestString)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;
        var testProject = new Project(testString, testString, testString, testDate, testDate, testDate);
        var task = new ProjectTask(testProject, testString, testString, []);

        //Act
        task.Update(newTestString, newTestString, date, null);

        //Assert
        Assert.NotNull(task);
        Assert.Equal(newTestString, task.Title);
        Assert.Equal(date, task.StartDate);
        Assert.Null(task.EndDate);
    }

    [Theory, AutoData]
    public void Update_ProjectTask_NullTitle_ArgumentNullException(string testString, DateTime? date, string newTestString)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;
        var testProject = new Project(testString, testString, testString, testDate, testDate, testDate);
        var task = new ProjectTask(testProject, testString, testString, []);

        //Act
        //Assert
        Assert.Throws<ArgumentNullException>(() => task.Update(null, newTestString, date, date));
    }


    [Theory, AutoData]
    public void Update_ProjectStatus_success(string testString, DateTime? date, TaskStatusEnum status)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;
        var testProject = new Project(null, testString, testString, testDate, testDate, testDate);
        var task = new ProjectTask(testProject, testString, testString, []);

        //Act
        task.UpdateStatus(status);

        //Assert
        Assert.Equal(status, task.Status);
    }

    [Theory, AutoData]
    public void Update_ProjectTaskAttachments_success(string testString, DateTime? date, Attachment attachment)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;
        var testProject = new Project(null, testString, testString, testDate, testDate, testDate);
        var task = new ProjectTask(testProject, testString, testString, []);

        //Act
        task.UpdateAttachments([attachment]);

        //Assert
        Assert.NotEmpty(task.Attachments);
    }

    [Theory, AutoData]
    public void Create_TaskComment_success(string testString, DateTime? date)
    {
        //Arrange
        DateOnly? testDate = date.HasValue ? DateOnly.FromDateTime(date.Value) : null;
        var testProject = new Project(null, testString, testString, testDate, testDate, testDate);
        var task = new ProjectTask(testProject, testString, testString, []);

        //Act
        var comment = new TaskComment(task, testString);

        //Assert
        Assert.NotNull(comment);
        Assert.Equal(testString, comment.Comment);
        Assert.Equal(task.Id, comment.Task.Id);
    }


    [Theory, AutoData]
    public void Create_TaskComment_NullTask_ArgumentNullException(string testString)
    {
        //Arrange
        //Act
        //Assert
        Assert.Throws<ArgumentNullException>(() => new TaskComment(null, testString));
    }

}
