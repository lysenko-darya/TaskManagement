using MediatR;
using TaskManagement.Domain.Abstractions;

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
        var author = _authenticationService.GetSubjectFromUser() ?? throw new ArgumentNullException(); //todo

        var task = await _taskRepository.GetById(message.Id, cancellationToken) ?? throw new KeyNotFoundException();

        task.SetExecutor(message.Executor);
        task.SetStatus(message.Status);
        task.SetPriority(message.Priority);

        foreach (var subTask in message.SubTasks)
        {
            if (!task.SubTasks.Any(t => t.Id == subTask.Id))
            {
                task.AddSubTask(subTask.Author, subTask.Executor, subTask.Status, subTask.Priority);
            }
        }

        await _taskRepository.Update(task, cancellationToken);
        return true;
    }
}
