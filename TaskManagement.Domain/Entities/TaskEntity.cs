using TaskManagement.Domain.Enums;
using TaskManagement.Domain.Exceptions;

namespace TaskManagement.Domain.Entities;

public class TaskEntity
{
    public long Id { get; private set; }
    public string Author { get; private set; } = string.Empty;
    public string? Executor { get; private set; }
    public Status Status { get; private set; }
    public Priority? Priority { get; private set; }

    private readonly List<TaskEntity> _subTasks = null!;
    public IReadOnlyCollection<TaskEntity> SubTasks => _subTasks;

    private readonly List<TaskEntity> _relatedTasks = null!;
    public IReadOnlyCollection<TaskEntity> RelatedTasks => _relatedTasks;

    protected TaskEntity()
    {
        _subTasks = [];
        _relatedTasks = [];
    }

    public TaskEntity(string author, string? executor = null, Status? status = null, Priority? priority = null) : this()
    {
        Author = author;
        Executor = executor;
        Status = status ?? Status.New;
        Priority = priority;
    }

    public void AddSubTask(string author, string? executor = null, Status? status = null, Priority? priority = null)
    {
        var subTask = new TaskEntity(author, executor, status, priority);
        _subTasks.Add(subTask);
    }

    public void AddRelatedTask(TaskEntity relatedTask)
    {
        if (ExistsRelatedTask(relatedTask))
        {
            throw new DomainException($"The tasks are already linked.");
        }
        _relatedTasks.Add(relatedTask);
    }

    public void DeleteRelatedTask(TaskEntity relatedTask)
    {
        if (!ExistsRelatedTask(relatedTask))
        {
            throw new DomainException($"The tasks are not linked.");
        }
        _relatedTasks.Remove(relatedTask);
    }

    private bool ExistsRelatedTask(TaskEntity relatedTask)
    {
        return _relatedTasks.Any(t => t.Id == relatedTask.Id);
    }

    public void SetStatus(Status newStatus)
    {
        if (Status == Status.Done)
        {
            StatusChangeException(newStatus);
        }
        if (Status == Status.InProgress && newStatus == Status.New)
        {
            StatusChangeException(newStatus);
        }
        Status = newStatus;
    }

    public void SetPriority(Priority? newPriority)
    {
        Priority = newPriority;
    }

    public void SetExecutor(string? executor)
    {
        Executor = executor;
    }

    private void StatusChangeException(Status newStatus)
    {
        throw new DomainException($"Is not possible to change the task status from {Status} to {newStatus}.");
    }
}
