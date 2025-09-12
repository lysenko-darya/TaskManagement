using MediatR;

namespace TaskManagement.Api.Application.Commands;
public record LoginCommand : IRequest<string>
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
}
