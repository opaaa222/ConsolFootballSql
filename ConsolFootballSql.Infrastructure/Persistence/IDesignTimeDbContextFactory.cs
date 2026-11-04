using ConsolFootballSql.Application.Interfaces;
using ConsolFootballSql.Infrastructure.Persistence;
using ConsolFootballSql.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

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
                "Server=DESKTOP-QV3K8PJ\\SQLEXPRESS;Database=FootballDb;Trusted_Connection=True;TrustServerCertificate=True");

            return new FootballDbContext(optionsBuilder.Options);
        }
    }
}