using backend.src.Application.Models;
using backend.src.Database;
using backend.src.Dtos;
using backend.src.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace backend.tests.integrations;

public class UserServiceTest
{
  [Fact]
  public async Task ShouldBeCreateUser() 
  {
    var user = new UserDTO("Test", "Test", "Test");
    var options = new DbContextOptionsBuilder<Persistence>()
    .UseSqlite("Data Source=:memory:") 
    .Options;
    var userService = new UserService(new Persistence(options));

    var createdUser = await userService.Store(user);

    Assert.NotNull(createdUser);
    Assert.IsType<UserModel>(createdUser);
    Assert.NotEqual(createdUser.Password, user.Password);
  }

    [Fact]
    public async Task ShouldBeFindUserById() 
  {
    var user = new UserDTO("Test", "Test", "Test");
    var options = new DbContextOptionsBuilder<Persistence>()
    .UseSqlite("Data Source=:memory:") 
    .Options;

    using var context = new Persistence(options);
    var userService = new UserService(context);
    var createdUser = await userService.Store(user);
    var foundUser = await userService.FindById(createdUser.Id);

    Assert.NotNull(foundUser);
    Assert.IsType<UserModel>(foundUser);
    Assert.Equal(createdUser.Id, foundUser.Id);
    Assert.Equal(createdUser.Name, foundUser.Name);
    Assert.Equal(createdUser.Email, foundUser.Email);
    Assert.Equal(createdUser.Password, foundUser.Password);



    }
}