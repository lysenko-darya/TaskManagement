using TaskManagement.Contracts.Enums;

namespace TaskManagement.Domain.Entities;

public class TaskEntity
{
    public long Id { get; private set; }
    public string Author { get; private set; } = string.Empty;
    public string? Executor { get; private set; }
    public Status Status { get; private set; }
    public Priority? Priority { get; private set; }

    //private readonly List<TaskEntity> _subTasks = null!;
    //public IReadOnlyCollection<TaskEntity> SubTasks => _subTasks;

    //private readonly List<TaskEntity> _relatedTasks = null!;
    //public IReadOnlyCollection<TaskEntity> RelatedTasks => _relatedTasks;

    protected TaskEntity()
    {
        //_subTasks = [];
        //_relatedTasks = [];
    }

    public TaskEntity(string author, string? executor = null, Status? status = null, Priority? priority = null) : this()
    {
        Author = author;
        Executor = executor;
        Status = status ?? Status.New;
        Priority = priority;
    }
}
