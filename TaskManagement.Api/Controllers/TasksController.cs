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
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> Add(AddTaskCommand command, CancellationToken cancellationToken = default)
    {
        var id = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id });
    }

    /// <summary>
    /// Add new subtask to parent task
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("add-subtask")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> AddSubTask(AddSubTaskCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Set tasks as related to each other
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("add-tasks-relation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> AddTasksRelation(AddTasksRelationCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Unset tasks as related to each other
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("delete-tasks-relation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> DeleteTasksRelation(DeleteTasksRelationCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Update task
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> Update(UpdateTaskCommand command, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Delete task with subtasks
    /// </summary>
    /// <param name="taskId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{taskId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> Delete(long taskId, CancellationToken cancellationToken = default)
    {
        var deleteTaskCommand = new DeleteTaskCommand { Id = taskId };
        return await _mediator.Send(deleteTaskCommand, cancellationToken);
    }
}
