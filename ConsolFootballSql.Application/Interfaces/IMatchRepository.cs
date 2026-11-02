using ConsolFootballSql.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolFootballSql.Application.Interfaces
{
    public interface IMatchRepository
    {
        List<Match> GetALL();
        Match GetById(int id);
        void Add(Match match);
        void Update(Match match);






    }
}
