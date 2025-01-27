using System.Security.Claims;
using backend.src.Application.Services;
using backend.src.Application.Views;
using backend.src.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.src.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController(IUserServiceInterface userService, IValidator<UserDTO> validator) : ControllerBase
{
  private readonly IUserServiceInterface _userService = userService;
  private readonly IValidator<UserDTO> _validator = validator;
  [HttpPost]
  public async Task<IActionResult> Store(UserDTO user)
  {
    try
    {
      var validateResult = await _validator.ValidateAsync(user);

      if (!validateResult.IsValid)
      {
        return UnprocessableEntity(validateResult.Errors);
      }

      var result = await _userService.Store(user);

      return Ok(UsersView.ToHttp(result));
    }
    catch (Exception er)
    {
      return BadRequest(er.Message);
    }
  }

  [HttpGet("me")]
  [Authorize]
  public async Task<IActionResult> Delete()
  {
    try
    {
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

      if (userId == null)
      {
        return BadRequest("Invalid token");
      }

      var user = await _userService.FindById(new Guid(userId));

      return Ok(UsersView.ToHttp(user));
    }
    catch (Exception er)
    {
      return BadRequest(er.Message);
    }
  }


  [HttpGet("{id}")]
  public async Task<IActionResult> FindById(Guid id)
  {
    try
    {
      var result = await _userService.FindById(id);

      return Ok(UsersView.ToHttp(result));
    }
    catch (Exception er)
    {
      return BadRequest(er.Message);
    }
  }
}