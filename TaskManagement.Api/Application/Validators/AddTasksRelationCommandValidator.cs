using FluentValidation;
using TaskManagement.Api.Application.Commands;

namespace TaskManagement.Api.Application.Validators;

public class AddTasksRelationCommandValidator : AbstractValidator<AddTasksRelationCommand>
{
    public AddTasksRelationCommandValidator()
    {
        RuleFor(x => x.TaskId).NotEmpty();
        RuleFor(x => x.RelatedTaskId).NotEmpty();
    }
}