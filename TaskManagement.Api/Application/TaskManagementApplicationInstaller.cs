using FluentValidation;
using TaskManagement.Api.Application.Behaviors;
using TaskManagement.Api.Application.Queries;
using TaskManagement.Api.Application.Validators;

namespace TaskManagement.Api.Application;

static class TaskManagementApplicationInstaller
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(TaskManagementApplicationInstaller).Assembly);
            cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
        });
        // services.AddValidatorsFromAssemblyContaining<DeleteTaskCommandValidator>(includeInternalTypes: true);
        services.AddValidatorsFromAssembly(typeof(DeleteTaskCommandValidator).Assembly);

        services.AddScoped<ITaskQueries, TaskQueries>();

        return services;
    }
}
