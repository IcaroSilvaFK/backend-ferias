using backend.src.Providers;

namespace backend.src.Application.Models;

public class UserModel(string name, string email, string password)
{
    public Guid Id { get; set; } = GuidProvider.NewGuid();
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;

    public void HashPassword()
    { 
      Password = HashProvider.HashPassword(Password);
    }

    public bool VerifyPassword(string password)
    {
      return HashProvider.VerifyPassword(password, Password);
    }
}
