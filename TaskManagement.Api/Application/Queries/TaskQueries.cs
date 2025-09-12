using Microsoft.EntityFrameworkCore;
using TaskManagement.Infrastructure.DbContexts;

namespace TaskManagement.Api.Application.Queries;
public class TaskQueries(TaskManagementDbContext dbContext) : ITaskQueries
{
    public async Task<IEnumerable<TaskSummaryModel>> GetAllTasks(CancellationToken cancellationToken = default)
    {
        return await dbContext.Tasks
            .Select(t => new TaskSummaryModel
            {
                Id = t.Id,
                Author = t.Author,
                Executor = t.Executor,
                Priority = t.Priority,
                Status = t.Status
            }).ToListAsync(cancellationToken);
    }

    public async Task<TaskModel> GetTask(long id, CancellationToken cancellationToken = default)
    {
        var task = await dbContext.Tasks
            .Include(t => t.SubTasks)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken)
            ?? throw new KeyNotFoundException();

        return new TaskModel
        {
            Id = task.Id,
            Author = task.Author,
            Executor = task.Executor,
            Priority = task.Priority,
            Status = task.Status,
            SubTasks = [.. task.SubTasks.Select(t => new TaskSummaryModel
            {
                Id = t.Id,
                Author = t.Author,
                Executor = t.Executor,
                Priority = t.Priority,
                Status = t.Status
            })]
        };
    }
}
