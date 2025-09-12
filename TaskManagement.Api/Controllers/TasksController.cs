using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Commands;
using TaskManagement.Domain.Abstractions;

namespace TaskManagement.Api.Controllers;

/// <summary>
/// 
/// </summary>
/// <param name="mediator"></param>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TasksController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    /// <summary>
    /// test
    /// </summary>
    /// <param name="task"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<bool>> Add(AddTaskCommand task, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(task, cancellationToken);
    }
}
