using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Abstractions;

public interface ITaskRepository
{
    Task Add(TaskEntity task, CancellationToken cancellationToken = default);
    Task Delete(TaskEntity task, CancellationToken cancellationToken = default);
    Task<TaskEntity?> GetById(long taskId, CancellationToken cancellationToken = default);
    Task Update(TaskEntity task, CancellationToken cancellationToken = default);
}
