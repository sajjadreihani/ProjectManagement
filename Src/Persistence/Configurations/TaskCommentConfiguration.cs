using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.AggregatesModel.TaskAggregates;

namespace ProjectManagement.Persistence.Configurations;

public class TaskCommentConfiguration : IEntityTypeConfiguration<TaskComment>
{
    public void Configure(EntityTypeBuilder<TaskComment> builder)
    {
        builder.Property(b => b.Comment)
            .HasMaxLength(250)
           .IsRequired();

        builder.HasOne(p => p.Task)
         .WithMany()
         .HasForeignKey("TaskId")
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);
    }
}
