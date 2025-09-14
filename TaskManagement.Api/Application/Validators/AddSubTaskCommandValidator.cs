using FluentValidation;
using TaskManagement.Api.Application.Commands;
using TaskManagement.Infrastructure.DbContexts;

namespace TaskManagement.Api.Application.Validators;

public class AddSubTaskCommandValidator : AbstractValidator<AddSubTaskCommand>
{
    public AddSubTaskCommandValidator()
    {
        RuleFor(x => x.ParentTaskId).NotEmpty();
        RuleFor(x => x.Executor).MaximumLength(DataSchemaConstants.USER_NAME_LENGTH);
    }
}