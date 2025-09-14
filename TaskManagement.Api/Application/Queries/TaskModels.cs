using TaskManagement.Domain.Enums;

namespace TaskManagement.Api.Application.Queries;

public record TaskModel
{
    public long Id { get; set; }
    public string Author { get; init; } = string.Empty;
    public string? Executor { get; init; }
    public Status Status { get; init; }
    public Priority? Priority { get; init; }

    public IReadOnlyCollection<TaskSummaryModel> SubTasks { get; init; } = [];
    public IReadOnlyCollection<TaskSummaryModel> RelatedTasks { get; init; } = [];
}

public record TaskSummaryModel
{
    public long Id { get; init; }
    public string Author { get; init; } = string.Empty;
    public string? Executor { get; init; }
    public Status Status { get; init; }
    public Priority? Priority { get; init; }
}
