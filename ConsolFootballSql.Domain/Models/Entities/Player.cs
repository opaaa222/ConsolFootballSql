using ConsolFootballSql.Domain.Models.ENUM;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolFootballSql.Domain.Models.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; } =string.Empty;
        public int Number { get; set; } 
        public DateTime BirhDate { get; set; }
        public POSITION Position { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; } = null!;

        public ICollection<Goal> Goals { get; set; } = new List<Goal>();

    }
}
