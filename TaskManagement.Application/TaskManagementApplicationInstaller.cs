using Microsoft.Extensions.DependencyInjection;

namespace TaskManagement.Application;

public static class TaskManagementApplicationInstaller
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(TaskManagementApplicationInstaller).Assembly));
        return services;
    }
}
