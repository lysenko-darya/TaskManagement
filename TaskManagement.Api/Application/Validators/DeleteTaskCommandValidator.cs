using FluentValidation;
using TaskManagement.Api.Application.Commands;

namespace TaskManagement.Api.Application.Validators;

public class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
{
    public DeleteTaskCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}
