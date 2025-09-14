using MediatR;
using TaskManagement.Domain.Abstractions;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Exceptions;

namespace TaskManagement.Api.Application.Commands;

internal class UpdateTaskCommandHandler(ITaskRepository taskRepository,
                                     ILogger<UpdateTaskCommandHandler> logger,
                                     IAuthenticationService authenticationService) : IRequestHandler<UpdateTaskCommand, bool>
{
    private readonly ITaskRepository _taskRepository = taskRepository
        ?? throw new ArgumentNullException(nameof(taskRepository));
    private readonly IAuthenticationService _authenticationService = authenticationService
        ?? throw new ArgumentNullException(nameof(authenticationService));
    private readonly ILogger<UpdateTaskCommandHandler> _logger = logger
        ?? throw new ArgumentNullException(nameof(logger));

    public async Task<bool> Handle(UpdateTaskCommand message, CancellationToken cancellationToken)
    {
        var author = _authenticationService.GetSubjectFromUser() ?? throw new AccessDeniedException();

        var task = await _taskRepository.GetById(message.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(TaskEntity), message.Id);

        task.SetExecutor(message.Executor);
        task.SetStatus(message.Status);
        task.SetPriority(message.Priority);

        await _taskRepository.Update(task, cancellationToken);
        _logger.LogInformation("Task with Id: '{Id}' was updated", task.Id);
        return true;
    }
}
