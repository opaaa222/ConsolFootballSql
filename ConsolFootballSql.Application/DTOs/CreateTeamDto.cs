using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolFootballSql.Application.DTOs
{
    public record CreateTeamDto(string Name, string City, int FoundationYear);
}
