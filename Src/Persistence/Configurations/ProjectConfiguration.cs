using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.AggregatesModel.ProjectAggregates;

namespace ProjectManagement.Persistence.Configurations;
public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(b => b.ManagerId)
            .HasMaxLength(100)
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

        builder.Property(b => b.Deadline)
           .IsRequired(false);

        builder.HasMany(p => p.Users)
         .WithOne()
         .HasForeignKey("ProjectId")
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);
    }
}
