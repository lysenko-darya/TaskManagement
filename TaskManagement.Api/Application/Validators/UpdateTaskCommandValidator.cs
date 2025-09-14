using FluentValidation;
using TaskManagement.Api.Application.Commands;
using TaskManagement.Infrastructure.DbContexts;

namespace TaskManagement.Api.Application.Validators;

public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Executor).MaximumLength(DataSchemaConstants.USER_NAME_LENGTH);
    }
}