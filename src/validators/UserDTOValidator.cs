using backend.src.Dtos;
using FluentValidation;

namespace backend.src.Validators;

public class UserDTOValidator : AbstractValidator<UserDTO>
{
  public UserDTOValidator()
  {
    RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
    RuleFor(x => x.Password).NotEmpty();
    RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Email is not valid");
  }
}