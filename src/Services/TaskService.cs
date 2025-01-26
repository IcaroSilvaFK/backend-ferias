using backend.src.Application.Models;
using backend.src.Application.Services;
using backend.src.Database;
using backend.src.Dtos;
using Microsoft.EntityFrameworkCore;

namespace backend.src.Services;

public class TaskService(Persistence persistence): ITaskServiceInterface
{   
    private readonly Persistence _persistence = persistence;
    public async Task<TaskModel> ChangeStatus(Guid id, Guid userId)
    {
      var task = await _persistence.Tasks.FirstOrDefaultAsync(x => x.Id.Equals(id) && x.UserId.Equals(userId)) ?? throw new Exception("Task not found");

      task.ChangeCompleted();

      await _persistence.SaveChangesAsync();

      return task;
    }

    public async Task Delete(Guid id)
    {
        var task = await _persistence.Tasks.FirstOrDefaultAsync(x => x.Id.Equals(id)) ?? throw new Exception("Task not found");

        _persistence.Tasks.Remove(task);
    }

    public async Task<List<TaskModel>> FindAll(Guid userId)
    {
        var tasks = await _persistence.Tasks.Where(x => x.UserId.Equals(userId)).ToListAsync();

        return tasks;
    }

    public async Task<TaskModel> FindById(Guid id)
    {
        var task = await _persistence.Tasks.FirstOrDefaultAsync(x => x.Id.Equals(id)) ?? throw new Exception("Task not found");

        return task;
    }

    public async Task<TaskModel> Store(TaskDTO task, Guid userId)
    {
        var taskModel = new TaskModel(task.Title, task.Description, userId);

        _persistence.Tasks.Add(taskModel);
        await _persistence.SaveChangesAsync();

        return taskModel;
    }

    public Task Update(Guid id, TaskDTO task)
    {
        var taskToUpdate = _persistence.Tasks.First(x => x.Id.Equals(id)) ?? throw new Exception("Task not found");
        taskToUpdate.ChangeTile(task.Title);
        taskToUpdate.ChangeDescription(task.Description);

        _persistence.Tasks.Update(taskToUpdate);
        return _persistence.SaveChangesAsync();
    }
}