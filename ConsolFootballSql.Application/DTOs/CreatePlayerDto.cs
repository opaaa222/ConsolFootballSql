using ConsolFootballSql.Domain.Models.ENUM;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolFootballSql.Application.DTOs
{
    public record CreatePlayerDto(string Name, string Number, POSITION Position, int TeamId, DateTime DateOfBirth);
}
