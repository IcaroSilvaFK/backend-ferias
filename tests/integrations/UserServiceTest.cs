using System.Formats.Asn1;
using backend.src.Application.Models;
using backend.src.Dtos;
using backend.src.Services;
using backend.tests.InMemory;
using Xunit;

namespace backend.tests.integrations;

public class UserServiceTest
{
  [Fact]
  public async Task ShouldBeCreateUser()
  {
    var user = new UserDTO("Test", "Test", "Test");

    using var persistence = InMemoryDatabase.Initialize("users");

    var userService = new UserService(persistence);

    var createdUser = await userService.Store(user);

    Assert.NotNull(createdUser);
    Assert.IsType<UserModel>(createdUser);
    Assert.NotEqual(createdUser.Password, user.Password);
  }

  [Fact]
  public async Task ShouldBeFindUserById()
  {
    var user = new UserDTO("Test", "test@test.com", "Test");
    using var persistence = InMemoryDatabase.Initialize("users");


    var userService = new UserService(persistence);

    var createdUser = await userService.Store(user);
    var foundUser = await userService.FindById(createdUser.Id);

    Assert.NotNull(foundUser);
    Assert.IsType<UserModel>(foundUser);
    Assert.Equal(createdUser.Id, foundUser.Id);
    Assert.Equal(createdUser.Name, foundUser.Name);
    Assert.Equal(createdUser.Email, foundUser.Email);
    Assert.Equal(createdUser.Password, foundUser.Password);
  }

  [Fact]
  public async Task ShouldBeFindUserByEmail()
  {
    var user = new UserDTO("Test", "mock@test.com", "Test");
    using var persistence = InMemoryDatabase.Initialize("users");

    var userService = new UserService(persistence);

    var createdUser = await userService.Store(user);

    var foundUser = await userService.FindByEmail(user.Email!);

    Assert.NotNull(foundUser);
    Assert.IsType<UserModel>(foundUser);
    Assert.Equal(createdUser.Id, foundUser.Id);
    Assert.Equal(createdUser.Name, foundUser.Name);
    Assert.Equal(createdUser.Email, foundUser.Email);
    Assert.Equal(createdUser.Password, foundUser.Password);
  }

  [Fact]
  public async Task ShouldNotGenerateExceptionWhenFindUserByEmailNotFound()
  {
    using var persistence = InMemoryDatabase.Initialize("users");

    var userService = new UserService(persistence);

    var createdUser = await userService.FindByEmail("test@test");

    Assert.Null(createdUser);
  }

  [Fact]
  public async Task ShouldExpectExceptionWhenFindUserByIdNotFound()
  {
    var user = new UserDTO("Test", "testc@test.com", "Test");
    using var persistence = InMemoryDatabase.Initialize("users");

    var userService = new UserService(persistence);

    await userService.Store(user);

    await Assert.ThrowsAsync<BadHttpRequestException>(
        async () => await userService.FindById(Guid.NewGuid())
    );
  }

  [Fact]
  public async Task ShouldExpectUpdateUser()
  {
    var user = new UserDTO("Test", "updateUser@test.com", "Test");
    using var persistence = InMemoryDatabase.Initialize("users");

    var userService = new UserService(persistence);

    var createdUser = await userService.Store(user);

    var updatedUser = new UserDTO("TestUpdated", "updateUser@test.com", "Test");

    await userService.Update(createdUser.Id, updatedUser);

    var foundUser = await userService.FindById(createdUser.Id);

    Assert.NotNull(foundUser);
    Assert.IsType<UserModel>(foundUser);
    Assert.Equal(createdUser.Id, foundUser.Id);
    Assert.Equal(updatedUser.Username, foundUser.Name);
  }

  [Fact]
  public async Task ShouldExpectDeleteUser()
  {
    var user = new UserDTO("Test", "deleteUser@test.com", "Test");
    using var persistence = InMemoryDatabase.Initialize("users");

    var userService = new UserService(persistence);

    var createdUser = await userService.Store(user);

    await userService.Delete(createdUser.Id);


    await Assert.ThrowsAsync<BadHttpRequestException>(
        async () => await userService.FindById(createdUser.Id)
    );
  }
}