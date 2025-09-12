using MediatR;
using TaskManagement.Api.Application.Models;
using TaskManagement.Contracts.Enums;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Api.Application.Commands;

public class UpdateTaskCommand : IRequest<bool>
{
    public long Id { get; set; }
    public string? Executor { get; set; }
    public Status Status { get; set; }
    public Priority? Priority { get; set; }
    public IReadOnlyCollection<SubTask> SubTasks { get; set; } = [];
}