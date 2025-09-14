using MediatR;
using TaskManagement.Domain.Abstractions;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Exceptions;

namespace TaskManagement.Api.Application.Commands;

internal class DeleteTaskCommandHandler(ITaskRepository taskRepository,
                                     ILogger<DeleteTaskCommand> logger) : IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly ITaskRepository _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
    private readonly ILogger<DeleteTaskCommand> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<bool> Handle(DeleteTaskCommand message, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetById(message.Id, cancellationToken) ?? throw new NotFoundException(nameof(TaskEntity), message.Id);
        await _taskRepository.Delete(task, cancellationToken);
        _logger.LogInformation("Task with Id: '{Id}' was deleted", message.Id);
        return true;
    }
}