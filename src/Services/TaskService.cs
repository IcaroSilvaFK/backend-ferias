using backend.src.Application.Models;
using backend.src.Application.Services;
using backend.src.Dtos;

namespace backend.src.Services;

public class TaskService : ITaskServiceInterface
{
    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<TaskModel>> FindAll(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<TaskModel> FindById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<TaskDTO> Store(TaskDTO task)
    {
        throw new NotImplementedException();
    }

    public Task Update(Guid id, TaskDTO task)
    {
        throw new NotImplementedException();
    }
}