using MediatR;
using TaskManagement.Domain.Abstractions;
using TaskManagement.Domain.Exceptions;

namespace TaskManagement.Api.Application.Commands;

internal class AddSubTaskCommandHandler(ITaskRepository taskRepository,
                                     ILogger<AddSubTaskCommandHandler> logger,
                                     IAuthenticationService authenticationService) : IRequestHandler<AddSubTaskCommand, bool>
{
    private readonly ITaskRepository _taskRepository = taskRepository
        ?? throw new ArgumentNullException(nameof(taskRepository));
    private readonly IAuthenticationService _authenticationService = authenticationService
        ?? throw new ArgumentNullException(nameof(authenticationService));
    private readonly ILogger<AddSubTaskCommandHandler> _logger = logger
        ?? throw new ArgumentNullException(nameof(logger));

    public async Task<bool> Handle(AddSubTaskCommand message, CancellationToken cancellationToken)
    {
        var author = _authenticationService.GetSubjectFromUser() ?? throw new AccessDeniedException();

        var parentTask = await _taskRepository.GetById(message.ParentTaskId, cancellationToken) ?? throw new NotFoundException();

        parentTask.AddSubTask(author, message.Executor, message.Status, message.Priority);

        await _taskRepository.Update(parentTask, cancellationToken);

        _logger.LogInformation("SubTask was added to Task with Id: '{Id}'", message.ParentTaskId);
        return true;
    }
}
