using MediatR;
using TaskManagement.Domain.Abstractions;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Exceptions;

namespace TaskManagement.Api.Application.Commands;
internal class AddTaskCommandHandler(ITaskRepository taskRepository,
                                     ILogger<AddTaskCommandHandler> logger,
                                     IAuthenticationService authenticationService) : IRequestHandler<AddTaskCommand, long>
{
    private readonly ITaskRepository _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
    private readonly IAuthenticationService _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));

    private readonly ILogger<AddTaskCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<long> Handle(AddTaskCommand message, CancellationToken cancellationToken)
    {
        var author = _authenticationService.GetSubjectFromUser() ?? throw new AccessDeniedException();
        var task = new TaskEntity(author, message.Executor, message.Status, message.Priority);

        await _taskRepository.Add(task, cancellationToken);

        _logger.LogInformation("Task with Id: '{Id}' was created", task.Id);
        return task.Id;
    }
}
