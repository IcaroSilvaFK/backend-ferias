using backend.src.Application.Models;

namespace backend.src.Application.Views;


public class LoginView
{
    public static dynamic ToHttp(string token, UserModel? user)
    {   
       
        return new
        {
            AccessToken = token,
            User = user != null ?   UsersView.ToHttp(user) : null
        };
    }
}