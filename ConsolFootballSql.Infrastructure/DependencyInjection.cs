using ConsolFootballSql.Application.Interfaces;
using ConsolFootballSql.Application.InterFaces;
using ConsolFootballSql.Infrastructure.Persistence;
using ConsolFootballSql.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConsolFootballSql.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<FootballDbContext>(o => o.UseSqlServer(connectionString));
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IMatchRepository, MatchRepository>();
        return services;
    }

    public static void ApplyMigrations(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FootballDbContext>();
        dbContext.Database.Migrate();
        DbSeeder.SeedLaLigaTeams(dbContext);
    }

}