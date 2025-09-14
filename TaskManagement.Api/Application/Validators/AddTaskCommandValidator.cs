using FluentValidation;
using TaskManagement.Api.Application.Commands;
using TaskManagement.Infrastructure.DbContexts;

namespace TaskManagement.Api.Application.Validators;

public class AddTaskCommandValidator : AbstractValidator<AddTaskCommand>
{
    public AddTaskCommandValidator()
    {
        RuleFor(x => x.Executor).MaximumLength(DataSchemaConstants.USER_NAME_LENGTH);
    }
}