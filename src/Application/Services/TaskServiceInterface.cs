using backend.src.Application.Models;
using backend.src.Dtos;

namespace backend.src.Application.Services
{
  public interface ITaskServiceInterface
  {
    Task<TaskModel> Store(TaskDTO task,Guid userId);
    Task<List<TaskModel>> FindAll(Guid userId);
    Task<TaskModel> FindById(Guid id);
    Task Update(Guid id, TaskDTO task);
    Task Delete(Guid id);

    Task<TaskModel> ChangeStatus(Guid id, Guid userId);
  }
}