using MediatR;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Api.Application.Commands;

public record AddTaskCommand : IRequest<long>
{
    public string? Executor { get; set; }
    public Status? Status { get; set; }
    public Priority? Priority { get; set; }
}
