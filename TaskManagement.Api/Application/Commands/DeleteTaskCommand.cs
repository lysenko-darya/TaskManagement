using MediatR;

namespace TaskManagement.Api.Application.Commands;

public class DeleteTaskCommand : IRequest<bool>
{
    public long Id { get; set; }
}
