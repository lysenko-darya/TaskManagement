using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Application.Commands;
using TaskManagement.Api.Application.Queries;

namespace TaskManagement.Api.Controllers;

/// <summary>
/// 
/// </summary>
/// <param name="mediator"></param>
/// <param name="taskQueries"></param>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TasksController(IMediator mediator, ITaskQueries taskQueries) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly ITaskQueries _taskQueries = taskQueries ?? throw new ArgumentNullException(nameof(taskQueries));

    /// <summary>
    /// Get task with subtasks
    /// </summary>
    /// <param name="taskId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{taskId}")]
    public async Task<ActionResult<TaskModel>> GetById(long taskId, CancellationToken cancellationToken = default)
    {
        return await _taskQueries.GetTask(taskId, cancellationToken);
    }

    /// <summary>
    /// Add new task
    /// </summary>
    /// <param name="task"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<bool>> Add(AddTaskCommand task, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(task, cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="task"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult<bool>> Update(UpdateTaskCommand task, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(task, cancellationToken);
    }
}
