using ConsolFootballSql.Application.DTOs;
using ConsolFootballSql.Application.InterFaces;
using ConsolFootballSql.Domain.Models.Entities;
using ConsolFootballSql.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolFootballSql.Application.Services
{
    public  class TeamService
    {
        private readonly ITeamRepository _teamRepository;
        public TeamService(ITeamRepository teamRepository) => _teamRepository = teamRepository;
      
        public List<Team> GetAll() => _teamRepository.GetALL();


        public  void Create(CreateTeamDto createTeamDto)
        { 
            if (string.IsNullOrEmpty(createTeamDto.Name))
            {
                throw new ArgumentException("Team name cannot be null or empty.");

            }
            if(createTeamDto.FoundationYear <1850 || createTeamDto.FoundationYear > DateTime.Now.Year)
            {
                throw new ArgumentException("Invalid foundation year.");
            }
            if (_teamRepository.exists(createTeamDto.Name))
            {
                throw new ArgumentException("Team name already exists.");
            }

            _teamRepository.Add(new Team
            {
                Name = createTeamDto.Name,
                City = createTeamDto.City,
                FoundedYear = createTeamDto.FoundationYear
            });


        }

        public void Delete(int id)
        {
            var team = _teamRepository.GetById(id);
            if (team == null)
            {
                throw new ArgumentException("Team not found.");
            }
            _teamRepository.Delete(team);
        }


    }
}
