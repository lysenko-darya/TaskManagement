using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Abstractions;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Api.Application.Commands;
internal class LoginCommandHandler(IAuthenticationService authenticationService,
                                     ILogger<LoginCommandHandler> logger) : IRequestHandler<LoginCommand, string>
{
    private readonly IAuthenticationService _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));

    private readonly ILogger<LoginCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<string> Handle(LoginCommand message, CancellationToken cancellationToken)
    {
        // todo: check creds
        return _authenticationService.GenerateNewToken(message.UserName);
    }
}
