using TaskManagement.Domain.Enums;

namespace TaskManagement.Api.Application.Models;

public class SubTask
{
    public long Id { get; init; }
    public string Author { get; init; } = string.Empty;
    public string? Executor { get; init; }
    public Status Status { get; init; }
    public Priority? Priority { get; init; }
}
