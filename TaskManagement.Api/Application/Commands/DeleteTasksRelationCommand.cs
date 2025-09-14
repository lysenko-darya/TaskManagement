using MediatR;

namespace TaskManagement.Api.Application.Commands;

public class DeleteTasksRelationCommand : IRequest<bool>
{
    public long TaskId { get; set; }
    public long RelatedTaskId { get; set; }
}
