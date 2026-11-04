using ConsolFootballSql.Application.Interfaces;
using ConsolFootballSql.Domain.Models.Entities;
using ConsolFootballSql.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace ConsolFootballSql.Infrastructure.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly FootballDbContext _context;
        public MatchRepository(FootballDbContext context) => _context = context;

        public List<Match> GetALL() => _context.Matches.Include(m => m.HomeTeam).Include(m => m.AwayTeam).Include(m => m.Goals).ThenInclude(g => g.Player).OrderBy(m => m.MatchDate).ToList();

        public Match? GetById(int id) => _context.Matches.Include(m => m.Goals).FirstOrDefault(m => m.Id == id);

        public void Add(Match match)
        {
            _context.Matches.Add(match);
            _context.SaveChanges();
        }
        public void Update(Match match)
        {
            _context.Matches.Update(match);
            _context.SaveChanges();
        }







    }
}
