using Xunit;
using backend.src.Application.Models;

namespace backend.tests.units;

public class TaskModelTest
{
  [Fact]
  public void ShouldBeInstanceTaskModel()
  { 
  

    var validTask = new  {
      Name = "Test",
      Description = "Test",
      Completed = false,
      UserId = Guid.NewGuid()
    };

    var task = new TaskModel(validTask.Name, validTask.Description, validTask.UserId);

    Assert.Equal(validTask.Name, task.Name);
    Assert.Equal(validTask.Description, task.Description);
    Assert.Equal(validTask.Completed, task.Completed);
    Assert.Equal(validTask.UserId, task.UserId);
    Assert.Null(task.CompletedAt);
  }

  [Fact]
  public void ShouldBeSetEndDateWhenCompleted()
  {

    var validTask = new  {
      Name = "Test",
      Description = "Test",
      Completed = false,
      UserId = Guid.NewGuid()
    };

    var task = new TaskModel(validTask.Name, validTask.Description, validTask.UserId);
  
    task.ChangeCompleted();

    Assert.NotNull(task.CompletedAt);
    Assert.True(task.Completed);
  }
}