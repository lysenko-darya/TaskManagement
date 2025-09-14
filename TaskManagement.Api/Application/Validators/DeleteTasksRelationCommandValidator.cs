using FluentValidation;
using TaskManagement.Api.Application.Commands;

namespace TaskManagement.Api.Application.Validators;

public class DeleteTasksRelationCommandValidator : AbstractValidator<DeleteTasksRelationCommand>
{
    public DeleteTasksRelationCommandValidator()
    {
        RuleFor(x => x.TaskId).NotEmpty();
        RuleFor(x => x.RelatedTaskId).NotEmpty();
    }
}
