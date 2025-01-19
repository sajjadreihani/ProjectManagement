using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ProjectManagement.Persistence;

public abstract class DesignTimeDbContextFactoryBase<TContext> :
    IDesignTimeDbContextFactory<TContext> where TContext : DbContext
{
    private const string CONNECTION_STRINGName = "CONNECTION_STRING";
    private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

    public TContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}WebApp", Path.DirectorySeparatorChar);
        return Create(basePath, Environment.GetEnvironmentVariable(AspNetCoreEnvironment));
    }

    protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

    private TContext Create(string basePath, string environmentName)
    {

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.Local.json", optional: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var CONNECTION_STRING = configuration[CONNECTION_STRINGName];

        return Create(CONNECTION_STRING);
    }

    private TContext Create(string CONNECTION_STRING)
    {
        if (string.IsNullOrEmpty(CONNECTION_STRING))
        {
            throw new ArgumentException($"Connection string '{CONNECTION_STRINGName}' is null or empty.", nameof(CONNECTION_STRING));
        }

        Console.WriteLine($"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{CONNECTION_STRING}'.");

        var optionsBuilder = new DbContextOptionsBuilder<TContext>();

        optionsBuilder.UseSqlServer(CONNECTION_STRING);

        return CreateNewInstance(optionsBuilder.Options);
    }
}
