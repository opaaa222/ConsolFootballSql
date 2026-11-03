using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolFootballSql.Domain.Models.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime MatchDate { get; set; }
        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }
        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }

        public ICollection<Goal> Goals { get; set; } = new List<Goal>();



    }
}
