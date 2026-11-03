using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolFootballSql.Application.DTOs
{
  public  record CreateMatchDto(int HomeTeamId, int AwayTeamId, DateTime MatchDate);
}
