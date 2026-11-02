using ConsolFootballSql.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolFootballSql.Application.InterFaces
{
    public  interface IPlayerRepository
    {
        List<Player> GetALL();
        List<Player> GetByTeamId(int teamId);
        Player? GetById(int id);
        bool NumberTaken(string number, int teamId);
        void Add(Player player);
        void Update(Player player);
        void Delete(Player player);



    }
}
