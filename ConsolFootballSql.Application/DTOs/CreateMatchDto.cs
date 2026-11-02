using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolFootballSql.Application.DTOs
{
    record CreateMatchDto(int HomeTeamId, int AwayTeamId, DateTime MatchDate);
}
