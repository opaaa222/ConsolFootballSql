using ConsolFootballSql.Domain.Models;
using ConsolFootballSql.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsolFootballSql.Infrastructure.Persistence;

public class FootballDbContext : DbContext
{
    public FootballDbContext(DbContextOptions<FootballDbContext> options) : base(options) { }

    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<Goal> Goals => Set<Goal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>(e =>
        {
            e.Property(t => t.Name).IsRequired().HasMaxLength(100);
            e.Property(t => t.City).HasMaxLength(100);
            e.HasIndex(t => t.Name).IsUnique();
        });

        modelBuilder.Entity<Player>(e =>
        {
            e.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            e.HasIndex(p => new { p.TeamId, p.Number })
                .IsUnique();

            e.HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Match>(e =>
        {
            e.HasOne(m => m.HomeTeam)
             .WithMany(t => t.HomeMatches)
             .HasForeignKey(m => m.HomeTeamId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(m => m.AwayTeam)
             .WithMany(t => t.AwayMatches)
             .HasForeignKey(m => m.AwayTeamId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Goal>(e =>
        {
            e.HasOne(g => g.Match)
             .WithMany(m => m.Goals)
             .HasForeignKey(g => g.MatchId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(g => g.Player)
             .WithMany(p => p.Goals)
             .HasForeignKey(g => g.PlayerId)
             .OnDelete(DeleteBehavior.Restrict);
        });
    }
}