namespace ConsolFootballSql.Domain.Models.Entities
{
    public class Goal
    {
         public int Id { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; } = null!;
        public int MatchId { get; set; }
        public Match Match { get; set; } = null!;
        public int Minute { get; set; }



    }
}
