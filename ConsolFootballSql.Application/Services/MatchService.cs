using ConsolFootballSql.Application.DTOs;
using ConsolFootballSql.Application.Interfaces;
using ConsolFootballSql.Application.InterFaces;
using ConsolFootballSql.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace ConsolFootballSql.Application.Service
{
    public class MatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IPlayerRepository _playerRepository;

        public MatchService(IMatchRepository matchRepository, ITeamRepository teamRepository, IPlayerRepository playerRepository)
        {
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
        }


        public List<Match> GetAll() => _matchRepository.GetALL();

        public void Create(CreateMatchDto createMatchDto)
        {
            var homeTeam = _teamRepository.GetById(createMatchDto.HomeTeamId);
            var awayTeam = _teamRepository.GetById(createMatchDto.AwayTeamId);
            if (homeTeam == null || awayTeam == null)
            {
                throw new ArgumentException("One or both teams not found.");
            }
            if (createMatchDto.HomeTeamId == createMatchDto.AwayTeamId)
            {
                throw new ArgumentException("A team cannot play against itself.");
            }
            _matchRepository.Add(new Match
            {
                HomeTeamId = createMatchDto.HomeTeamId,
                AwayTeamId = createMatchDto.AwayTeamId,
                MatchDate = createMatchDto.MatchDate
            });
        }

        public void AddGoal(AddGoalDto addGoalDto)
        {
            var match = _matchRepository.GetById(addGoalDto.MatchId);
            if (match == null)
            {
                throw new ArgumentException("Match not found.");
            }
            var player = _playerRepository.GetById(addGoalDto.PlayerId);
            if (player == null)
            {
                throw new ArgumentException("Player not found.");
            }
            if (player.TeamId != match.HomeTeamId && player.TeamId != match.AwayTeamId)
            {
                throw new ArgumentException("Player does not belong to either team in the match.");
            }

            match.Goals.Add(new Goal
            {
                PlayerId = addGoalDto.PlayerId,
                Minute = addGoalDto.Minute
            });
            if (player.TeamId == match.HomeTeamId)
            {
                match.HomeTeamScore++;
            }
            else
            {
                match.AwayTeamScore++;
            }


            _matchRepository.Update(match);




        }


    }
}
