using backend.src.Application.Models;

namespace backend.src.Application.Views;

public class UsersView {

  public static dynamic ToHttp(UserModel user) {
    return new {
      ID = user.Id,
      Username= user.Name,
      Email = user.Email,
    };
  }
}