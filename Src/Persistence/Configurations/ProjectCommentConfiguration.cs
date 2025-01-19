using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.AggregatesModel.ProjectAggregates;

namespace ProjectManagement.Persistence.Configurations;

public class ProjectCommentConfiguration : IEntityTypeConfiguration<ProjectComment>
{
    public void Configure(EntityTypeBuilder<ProjectComment> builder)
    {
        builder.Property(b => b.Comment)
            .HasMaxLength(250)
           .IsRequired();

        builder.HasOne(p => p.Project)
         .WithMany()
         .HasForeignKey("ProjectId")
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);

    }
}
