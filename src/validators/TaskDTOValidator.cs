using backend.src.Dtos;
using FluentValidation;

namespace backend.src.Validators;

public class TaskDTOValidator : AbstractValidator<TaskDTO>
{
  public TaskDTOValidator()
  {
    RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required").MinimumLength(3).WithMessage("Title must be at least 3 characters long").MaximumLength(100).WithMessage("Title must be at most 100 characters long");
    RuleFor(x => x.TaskStatus).NotEmpty().WithMessage("TaskStatus is required");
    RuleFor(x => x.EndDate).NotEmpty().WithMessage("EndDate is required").GreaterThan(DateTime.Now).WithMessage("EndDate must be in the future");
  }
}