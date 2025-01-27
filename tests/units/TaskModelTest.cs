using Xunit;
using backend.src.Application.Models;

namespace backend.tests.units;

public class TaskModelTest
{
  [Fact]
  public void ShouldBeInstanceTaskModel()
  {


    var validTask = new
    {
      Title = "Test",
      Description = "Test",
      status = "Completed",
      EndDate = DateTime.Now,
      UserId = Guid.NewGuid()
    };

    var task = new TaskModel(validTask.Title, validTask.status, validTask.EndDate, validTask.UserId, validTask.Description);

    Assert.Equal(validTask.Title, task.Title);
    Assert.Equal(validTask.Description, task.Description);
    Assert.Equal(validTask.EndDate, task.EndDate);
    Assert.Equal(validTask.UserId, task.UserId);
    Assert.Equal(validTask.status, task.Status);
  }

  [Fact]
  public void ShouldBeChangeEndDate()
  {

    var validTask = new
    {
      Title = "Test",
      Description = "Test",
      status = "Completed",
      EndDate = DateTime.Now,
      UserId = Guid.NewGuid()
    };

    var task = new TaskModel(validTask.Title, validTask.status, validTask.EndDate, validTask.UserId, validTask.Description);

    task.ChangeEndDate(DateTime.Now.AddDays(1));

    Assert.NotEqual(validTask.EndDate, task.EndDate);
  }
}