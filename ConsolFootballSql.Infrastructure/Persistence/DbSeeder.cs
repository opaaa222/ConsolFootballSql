using ConsolFootballSql.Domain.Models.Entities;

namespace ConsolFootballSql.Infrastructure.Persistence;

public static class DbSeeder
{
    private static readonly (string Name, string City, int Year)[] LaLigaTeams =
    {
        ("Alaves", "Vitoria-Gasteiz", 1921),
        ("Athletic Bilbao", "Bilbao", 1898),
        ("Atletico Madrid", "Madrid", 1903),
        ("FC Barcelona", "Barcelona", 1899),
        ("Celta Vigo", "Vigo", 1923),
        ("Deportivo A Coruna", "A Coruna", 1906),
        ("Elche", "Elche", 1923),
        ("Espanyol", "Cornella de Llobregat", 1900),
        ("Getafe", "Getafe", 1983),
        ("Levante", "Valencia", 1909),
        ("Malaga", "Malaga", 1904),
        ("Osasuna", "Pamplona", 1920),
        ("Racing Santander", "Santander", 1913),
        ("Rayo Vallecano", "Madrid", 1924),
        ("Real Betis", "Seville", 1907),
        ("Real Madrid", "Madrid", 1902),
        ("Real Sociedad", "San Sebastian", 1909),
        ("Sevilla", "Seville", 1890),
        ("Valencia", "Valencia", 1919),
        ("Villarreal", "Villarreal", 1923),
    };

    public static void SeedLaLigaTeams(FootballDbContext db)
    {
        foreach (var (name, city, year) in LaLigaTeams)
        {
            if (db.Teams.Any(t => t.Name == name)) continue;
            db.Teams.Add(new Team { Name = name, City = city, FoundedYear = year });
        }
        db.SaveChanges();
    }
}