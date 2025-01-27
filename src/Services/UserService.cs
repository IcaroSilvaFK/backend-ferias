using backend.src.Application.Models;
using backend.src.Application.Services;
using backend.src.Database;
using backend.src.Dtos;
using Microsoft.EntityFrameworkCore;

namespace backend.src.Services;

public class UserService(Persistence persistence) : IUserServiceInterface
{
  private readonly Persistence _persistence = persistence;

  public async Task Delete(Guid id)
  {
    try
    {
      var user = await _persistence.Users.FirstOrDefaultAsync(x => x.Id.Equals(id)) ?? throw new BadHttpRequestException("User not found");

      _persistence.Users.Remove(user);

      await _persistence.SaveChangesAsync();
    }
    catch (BadHttpRequestException)
    {
      throw;
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }
  }

  public async Task<UserModel?> FindByEmail(string email)
  {
    try
    {
      var user = await _persistence.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));

      return user;
    }
    catch (BadHttpRequestException)
    {
      throw;
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }
  }

  public async Task<UserModel> FindById(Guid id)
  {
    try
    {
      var user = await _persistence.Users.FirstOrDefaultAsync(x => x.Id.Equals(id))
        ?? throw new BadHttpRequestException("User not found");

      return user;
    }
    catch (BadHttpRequestException)
    {
      throw;
    }
    catch (Exception e)
    {
      throw new Exception("An unexpected error occurred: " + e.Message, e);
    }
  }

  public async Task<UserModel> Store(UserDTO user)
  {
    try
    {
      var existsUser = await FindByEmail(user.Email!);

      if (existsUser != null)
      {
        throw new BadHttpRequestException("User Exists already" + user.Email);
      }

      var newUser = new UserModel(user.Username, user.Email!, user.Password!);
      newUser.HashPassword();
      _persistence.Users.Add(newUser);
      await _persistence.SaveChangesAsync();

      return newUser;
    }
    catch (BadHttpRequestException)
    {
      throw;
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }
  }

  public async Task Update(Guid id, UserDTO user)
  {
    try
    {
      var userToUpdate = _persistence.Users.First(x => x.Id.Equals(id)) ?? throw new BadHttpRequestException("User not found");
      userToUpdate.Name = user.Username;

      _persistence.Users.Update(userToUpdate);
      await _persistence.SaveChangesAsync();
    }
    catch (BadHttpRequestException)
    {
      throw;
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }
  }
}