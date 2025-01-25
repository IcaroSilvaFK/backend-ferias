using backend.src.Application.Models;
using backend.src.Dtos;

namespace backend.src.Application.Services
{
  public interface ITaskServiceInterface
  {
    Task<TaskDTO> Store(TaskDTO task);
    Task<List<TaskModel>> FindAll(Guid userId);
    Task<TaskModel> FindById(Guid id);
    Task Update(Guid id, TaskDTO task);
    Task Delete(Guid id);
  }
}