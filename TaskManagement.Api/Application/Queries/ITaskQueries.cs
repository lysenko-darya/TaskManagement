namespace TaskManagement.Api.Application.Queries;

public interface ITaskQueries
{
    Task<TaskModel> GetTask(long id, CancellationToken cancellationToken = default);
}
