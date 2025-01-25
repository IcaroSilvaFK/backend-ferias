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
      try{
        var user = await _persistence.Users.FirstOrDefaultAsync(x => x.Id.Equals(id)) ?? throw new Exception("User not found");

        _persistence.Users.Remove(user);

        await _persistence.SaveChangesAsync();
      }catch (Exception e){
        throw new Exception(e.Message);
      }
    }

    public async Task<UserModel> FindByEmail(string email)
    {
      try
      {
        var user =await _persistence.Users.FirstOrDefaultAsync(x => x.Email.Equals(email)) ?? throw new Exception("User not found");

        return user;
      }
      catch(Exception e) 
      { 
        throw new Exception(e.Message);
      }
    }

    public async Task<UserModel> FindById(Guid id)
    {
        try
        {
          var user =await _persistence.Users.FirstAsync(x => x.Id.Equals(id)) ?? throw new Exception("User not found");

          return user;
        }
        catch(Exception e)
        {
          throw new Exception(e.Message);
        }
    }

    public async Task<UserModel> Store(UserDTO user)
    {
      try
      { 
        var newUser = new UserModel(user.Username, user.Email, user.Password);
        newUser.HashPassword();
        _persistence.Users.Add(newUser);
        await _persistence.SaveChangesAsync();

        return newUser;
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
        var userToUpdate = _persistence.Users.First(x => x.Id.Equals(id)) ?? throw new Exception("User not found");
        userToUpdate.Name = user.Username;
        userToUpdate.Email = user.Email;
        userToUpdate.Password = user.Password;

        _persistence.Users.Update(userToUpdate);
        await _persistence.SaveChangesAsync();
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }
}