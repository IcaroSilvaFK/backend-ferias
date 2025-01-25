using backend.src.Providers;

namespace backend.src.Application.Models;
  public class TaskModel
  {
    public Guid Id { get; init; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool Completed { get; private set; } 
    public DateTime? CompletedAt { get; private set; }
    public Guid UserId { get; private set; }

    public TaskModel(string name, string description, Guid userId) 
    {
      Id = GuidProvider.NewGuid();
      Name = name;
      Description = description;
      Completed = false;
      UserId = userId;
    }


    public void ChangeCompleted(){
      Completed = !Completed;
      CompletedAt = Completed ?  DateTime.UtcNow : null;
    }
  }
