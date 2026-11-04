using ConsolFootballSql.Application.InterFaces;
using ConsolFootballSql.Domain.Models.Entities;
using ConsolFootballSql.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolFootballSql.Infrastructure.Repositories
{
    public  class TeamRepository : ITeamRepository
    {

       private readonly FootballDbContext _context;
        public TeamRepository(FootballDbContext context) => _context = context;
         
        public List<Team> GetALL() => _context.Teams.OrderBy(t => t.Name).ToList();
        public Team GetById(int id) => _context.Teams.Find(id);
        public bool exists(string name) => _context.Teams.Any(t => t.Name == name.Trim());
        public void Add(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
        }
        public void Update(Team team)
        {
            _context.Teams.Update(team);
            _context.SaveChanges();
        }
        public void Delete(Team team)
        {
            _context.Teams.Remove(team);
            _context.SaveChanges();
        }


    }
}
