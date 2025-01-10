using ProjectManagement.Domain.AggregatesModel.NotificationAggregate;
using ProjectManagement.Shared.Enum;

namespace Domain.UnitTests;
public class NotificationAggregateTests
{
    [Theory, AutoData]
    public void Create_Notification_success(string testString, Guid testGuid, ReferenceTypeEnum referenceType)
    {
        //Arrange
        //Act
        var testNotif = new Notification(testString, testString, testString, testGuid, testGuid, referenceType);

        //Assert
        Assert.NotNull(testNotif);
        Assert.Null(testNotif.Visited);
        Assert.Equal(testString, testNotif.Title);
        Assert.Equal(testGuid, testNotif.ProjectId);
        Assert.Equal(referenceType, testNotif.ReferenceType);
    }

    [Theory, AutoData]
    public void Create_Notification_NullTitle_ArgumentNullException(string testString, Guid testGuid, ReferenceTypeEnum referenceType)
    {
        //Act
        //Assert
        Assert.Throws<ArgumentNullException>(() => new Notification(testString, null, testString, testGuid, testGuid, referenceType));
    }

    [Theory, AutoData]
    public void Set_Notification_Visited_success(string testString, Guid testGuid, ReferenceTypeEnum referenceType)
    {
        //Arrange
        var testNotif = new Notification(testString, testString, testString, testGuid, testGuid, referenceType);

        //Act
        testNotif.SetVisited();

        //Assert
        Assert.NotNull(testNotif.Visited);
    }
}

