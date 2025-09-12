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
            .HasMaxLength(100);
        builder
            .Property(x => x.Executor)
            .HasMaxLength(100);

        //builder
        //    .HasOne(t => t.Device)
        //    .WithMany(b => b.Channels)
        //    .HasForeignKey(t => t.DeviceId);
    }
}

