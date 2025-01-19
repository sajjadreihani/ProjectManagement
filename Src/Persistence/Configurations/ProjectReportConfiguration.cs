using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.AggregatesModel.ProjectAggregates;

namespace ProjectManagement.Persistence.Configurations;
public class ProjectReportConfiguration : IEntityTypeConfiguration<ProjectReport>
{
    public void Configure(EntityTypeBuilder<ProjectReport> builder)
    {
        builder.Property(b => b.Title)
            .HasMaxLength(100)
           .IsRequired();

        builder.Property(b => b.Comment)
            .HasMaxLength(500)
           .IsRequired(false);

        builder.Property(b => b.Report)
            .HasColumnType("nvarchar(MAX)")
           .IsRequired();

        builder.OwnsMany(b => b.Attachments, b => b.ToJson());

        builder.HasOne(p => p.Project)
            .WithMany(p => p.Reports)
            .HasForeignKey("ProjectId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
