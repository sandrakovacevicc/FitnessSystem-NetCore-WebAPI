using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Application.DTOs.Session;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Services
{
    public class SessionService : ISessionService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<SessionAddDto> CreateSessionAsync(SessionAddDto sessionAddDto)
        {
            var session = _mapper.Map<Session>(sessionAddDto);
            await _unitOfWork.Sessions.CreateAsync(session);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<SessionAddDto>(session);
        }

        public async Task<SessionDeleteDto> DeleteSessionAsync(int id)
        {
            var session = await _unitOfWork.Sessions.GetByIdAsync(id);
            if (session == null)
            {
                return null;
            }

            await _unitOfWork.Sessions.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<SessionDeleteDto>(session);
        }

        public async Task<List<SessionDto>> GetAllAsync(string filterBy = null, string filterValue = null, string sortBy = null, bool ascending = true, int pageNumber = 1, int pageSize = 10)
        {
            var query = _unitOfWork.Sessions.GetAll("Trainer,Room,TrainingProgram");

            if (!string.IsNullOrWhiteSpace(filterBy) && !string.IsNullOrWhiteSpace(filterValue))
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

            var sessions = query.ToList();
            return _mapper.Map<List<SessionDto>>(sessions);
        }

        public async Task<SessionDto> GetByIdAsync(int id)
        {
            var session = await _unitOfWork.Sessions.GetByIdAsync(id);
            return _mapper.Map<SessionDto>(session);
        }

        public async Task<SessionDto> UpdateSessionAsync(int id, SessionUpdateDto sessionUpdateDto)
        {
            var session = await _unitOfWork.Sessions.GetByIdAsync(id);
            if (session == null)
            {
                throw new KeyNotFoundException("Session not found.");
            }

            _mapper.Map(sessionUpdateDto, session);
            await _unitOfWork.Sessions.UpdateAsync(session, id);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<SessionDto>(session);
        }
    }
}
