using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolFootballSql.Application.DTOs
{
    public record AddGoalDto(int MatchId, int PlayerId, int Minute);

    
}
