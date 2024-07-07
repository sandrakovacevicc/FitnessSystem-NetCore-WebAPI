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

        public async Task<List<SessionDto>> GetAllAsync(string filterBy = null,string filterValue = null,string sortBy = null,bool ascending = true,int pageNumber = 1,int pageSize = 10)
        {
            var query = _sessionRepository.GetAll("Trainer,Room,TrainingProgram");


            if (string.IsNullOrWhiteSpace(filterBy) == false && string.IsNullOrWhiteSpace(filterValue) == false)
            {
                if (filterBy.Equals("TrainerJMBG", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(r => r.Trainer.JMBG.Contains(filterValue));
                }
                else if (filterBy.Equals("RoomId", StringComparison.OrdinalIgnoreCase))
                {
                    if (int.TryParse(filterValue, out int roomId))
                    {
                        query = query.Where(s => s.RoomId == roomId);
                    }
                }
            }


            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy == "Date")
                {
                    query = ascending ? query.OrderBy(s => s.Date) : query.OrderByDescending(s => s.Date);
                }
                else if (sortBy.Equals("Time", StringComparison.OrdinalIgnoreCase))
                {
                    query = ascending ? query.OrderBy(s => s.Time) : query.OrderByDescending(s => s.Time);
                }

            }

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var sessions =  query.ToList();
            var sessionsDto = _mapper.Map<List<SessionDto>>(sessions);
            return sessionsDto;
        }

        public async Task<SessionDto> GetByIdAsync(int id)
        {
            var session = await _sessionRepository.GetByIdAsync(id);
            return _mapper.Map<SessionDto>(session);
        }

        public async Task<SessionDto> UpdateSessionAsync(int id, SessionUpdateDto sessionUpdateDto)
        {
            var session = await _sessionRepository.GetByIdAsync(id);
            if (session == null)
            {
                throw new KeyNotFoundException("Session not found.");
            }

            session.Duration = sessionUpdateDto.Duration;
            session.Date = sessionUpdateDto.Date;
            session.Time = sessionUpdateDto.Time;
            session.Capacity = sessionUpdateDto.Capacity;
            session.RoomId = sessionUpdateDto.RoomId;
            session.TrainerJMBG = sessionUpdateDto.TrainerJMBG;
            session.TrainingProgramId = sessionUpdateDto.TrainingProgramId;

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _sessionRepository.UpdateAsync(session, id);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                var sessionDto = _mapper.Map<SessionDto>(session);

                return sessionDto;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
