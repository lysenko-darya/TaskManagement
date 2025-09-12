using Microsoft.EntityFrameworkCore;

using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.DbContexts;

public class TaskManagementDbContext : DbContext
{
    public DbSet<TaskEntity> Tasks { get; set; }

    public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options)
       : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder?.ApplyConfigurationsFromAssembly(typeof(TaskManagementDbContext).Assembly);
        base.OnModelCreating(modelBuilder!);
    }
}
