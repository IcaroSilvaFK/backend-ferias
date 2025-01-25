using backend.src.Application.Models;

namespace backend.src.Application.Services;


public interface IAuthServiceInterface
{
  string GenerateToken(UserModel user);
  ITokenPayload Decode(string token);
}

public class ITokenPayload 
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
}