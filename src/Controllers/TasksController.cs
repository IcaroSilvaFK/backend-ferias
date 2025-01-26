using System.Security.Claims;
using backend.src.Application.Models;
using backend.src.Application.Services;
using backend.src.Application.Views;
using backend.src.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.src.Controllers;
[ApiController]
[Route("tasks")]
public class TasksController(ITaskServiceInterface taskService) : ControllerBase
{
  private readonly ITaskServiceInterface _taskService = taskService;

  [HttpPost]
  [Authorize]
  public async Task<TaskDTO> Store(TaskDTO input)
  {

    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new BadHttpRequestException("Invalid token");

    var task = await _taskService.Store(input, new Guid(userId));

    return Ok(TasksView.ToHttp(task));
  }

  [HttpGet]
  [Authorize]
  public async Task<List<TaskDTO>> FindAll()
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new BadHttpRequestException("Invalid token");

    var tasks = await _taskService.FindAll(new Guid(userId));

    return Ok(TasksView.ToHttp(tasks));
  }

  [HttpGet("{id}")]
  [Authorize]
  public async Task<IActionResult> FindById(Guid id)
  {
    try
    {
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new BadHttpRequestException("Invalid token");

      var task = await _taskService.FindById(id);

      return Ok(TasksView.ToHttp(task));
    }
    catch (Exception er)
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

      var task = await _taskService.ChangeStatus(id, new Guid(userId));

      return Ok(TasksView.ToHttp(task));
    }
    catch (Exception er)
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

      await _taskService.Delete(id);

      return Ok();
    }
    catch (Exception er)
    {
      return BadRequest(er.Message);
    }
  }
}