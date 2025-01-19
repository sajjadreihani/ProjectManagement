using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Persistence;

public class PMDbContextFactory : DesignTimeDbContextFactoryBase<PMDbContext>
{
    protected override PMDbContext CreateNewInstance(DbContextOptions<PMDbContext> options)
    {
        return new PMDbContext(options);
    }
}
