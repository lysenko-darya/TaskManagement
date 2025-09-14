namespace TaskManagement.Domain.Entities;

public class TaskRelation
{
    public long TaskId { get; set; }

    public long RelatedTaskId { get; set; }
}
