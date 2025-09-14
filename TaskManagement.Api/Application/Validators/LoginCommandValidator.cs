using FluentValidation;
using TaskManagement.Api.Application.Commands;
using TaskManagement.Infrastructure.DbContexts;

namespace TaskManagement.Api.Application.Validators;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(DataSchemaConstants.USER_NAME_LENGTH);
        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
