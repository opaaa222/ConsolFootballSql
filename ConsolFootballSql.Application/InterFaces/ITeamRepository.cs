
using ConsolFootballSql.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolFootballSql.Application.InterFaces
{
    public interface ITeamRepository
    {
        List<Team> GetALL();
        Team? GetById(int id);
        bool exists(string Name);
        void Add(Team team);
        void Update(Team team);
        void Delete(Team team);

    }
}
