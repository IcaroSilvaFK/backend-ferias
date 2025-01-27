using backend.src.Application.Models;
using backend.src.Application.Services;
using backend.src.Database;
using backend.src.Dtos;
using Microsoft.EntityFrameworkCore;

namespace backend.src.Services;

public class TaskService(Persistence persistence) : ITaskServiceInterface
{

    private readonly Persistence _persistence = persistence;



    public async Task Delete(Guid id)
    {
        var task = await _persistence.Tasks.FirstOrDefaultAsync(x => x.Id.Equals(id)) ?? throw new BadHttpRequestException("Task not found");

        _persistence.Tasks.Remove(task);

        await _persistence.SaveChangesAsync();
    }

    public async Task<List<TaskModel>> FindAll(Guid userId, int page, int limit, string? type)
    {
        var tasksQuery = _persistence.Tasks.AsQueryable();

        if (type != null)
        {
            tasksQuery = tasksQuery.Where(x => x.Status.Equals(type));
        }

        tasksQuery = tasksQuery.Where(x => x.UserId.Equals(userId));

        var tasks = await tasksQuery.Skip((page - 1) * limit).Take(limit).ToListAsync();

        return tasks;
    }

    public async Task<TaskModel> FindById(Guid id)
    {
        var task = await _persistence.Tasks.FirstOrDefaultAsync(x => x.Id.Equals(id)) ?? throw new BadHttpRequestException("Task not found");

        return task;
    }

    public async Task<TaskModel> Store(TaskDTO task, Guid userId)
    {
        var taskModel = new TaskModel(task.Title, task.TaskStatus, task.EndDate, userId, task.Description);

        _persistence.Tasks.Add(taskModel);
        await _persistence.SaveChangesAsync();

        return taskModel;
    }

    public Task Update(Guid id, TaskDTO task)
    {
        var taskToUpdate = _persistence.Tasks.First(x => x.Id.Equals(id)) ?? throw new BadHttpRequestException("Task not found");

        taskToUpdate.ChangeDescription(task.Description ?? taskToUpdate.Description);
        taskToUpdate.ChangeTitle(task.Title ?? taskToUpdate.Title);
        taskToUpdate.ChangeStatus(task.TaskStatus ?? taskToUpdate.Status);
        taskToUpdate.ChangeEndDate(task.EndDate);

        _persistence.Tasks.Update(taskToUpdate);
        return _persistence.SaveChangesAsync();
    }

    public async Task<int> Count(Guid userId, string? type)
    {
        var productsQuery = _persistence.Tasks.AsQueryable();

        if (type != null)
        {
            productsQuery = productsQuery.Where(x => x.Status.Equals(type));
        }

        var totalTasks = await productsQuery.CountAsync();

        return totalTasks;
    }
    public async Task<TaskModel> ChangeStatus(Guid id, Guid userId, string status)
    {
        var task = await _persistence.Tasks.FirstOrDefaultAsync(x => x.Id.Equals(id) && x.UserId.Equals(userId)) ?? throw new BadHttpRequestException("Task not found");

        task.ChangeStatus(status);

        await _persistence.SaveChangesAsync();

        return task;
    }

    public async Task<int> CountDone(Guid userId)
    {
        var totalTasks = await _persistence.Tasks.CountAsync(x => x.UserId.Equals(userId) && x.Status.Equals("done"));

        return totalTasks;
    }
}