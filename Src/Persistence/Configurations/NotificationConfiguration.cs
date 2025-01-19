using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.AggregatesModel.NotificationAggregate;

namespace ProjectManagement.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(b => b.ReceiverId)
            .HasMaxLength(100)
           .IsRequired();

        builder.Property(b => b.Title)
            .HasMaxLength(250)
           .IsRequired();

        builder.Property(b => b.Message)
            .HasMaxLength(500)
           .IsRequired();

        builder.Property(b => b.ProjectId)
           .IsRequired();

        builder.Property(b => b.ReferenceId)
           .IsRequired();

        builder.Property(b => b.ReferenceType)
           .IsRequired();

        builder.Property(b => b.Visited)
           .IsRequired(false);

        builder.Property(b => b.Created)
           .IsRequired();
    }
}
