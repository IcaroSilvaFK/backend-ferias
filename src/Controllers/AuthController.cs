using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using backend.src.Application.Services;
using backend.src.Application.Views;
using backend.src.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.src.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthServiceInterface authService, IUserServiceInterface userService) : ControllerBase
{
  private readonly IAuthServiceInterface _authService = authService;
  private readonly IUserServiceInterface _userService = userService;

  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginDto data)
  {
    try
    {
      var result = await _userService.FindByEmail(data.Email);

      Console.WriteLine(result);
      if (result == null)
      {
        return BadRequest("Invalid credentials");
      }
      if (!result.VerifyPassword(data.Password))
      {
        return BadRequest("Invalid credentials");
      }

      var token = _authService.GenerateToken(result);

      return Ok(LoginView.ToHttp(token, result));
    }
    catch (Exception er)
    {
      return BadRequest(er.Message);
    }
  }

  [HttpGet("refresh")]
  [Authorize]
  public async Task<IActionResult> Refresh()
  {
    try
    {
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


      if (userId == null)
      {
        return BadRequest("Invalid token");
      }

      var user = await _userService.FindById(new Guid(userId));

      var token = _authService.GenerateToken(user);

      return Ok(LoginView.ToHttp(token, null));
    }
    catch (Exception er)
    {
      return BadRequest(er.Message);
    }
  }
}