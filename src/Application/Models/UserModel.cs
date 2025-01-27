using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.src.Providers;
using Microsoft.EntityFrameworkCore;

namespace backend.src.Application.Models;

[Table("users")]
public class UserModel(string name, string email, string password)
{
  public Guid Id { get; set; } = GuidProvider.NewGuid();
  public string Name { get; set; } = name;
  public string Email { get; set; } = email;
  public string Password { get; set; } = password;
  public ICollection<TaskModel> Tasks { get; set; }
  public void HashPassword()
  {
    Password = HashProvider.HashPassword(Password);
  }

  public bool VerifyPassword(string password)
  {
    return HashProvider.VerifyPassword(password, Password);
  }
}
