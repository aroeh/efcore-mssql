using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Demo.Restuarants.Infrastructure.MSSQL.Repos;

public class RestuarantDbContext
(
    DbContextOptions<RestuarantDbContext> options
) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
