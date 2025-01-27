using backend.src.Application.Models;
using backend.src.Dtos;

namespace backend.src.Application.Services;

public interface IUserServiceInterface
{
  public Task<UserModel> FindById(Guid id);
  public Task<UserModel?> FindByEmail(string email);
  public Task<UserModel> Store(UserDTO user);
  public Task Update(Guid id, UserDTO user);
  public Task Delete(Guid id);
}