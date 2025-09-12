using MediatR;
using TaskManagement.Domain.Abstractions;

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
