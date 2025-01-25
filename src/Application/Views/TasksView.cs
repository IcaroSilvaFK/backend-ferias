using backend.src.Application.Models;

namespace backend.src.Application.Views;

public static class TasksView
{
  public static dynamic ToHttp(TaskModel task) {
    return new {
      ID = task.Id,
      Description = task.Description,
      Completed = task.Completed,
      CompletedAt = task.CompletedAt,
    };
  }

  public static dynamic ToHttp(List<TaskModel> tasks) {
    return tasks.Select(ToHttp).ToList();
  }
}