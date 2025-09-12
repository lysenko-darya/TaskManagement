using TaskManagement.Api.Application.Queries;

namespace TaskManagement.Api.Application;

static class TaskManagementApplicationInstaller
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(TaskManagementApplicationInstaller).Assembly));

        services.AddScoped<ITaskQueries, TaskQueries>();

        return services;
    }
}
