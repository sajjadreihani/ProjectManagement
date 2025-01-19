using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.AggregatesModel.TaskAggregates;

namespace ProjectManagement.Persistence.Configurations;
public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
{
    public void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        builder.Property(b => b.AssignedTo)
            .HasMaxLength(50)
           .IsRequired();

        builder.Property(b => b.Title)
            .HasMaxLength(100)
           .IsRequired();

        builder.Property(b => b.Description)
            .HasColumnType("nvarchar(MAX)")
           .IsRequired(false);

        builder.Property(b => b.StartDate)
           .IsRequired(false);

        builder.Property(b => b.EndDate)
           .IsRequired(false);

        builder.OwnsMany(b => b.Attachments, b => b.ToJson());

        builder.HasOne(p => p.Project)
         .WithMany(p => p.Tasks)
         .HasForeignKey("ProjectId")
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);
    }
}
