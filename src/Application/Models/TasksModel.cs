using System.ComponentModel.DataAnnotations.Schema;
using backend.src.Providers;

namespace backend.src.Application.Models;


[Table("tasks")]
public class TaskModel(string title, string status, DateTime endDate, Guid userId, string? description)
{
  public Guid Id { get; init; } = GuidProvider.NewGuid();
  public string Title { get; private set; } = title;
  public string Description { get; private set; } = description ?? string.Empty;
  public string Status { get; private set; } = status;
  public DateTime EndDate { get; private set; } = endDate;
  public Guid UserId { get; private set; } = userId;
  public UserModel User { get; set; }
  public void ChangeTitle(string newTitle)
  {
    Title = newTitle;
  }

  public void ChangeDescription(string newDescription)
  {
    Description = newDescription;
  }

  public void ChangeStatus(string newStatus)
  {
    Status = newStatus;
  }

  public void ChangeEndDate(DateTime newEndDate)
  {
    EndDate = newEndDate;
  }
}
