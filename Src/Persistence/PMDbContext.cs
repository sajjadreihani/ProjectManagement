using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ProjectManagement.Application.Common.Interfaces;
using ProjectManagement.Domain.AggregatesModel.NotificationAggregate;
using ProjectManagement.Domain.AggregatesModel.ProjectAggregates;
using ProjectManagement.Domain.AggregatesModel.TaskAggregates;
using ProjectManagement.Domain.SeedWork;
using System.Linq.Expressions;

namespace ProjectManagement.Persistence;

public class PMDbContext(DbContextOptions<PMDbContext> options, ICurrentUserService currentUser) : DbContext(options), IUnitOfWork
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; }
    public DbSet<ProjectReport> ProjectReports { get; set; }
    public DbSet<ProjectComment> ProjectComments { get; set; }
    public DbSet<ProjectUser> ProjectUsers { get; set; }
    public DbSet<TaskComment> TaskComments { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PMDbContext).Assembly);

        GlobalQueryFilter(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var currentDate = DateTime.UtcNow;
        foreach (var entry in ChangeTracker.Entries()
            .Where(e =>
             e.Entity is BaseEntity
             && (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)))
        {
            if (entry.State == EntityState.Deleted && entry.Entity is AuditableEntity)
            {
                entry.State = EntityState.Modified;
                entry.Property("Deleted").CurrentValue = currentDate;
            }
            else if (entry.Entity is AuditableEntity)
            {
                entry.Property("LastModifiedBy").CurrentValue = currentUser.UserId;
                entry.Property("LastModified").CurrentValue = currentDate;
            }
            
            if (entry.State == EntityState.Added)
            {
                entry.Property("CreatedBy").CurrentValue = currentUser.UserId;
                entry.Property("Created").CurrentValue = currentDate;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    private static void GlobalQueryFilter(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes()
                .Where(e => typeof(BaseEntity).IsAssignableFrom(e.ClrType)))
        {
            builder.Entity(entityType.ClrType).HasKey("Id").IsClustered(false);
            builder.Entity(entityType.ClrType).Property("Id").ValueGeneratedNever();

            builder.Entity(entityType.ClrType).Property<int>("Index").ValueGeneratedOnAdd();
            builder.Entity(entityType.ClrType).HasAlternateKey("Index").IsClustered();

            builder.Entity(entityType.ClrType).Property("CreatedBy")
                .HasMaxLength(50)
                .IsRequired();
            builder.Entity(entityType.ClrType).Property<DateTime>("Created");
        }

        Expression<Func<AuditableEntity, bool>> filterExpr = t => !EF.Property<DateTime?>(t, "Deleted").HasValue;
        foreach (var entityType in builder.Model.GetEntityTypes()
                .Where(e => typeof(AuditableEntity).IsAssignableFrom(e.ClrType)))
        {
            builder.Entity(entityType.ClrType).Property("LastModifiedBy")
                .HasMaxLength(50)
                .IsRequired();

            builder.Entity(entityType.ClrType).Property<DateTime>("LastModified");

            builder.Entity(entityType.ClrType).Property<DateTime?>("Deleted").IsRequired(false);

            var parameter = Expression.Parameter(entityType.ClrType);
            var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
            var lambdaExpression = Expression.Lambda(body, parameter);

            // set filter
            entityType.SetQueryFilter(lambdaExpression);
        }

    }

}