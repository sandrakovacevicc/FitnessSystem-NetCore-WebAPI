using AutoMapper;
using Core.Interfaces;
using FitnessSystem.Application.DTOs;
using FitnessSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IMapper _mapper;

        public SessionService(ISessionRepository sessionRepository, IMapper mapper)
        {
            _sessionRepository = sessionRepository;
            _mapper = mapper;

        }

        public async Task<List<SessionDto>> GetAllAsync()
        {
            var sessions =  _sessionRepository.GetAll("Trainer,Room,TrainingProgram").ToList();

            var sessionsDto = _mapper.Map<List<SessionDto>>(sessions);

            return sessionsDto;

        }

        public async Task<SessionDto> GetByIdAsync(int id)
        {
            var session = await _sessionRepository.GetByIdAsync(id);
            return _mapper.Map<SessionDto>(session);
        }
    }
}
