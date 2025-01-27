using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using backend.src.Application.Models;
using backend.src.Application.Services;
using Microsoft.IdentityModel.Tokens;

namespace backend.src.Services;

public class AuthService : IAuthServiceInterface
{
  public string GenerateToken(UserModel user)
  {
    var key = Encoding.UTF8.GetBytes("e5266e9f-6aa0-4282-84f8-4657e2762050!");
    var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.NameId, user.Name)
    };
    var token = new JwtSecurityToken(
      issuer: "backend",
      audience: "backend",
      claims: claims,
      expires: DateTime.Now.AddDays(1),
      signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

  public ITokenPayload Decode(string token)
  {
    var handler = new JwtSecurityTokenHandler();

    if (!handler.CanReadToken(token))
    {
      throw new Exception("Invalid token");
    }

    var jwtToken = handler.ReadJwtToken(token);
    var payload = jwtToken.Payload.SerializeToJson();

    var obj = JsonSerializer.Deserialize<ITokenPayload>(payload) ?? throw new Exception("Invalid token");

    return obj;
  }
}