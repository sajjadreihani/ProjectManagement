using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.AggregatesModel.ProjectAggregates;

namespace ProjectManagement.Persistence.Configurations;
public class ProjectUserConfiguration : IEntityTypeConfiguration<ProjectUser>
{
    public void Configure(EntityTypeBuilder<ProjectUser> builder)
    {
        builder.HasKey(["ProjectId", "UserId"]).IsClustered(false);

        builder.Property(b => b.UserId)
            .HasMaxLength(50)
           .IsRequired();

        builder.Property(b => b.Role)
            .HasMaxLength(100)
           .IsRequired();
    }
}
