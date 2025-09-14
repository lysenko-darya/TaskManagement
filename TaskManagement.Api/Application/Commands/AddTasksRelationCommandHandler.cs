using MediatR;
using TaskManagement.Domain.Abstractions;
using TaskManagement.Domain.Exceptions;

namespace TaskManagement.Api.Application.Commands;

public class AddTasksRelationCommandHandler(ITaskRepository taskRepository,
                                     ILogger<AddTasksRelationCommandHandler> logger) : IRequestHandler<AddTasksRelationCommand, bool>
{
    private readonly ITaskRepository _taskRepository = taskRepository
        ?? throw new ArgumentNullException(nameof(taskRepository));
    private readonly ILogger<AddTasksRelationCommandHandler> _logger = logger
        ?? throw new ArgumentNullException(nameof(logger));

    public async Task<bool> Handle(AddTasksRelationCommand message, CancellationToken cancellationToken)
    {
        var currentTask = await _taskRepository.GetById(message.TaskId, cancellationToken) ?? throw new NotFoundException();
        var relatedTask = await _taskRepository.GetById(message.RelatedTaskId, cancellationToken) ?? throw new NotFoundException();

        currentTask.AddRelatedTask(relatedTask);

        await _taskRepository.Update(currentTask, cancellationToken);

        _logger.LogInformation("Relation from Task with Id: '{Id}' to Task with Id: '{RelatedTaskId}' was saved", 
            message.TaskId, message.RelatedTaskId);
        return true;
    }
}
