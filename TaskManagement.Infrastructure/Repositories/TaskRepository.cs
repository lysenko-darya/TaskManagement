using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Abstractions;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.DbContexts;

namespace TaskManagement.Infrastructure.Repositories;

internal class TaskRepository(TaskManagementDbContext dbContext) : ITaskRepository
{
    private readonly TaskManagementDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task<TaskEntity?> GetById(long taskId, CancellationToken cancellationToken = default)
    {
        var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);
        if (task != null)
        {
            await _dbContext.Entry(task)
                .Collection(i => i.SubTasks).LoadAsync(cancellationToken);
        }

        return task;
    }

    public async Task Add(TaskEntity task, CancellationToken cancellationToken = default)
    {
        _dbContext.Tasks.Add(task);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(TaskEntity task, CancellationToken cancellationToken = default)
    {
        _dbContext.Tasks.Update(task);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(TaskEntity task, CancellationToken cancellationToken = default)
    {
        _dbContext.Tasks.Remove(task);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
