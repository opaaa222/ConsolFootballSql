using ConsolFootballSql.Domain.Models.Entities;
using ConsolFootballSql.Infrastructure.Persistence;
using ConsolFootballSql.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ConsolFootballSql.Tests;

public class TeamRepositoryTests
{
   
    private static string NewDbName() => Guid.NewGuid().ToString();

    private static FootballDbContext CreateContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<FootballDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;
        return new FootballDbContext(options);
    }

    private static Team NewTeam(string name = "Barca", string city = "Barcelona", int year = 1899)
        => new() { Name = name, City = city, FoundedYear = year };

    [Fact]
    public void Add_SavesTeam()
    {
        var db = NewDbName();
        using (var ctx = CreateContext(db))
            new TeamRepository(ctx).Add(NewTeam());

        using var check = CreateContext(db);
        Assert.Single(check.Teams);
        Assert.Equal("Barca", check.Teams.First().Name);
    }

    [Fact]
    public void GetALL_ReturnsTeamsOrderedByName()
    {
        var db = NewDbName();
        using var ctx = CreateContext(db);
        var repo = new TeamRepository(ctx);
        repo.Add(NewTeam("Valencia"));
        repo.Add(NewTeam("Alaves"));
        repo.Add(NewTeam("Real Madrid"));

        var names = repo.GetALL().Select(t => t.Name).ToList();

        Assert.Equal(new[] { "Alaves", "Real Madrid", "Valencia" }, names);
    }

    [Fact]
    public void GetById_ReturnsTeam_WhenExists()
    {
        using var ctx = CreateContext(NewDbName());
        var repo = new TeamRepository(ctx);
        var team = NewTeam();
        repo.Add(team);

        var found = repo.GetById(team.Id);

        Assert.NotNull(found);
        Assert.Equal("Barca", found!.Name);
    }

    [Fact]
    public void GetById_ReturnsNull_WhenMissing()
    {
        using var ctx = CreateContext(NewDbName());

        var found = new TeamRepository(ctx).GetById(999);

        Assert.Null(found);
    }

    [Theory]
    [InlineData("Barca", true)]
    [InlineData("  Barca  ", true)]   
    [InlineData("Real Madrid", false)]
    public void exists_ChecksByName(string name, bool expected)
    {
        using var ctx = CreateContext(NewDbName());
        var repo = new TeamRepository(ctx);
        repo.Add(NewTeam("Barca"));

        Assert.Equal(expected, repo.exists(name));
    }

    [Fact]
    public void Update_ChangesTeam()
    {
        var db = NewDbName();
        int id;
        using (var ctx = CreateContext(db))
        {
            var team = NewTeam();
            new TeamRepository(ctx).Add(team);
            id = team.Id;
        }

        using (var ctx = CreateContext(db))
        {
            var repo = new TeamRepository(ctx);
            var team = repo.GetById(id)!;
            team.City = "Catalunya";
            repo.Update(team);
        }

        using var check = CreateContext(db);
        Assert.Equal("Catalunya", check.Teams.Find(id)!.City);
    }

    [Fact]
    public void Delete_RemovesTeam()
    {
        var db = NewDbName();
        using (var ctx = CreateContext(db))
        {
            var repo = new TeamRepository(ctx);
            var team = NewTeam();
            repo.Add(team);
            repo.Delete(team);
        }

        using var check = CreateContext(db);
        Assert.Empty(check.Teams);
    }
}