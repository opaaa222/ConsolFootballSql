using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolFootballSql.Domain.Models.Entities
{
    public class Team
    {
     public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int FoundedYear { get; set; }

        public ICollection<Player> Players { get; set; } = new List<Player>();
        public ICollection<Match> HomeMatches { get; set; } = new List<Match>();
        public ICollection<Match> AwayMatches { get; set; } = new List<Match>();


        // public Team(string name, string city, int foundedYear)
        // ise satesto aris 

    }
}
