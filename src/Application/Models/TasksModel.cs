using System.ComponentModel.DataAnnotations.Schema;
using backend.src.Providers;

namespace backend.src.Application.Models;

[Table("tasks")]
public class TaskModel(string name, string description, Guid userId)
{
  public Guid Id { get; init; } = GuidProvider.NewGuid();
  public string Name { get; private set; } = name;
  public string Description { get; private set; } = description;
  public bool Completed { get; private set; } = false;
  public DateTime? CompletedAt { get; private set; }
  public Guid UserId { get; private set; } = userId;

  public void ChangeCompleted()
  {
    Completed = !Completed;
    CompletedAt = Completed ? DateTime.UtcNow : null;
  }

  public void ChangeDescription(string newDescription)
  {
    Description = newDescription;
  }

  public void ChangeTile(string title)
  {
    Name = title;
  }
}
