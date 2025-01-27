using backend.src.Application.Models;
using backend.src.Dtos;

namespace backend.src.Application.Services
{
  public interface ITaskServiceInterface
  {
    Task<TaskModel> Store(TaskDTO task, Guid userId);
    Task<List<TaskModel>> FindAll(Guid userId, int page, int limit, string? type);
    Task<TaskModel> FindById(Guid id);
    Task Update(Guid id, TaskDTO task);
    Task Delete(Guid id);
    Task<int> Count(Guid userId, string? type);
    Task<int> CountDone(Guid userId);
    Task<TaskModel> ChangeStatus(Guid id, Guid userId, string status);
  }
}