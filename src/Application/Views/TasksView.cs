using backend.src.Application.Models;

namespace backend.src.Application.Views;

public static class TasksView
{
  public static dynamic ToHttp(TaskModel task)
  {
    return new
    {
      ID = task.Id,
      Description = task.Description,
      Status = task.Status,
      EndDate = task.EndDate,
      Title = task.Title,
    };
  }

  public static dynamic ToHttpCount(int count)
  {
    return new
    {
      Count = count
    };
  }

  public static dynamic ToHttp(List<TaskModel> tasks, int quantityPages, int totalTasks)
  {
    if (tasks == null || tasks.Count == 0)
    {
      return new List<dynamic>();
    }
    var list = new List<dynamic>();

    foreach (var task in tasks)
    {
      list.Add(ToHttp(task));
    }

    return new
    {
      Tasks = list,
      QuantityPages = quantityPages,
      TotalTasks = totalTasks
    };
  }
}