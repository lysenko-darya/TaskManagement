using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.Api.Application.Commands;

namespace TaskManagement.Api.Controllers;

/// <summary>
/// 
/// </summary>
/// <param name="mediator"></param>
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    /// <summary>
    /// Login
    /// </summary>
    /// <param name="creds">username and password</param>
    /// <param name="cancellationToken"></param>
    /// <returns>token</returns>
    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login(LoginCommand creds, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(creds, cancellationToken);
    }
}