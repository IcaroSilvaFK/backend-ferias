using System.Security.Claims;
using backend.src.Application.Models;
using backend.src.Application.Views;
using backend.src.Database;
using backend.src.Dtos;
using backend.src.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.src.Controllers;
[ApiController]
[Route("tasks")]
public class TasksController(Persistence persistence) : ControllerBase
{
  private readonly Persistence _persistence = persistence;

    [HttpPost]
  [Authorize]
  public async Task<TaskDTO>Store(TaskDTO input) 
  { 
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new BadHttpRequestException("Invalid token");

    var task = new TaskModel(input.Title, input.Description, new Guid(userId));

    _persistence.Tasks.Add(task);
    await _persistence.SaveChangesAsync();

    return Ok(TasksView.ToHttp(task));
  }

  [HttpGet]
  [Authorize]
  public async Task<List<TaskDTO>> FindAll()
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new BadHttpRequestException("Invalid token");

    var tasks =await _persistence.Tasks.Where(x => x.UserId.Equals(new Guid(userId))).ToListAsync();

    return Ok(TasksView.ToHttp(tasks));
  }

  [HttpGet("{id}")]
  [Authorize]
  public async Task<IActionResult> FindById(Guid id)
  {
    try
    {
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new BadHttpRequestException("Invalid token");

      var task = await _persistence.Tasks.FirstOrDefaultAsync(x => x.Id.Equals(id) && x.UserId.Equals(new Guid(userId))) ?? throw new Exception("Task not found");

      return Ok(TasksView.ToHttp(task));
    }
    catch(Exception er)
    {
      return BadRequest(er.Message);
    }
  }


  [HttpPut("{id}/completed")]
  [Authorize]
  public async Task<IActionResult> Complete(Guid id)
  {
    try
    {
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new BadHttpRequestException("Invalid token");

      var task = await _persistence.Tasks.FirstOrDefaultAsync(x => x.Id.Equals(id) && x.UserId.Equals(new Guid(userId))) ?? throw new Exception("Task not found");

      task.ChangeCompleted();

      await _persistence.SaveChangesAsync();

      return Ok(TasksView.ToHttp(task));
    }
    catch(Exception er)
    {
      return BadRequest(er.Message);
    }   
  }   


  [HttpDelete("{id}")]
  [Authorize]
  public async Task<IActionResult> Delete(Guid id)
  {
    try
    {
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new BadHttpRequestException("Invalid token");

      var task = await _persistence.Tasks.FirstOrDefaultAsync(x => x.Id.Equals(id) && x.UserId.Equals(new Guid(userId))) ?? throw new Exception("Task not found");

      _persistence.Tasks.Remove(task);

      await _persistence.SaveChangesAsync();

      return Ok(TasksView.ToHttp(task));
    }
    catch(Exception er)
    {
      return BadRequest(er.Message);
    }   
  }
}