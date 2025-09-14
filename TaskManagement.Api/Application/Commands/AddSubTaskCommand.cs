using MediatR;
using TaskManagement.Contracts.Enums;

namespace TaskManagement.Api.Application.Commands;

public class AddSubTaskCommand : IRequest<bool>
{
    public long ParentTaskId { get; set; }
    public string? Executor { get; set; }
    public Status? Status { get; set; }
    public Priority? Priority { get; set; }
}
