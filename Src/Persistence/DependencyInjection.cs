using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Domain.AggregatesModel.NotificationAggregate;
using ProjectManagement.Domain.AggregatesModel.ProjectAggregates;
using ProjectManagement.Domain.AggregatesModel.TaskAggregates;
using ProjectManagement.Persistence.Repositories;

namespace ProjectManagement.Persistence;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<PMDbContext>(options =>
            options.UseSqlServer(configuration["CONNECTION_STRING"]), ServiceLifetime.Scoped);

        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectReportRepository, ProjectReportRepository>();
        services.AddScoped<IProjectCommentRepository, ProjectCommentRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITaskCommentRepository, TaskCommentRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();

        return services;
    }
}
