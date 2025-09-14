using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.DbContexts.Configurations;

internal class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.ToTable("tasks");
        builder.HasKey(e => e.Id);
        builder
            .Property(x => x.Author)
            .HasMaxLength(DataSchemaConstants.USER_NAME_LENGTH);
        builder
            .Property(x => x.Executor)
            .HasMaxLength(DataSchemaConstants.USER_NAME_LENGTH);

        builder
            .HasMany(t => t.SubTasks)
            .WithOne()
            .HasForeignKey("ParentTaskId");

        builder
            .HasMany(t => t.RelatedTasks)
            .WithMany()
            .UsingEntity<TaskRelation>(
                configureRight => configureRight.HasOne<TaskEntity>().WithMany().HasForeignKey(e => e.RelatedTaskId),
                configureLeft => configureLeft.HasOne<TaskEntity>().WithMany().HasForeignKey(e => e.TaskId));
    }
}

