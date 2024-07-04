using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Application.DTOs;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Core.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(ISessionRepository sessionRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _sessionRepository = sessionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<SessionAddDto> CreateSessionAsync(SessionAddDto sessionAddDto)
        {
            var session = _mapper.Map<Session>(sessionAddDto);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _sessionRepository.CreateAsync(session);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }

            return _mapper.Map<SessionAddDto>(session);
        }

        public async Task<SessionDeleteDto> DeleteSessionAsync(int id)
        {
            var session = await _sessionRepository.GetByIdAsync(id);
            if (session == null)
            {
                return null;
            }

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var deletedSession = await _sessionRepository.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<SessionDeleteDto>(deletedSession);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
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
