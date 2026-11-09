using ConsolFootballSql.Domain.Models.Entities;
using ConsolFootballSql.Domain.Models.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;

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

    private static readonly string[] FirstNames =
    {
        "Carlos", "Javier", "Miguel", "Sergio", "Pablo", "Alvaro", "Diego", "Adrian", "Daniel", "David",
        "Marcos", "Ivan", "Raul", "Hugo", "Lucas", "Mateo", "Pedro", "Alejandro", "Antonio", "Fernando"
    };

    private static readonly string[] LastNames =
    {
        "Garcia", "Martinez", "Lopez", "Sanchez", "Perez", "Gomez", "Fernandez", "Ruiz", "Diaz", "Moreno",
        "Alonso", "Romero", "Navarro", "Torres", "Dominguez", "Vazquez", "Ramos", "Gil", "Serrano", "Blanco"
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

    public static void SeedPlayers(FootballDbContext db)
    {
        var random = new Random();

        var squad = new (int Number, POSITION Position)[]
        {
            (1, POSITION.Goalkeeper),
            (2, POSITION.Defender), (3, POSITION.Defender), (4, POSITION.Defender), (5, POSITION.Defender),
            (6, POSITION.Midfielder), (8, POSITION.Midfielder), (10, POSITION.Midfielder), (14, POSITION.Midfielder),
            (7, POSITION.Forward), (9, POSITION.Forward), (11, POSITION.Forward)
        };

        foreach (var team in db.Teams.ToList())
        {
            // თუ გუნდს უკვე ჰყავს მოთამაშეები, გამოვტოვებთ
            if (db.Players.Any(p => p.TeamId == team.Id)) continue;

            var usedNames = new HashSet<string>();

            foreach (var (number, position) in squad)
            {
                string name;
                do
                {
                    name = $"{FirstNames[random.Next(FirstNames.Length)]} {LastNames[random.Next(LastNames.Length)]}";
                }
                while (!usedNames.Add(name));

                db.Players.Add(new Player
                {
                    Name = name,
                    Number = number,
                    Position = position,
                    BirhDate = GetRandomDate(random),
                    TeamId = team.Id
                });
            }
        }

        db.SaveChanges();
    }

    
    private static DateTime GetRandomDate(Random random)
    {
        var start = new DateTime(1985, 1, 1);
        int range = (new DateTime(2005, 1, 1) - start).Days;
        return start.AddDays(random.Next(range));
    }
}