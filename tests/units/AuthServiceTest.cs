using backend.src.Application.Models;
using backend.src.Application.Services;
using backend.src.Providers;
using backend.src.Services;
using Moq;
using Xunit;

namespace backend.tests.units;


public class AuthServiceTest 
{
  
  [Fact]
  public void ShouldBeGenerateToken() 
  {
  
    var validUser = new  {
      Name = "Test",
      Email = "Test",
      Password = "Test"
    };

    var user = new UserModel(validUser.Name, validUser.Email, validUser.Password);

    var authService = new AuthService();

    var token = authService.GenerateToken(user);

    Assert.NotNull(token);
    Assert.IsType<string>(token);
  }
  
  [Fact]
  public void ShouldBeDecodeToken()
  {
    var authService = new AuthService();
 
  
    var validUser = new  {
      Name = "Test",
      Email = "Test",
      Password = "Test"
    };


    var user = new UserModel(validUser.Name, validUser.Email, validUser.Password);

    var token = authService.GenerateToken(user);

    var decodedToken = authService.Decode(token) ?? throw new Exception("Invalid token");

    Assert.NotNull(decodedToken);
    Assert.IsType<ITokenPayload>(decodedToken);
  }
}