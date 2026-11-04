using ConsolFootballSql.Application.DTOs;
using ConsolFootballSql.Application.Service;
using ConsolFootballSql.Application.Services;
using ConsolFootballSql.Domain.Models.ENUM;
using ConsolFootballSql.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var provider = new ServiceCollection()
    .AddInfrastructure(config.GetConnectionString("FootballDb")!)
    .AddScoped<TeamService>()
    .AddScoped<PlayerService>()
    .AddScoped<MatchService>()
    .BuildServiceProvider();

while (true)
{
    using var scope = provider.CreateScope();
    var teams = scope.ServiceProvider.GetRequiredService<TeamService>();
    var players = scope.ServiceProvider.GetRequiredService<PlayerService>();
    var matches = scope.ServiceProvider.GetRequiredService<MatchService>();

    Console.WriteLine("\n=== PEKHBURTI ===");
    Console.WriteLine("1. Gundebi          2. Gundis damateba     3. Gundis washla");
    Console.WriteLine("4. Motamasheebi     5. Motamashis damateba");
    Console.WriteLine("6. Matchebi         7. Matchis shekmna     8. Golis damateba");
    Console.WriteLine("0. Gasvla");

    var choice = Ask("Archevani");
    if (choice == "0") break;

    try
    {
        switch (choice)
        {
            case "1":
                foreach (var t in teams.GetAll())
                    Console.WriteLine($"{t.Id}. {t.Name} ({t.City}, {t.FoundedYear})");
                break;

            case "2":
                teams.Create(new CreateTeamDto(
                    Ask("Saxeli"), Ask("Qalaqi"), AskInt("Daarsebis weli")));
                Console.WriteLine("Gundi daemata.");
                break;

            case "3":
                teams.Delete(AskInt("Gundis Id"));
                Console.WriteLine("Gundi washilda.");
                break;

            case "4":
                foreach (var p in players.GetAll())
                    Console.WriteLine($"{p.Id}. {p.Name} #{p.Number} {p.Position} - {p.Team.Name}");
                break;

            case "5":
                var name = Ask("Saxeli");
                var number = AskInt("Nomeri");
                var birth = AskDate("Dabadebis tarighi (magalitad 25.12.2000)");
                var pos = AskInt("Pozicia (0-mekare, 1-damcveli, 2-naxevardamcveli, 3-tavdamsxmeli)");
                if (!Enum.IsDefined(typeof(POSITION), pos))
                    throw new ArgumentException("Pozicia arasworia.");
                var teamId = AskInt("Gundis Id");
                players.Create(new CreatePlayerDto(name, number, (POSITION)pos, teamId, birth));
                Console.WriteLine("Motamashe daemata.");
                break;

            case "6":
                foreach (var m in matches.GetAll())
                    Console.WriteLine($"{m.Id}. {m.HomeTeam.Name} {m.HomeTeamScore}:{m.AwayTeamScore} {m.AwayTeam.Name} ({m.MatchDate:dd.MM.yyyy})");
                break;

            case "7":
                matches.Create(new CreateMatchDto(
                    AskInt("Maspinzeli gundis Id"),
                    AskInt("Stumari gundis Id"),
                    AskDate("Tarighi")));
                Console.WriteLine("Matchi shekmnilia.");
                break;

            case "8":
                matches.AddGoal(new AddGoalDto(
                    AskInt("Matchis Id"), AskInt("Motamashis Id"), AskInt("Wuti")));
                Console.WriteLine("Goli daemata.");
                break;

            default:
                Console.WriteLine("Arasworia archevani.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Shecdoma: {ex.Message}");
    }
}

static string Ask(string label)
{
    Console.Write($"{label}: ");
    return Console.ReadLine() ?? "";
}

static int AskInt(string label)
{
    while (true)
    {
        if (int.TryParse(Ask(label), out var v)) return v;
        Console.WriteLine("Sheiqvane ricxvi.");
    }
}

static DateTime AskDate(string label)
{
    while (true)
    {
        if (DateTime.TryParse(Ask(label), out var v)) return v;
        Console.WriteLine("Tarighi arasworia.");
    }
}
