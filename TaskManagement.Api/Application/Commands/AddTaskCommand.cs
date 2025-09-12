using MediatR;
using TaskManagement.Contracts.Enums;

namespace TaskManagement.Api.Application.Commands;

public record AddTaskCommand : IRequest<bool>
{
    public string? Executor { get; set; }
    public Status? Status { get; set; }
    public Priority? Priority { get; set; }
}
