using ConsolFootballSql.Application.InterFaces;
using ConsolFootballSql.Domain.Models.Entities;
using ConsolFootballSql.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolFootballSql.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {


        private readonly FootballDbContext _context;
        public PlayerRepository(FootballDbContext context) => _context = context;
        public List<Player> GetALL() => _context.Players.OrderBy(p => p.Name).ToList();
        public List<Player> GetByTeamId(int teamId) => _context.Players.Include(p => p.Team).Where(p => p.TeamId == teamId).ToList();
        public Player? GetById(int id) => _context.Players.Find(id);
        public bool NumberTaken(int number, int teamId) => _context.Players.Any(p => p.Number == number && p.TeamId == teamId);

        public List<Player> GetTopScorers() => _context.Players.Include(p => p.Team).
            Include(p => p.Goals).ToList();

        public void Add(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();
        }
        public void Update(Player player)
        {
            _context.Players.Update(player);
            _context.SaveChanges();
        }
        public void Delete(Player player)
        {
            _context.Players.Remove(player);
            _context.SaveChanges();
        }
    }
}
