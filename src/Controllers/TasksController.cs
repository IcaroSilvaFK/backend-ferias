using System.Security.Claims;
using backend.src.Application.Services;
using backend.src.Application.Views;
using backend.src.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.src.Controllers;
[ApiController]
[Route("api/tasks")]
public class TasksController(ITaskServiceInterface taskService, IValidator<TaskDTO> validator) : ControllerBase
{
  private readonly ITaskServiceInterface _taskService = taskService;
  private readonly IValidator<TaskDTO> _validator = validator;
  private readonly int _defaultLimitPage = 20;

  [HttpPost]
  [Authorize]
  public async Task<IActionResult> Store(TaskDTO input)
  {

    var validateResult = await _validator.ValidateAsync(input);

    if (!validateResult.IsValid)
    {
      return UnprocessableEntity(validateResult.Errors);
    }

    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new BadHttpRequestException("Invalid token");

    var task = await _taskService.Store(input, new Guid(userId));

    return Ok(TasksView.ToHttp(task));
  }

  [HttpGet]
  [Authorize]
  public async Task<IActionResult> FindAll(int page = 1, string? type = null)
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new BadHttpRequestException("Invalid token");

    var totalTasks = await _taskService.Count(new Guid(userId), type);
    var totalPages = (int)Math.Ceiling((double)totalTasks / _defaultLimitPage);
    var tasks = await _taskService.FindAll(new Guid(userId), page, _defaultLimitPage, type);

    if (tasks.Count == 0)
    {
      return NoContent();
    }

    return Ok(TasksView.ToHttp(tasks, totalPages, totalTasks));
  }

  [HttpGet("done")]
  [Authorize]
  public async Task<IActionResult> FindAllDone()
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new BadHttpRequestException("Invalid token");
    var dones = await _taskService.CountDone(new Guid(userId));

    return Ok(TasksView.ToHttpCount(dones));
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


  [HttpPut("{id}/status")]
  [Authorize]
  public async Task<IActionResult> Complete(Guid id, UpdateTaskStatusDto input)
  {
    try
    {
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new BadHttpRequestException("Invalid token");

      var task = await _taskService.ChangeStatus(id, new Guid(userId), input.Status);

      return Ok(TasksView.ToHttp(task));
    }
    catch (Exception er)
    {
      return BadRequest(er.Message);
    }
  }

  [HttpPut("{id}")]
  [Authorize]
  public async Task<IActionResult> Update(Guid id, TaskDTO task)
  {
    try
    {
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new BadHttpRequestException("Invalid token");

      await _taskService.Update(id, task);

      return NoContent();
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

      return NoContent();
    }
    catch (Exception er)
    {
      return BadRequest(er.Message);
    }
  }
}