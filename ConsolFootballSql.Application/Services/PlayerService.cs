using ConsolFootballSql.Application.DTOs;
using ConsolFootballSql.Application.InterFaces;
using ConsolFootballSql.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolFootballSql.Application.Service
{
    public class PlayerService
    {

        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;

        public PlayerService(IPlayerRepository playerRepository, ITeamRepository teamRepository)
        {
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
        }


        public List<Player> GetAll() => _playerRepository.GetALL();
        public List<Player> GetByTeamId(int teamId) => _playerRepository.GetByTeamId(teamId);


        public void Create(CreatePlayerDto createPlayerDto)
        {
            if (string.IsNullOrEmpty(createPlayerDto.Name))
            {
                throw new ArgumentException("Player name cannot be null or empty.");
            }
            if (createPlayerDto.DateOfBirth >= DateTime.Now.AddYears(-16) || createPlayerDto.DateOfBirth <= DateTime.Now.AddYears(-50)  )
            {
                throw new ArgumentException("Invalid player age.");
            }
            var team = _teamRepository.GetById(createPlayerDto.TeamId);
            if (team == null)
            {
                throw new ArgumentException("Team not found.");
            }
            if (_playerRepository.NumberTaken(createPlayerDto.Number, createPlayerDto.TeamId))
            {
                throw new ArgumentException("Player number already taken in the team.");
            }
            _playerRepository.Add(new Player
            {
                Name = createPlayerDto.Name,
                Number = createPlayerDto.Number,
                Position = createPlayerDto.Position,
                TeamId = createPlayerDto.TeamId,
                BirhDate = createPlayerDto.DateOfBirth
            });


            



        }

        public void Delete(int playerId)
        {
            var player = _playerRepository.GetById(playerId);
            if (player == null)
            {
                throw new ArgumentException("Player not found.");
            }
            _playerRepository.Delete(player);
        }

    }
}
