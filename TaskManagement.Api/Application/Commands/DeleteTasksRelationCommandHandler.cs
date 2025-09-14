using MediatR;
using TaskManagement.Api.Application.Helpers;
using TaskManagement.Domain.Abstractions;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Exceptions;

namespace TaskManagement.Api.Application.Commands;

public class DeleteTasksRelationCommandHandler(ITaskRepository taskRepository,
                                     ILogger<DeleteTasksRelationCommandHandler> logger) : IRequestHandler<DeleteTasksRelationCommand, bool>
{
    private readonly ITaskRepository _taskRepository = taskRepository
        ?? throw new ArgumentNullException(nameof(taskRepository));
    private readonly ILogger<DeleteTasksRelationCommandHandler> _logger = logger
        ?? throw new ArgumentNullException(nameof(logger));

    public async Task<bool> Handle(DeleteTasksRelationCommand message, CancellationToken cancellationToken)
    {
        var currentTask = await _taskRepository.GetById(message.TaskId, cancellationToken)
            ?? throw new NotFoundException(nameof(TaskEntity), message.TaskId);
        var relatedTask = await _taskRepository.GetById(message.RelatedTaskId, cancellationToken)
            ?? throw new NotFoundException(nameof(TaskEntity), message.RelatedTaskId);

        try
        {
            using var scope = TransactionHelper.GetTransactionScope();

            currentTask.DeleteRelatedTask(relatedTask);
            await _taskRepository.Update(currentTask, cancellationToken);

            relatedTask.DeleteRelatedTask(currentTask);
            await _taskRepository.Update(relatedTask, cancellationToken);

            scope.Complete();

            _logger.LogInformation("Relation from Task with Id: '{Id}' to Task with Id: '{RelatedTaskId}' was deleted",
                message.TaskId, message.RelatedTaskId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Removing relation between task with Id: {Id} and {RelatedTaskId} is failed",
                message.TaskId, message.RelatedTaskId);
            throw;
        }
        return true;
    }
}
