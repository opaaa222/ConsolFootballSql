using ConsolFootballSql.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConsolFootballSql.Infrastructure.Persistence
{
    public class FootballDbContextFactory
        : IDesignTimeDbContextFactory<FootballDbContext>
    {
        public FootballDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder =
                new DbContextOptionsBuilder<FootballDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-QV3K8PJ;Database=FootballDb;Trusted_Connection=True;TrustServerCertificate=True");

            return new FootballDbContext(optionsBuilder.Options);
        }
    }
}