using QRCoder;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Application.DTOs.Reservation;
using FitnessSystem.Application.DTOs.Session;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using System.Drawing;  
using System.IO;

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
            var existingSession = _unitOfWork.Sessions.GetAll("Trainer,Room,TrainingProgram")
                .Where(s => s.TrainerJMBG == session.TrainerJMBG
                    && s.Date.Date == session.Date.Date
                    && s.Time == session.Time)
                .FirstOrDefault();

            if (existingSession != null)
            {
                throw new InvalidOperationException("Trainer is already busy in that time");
            }

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

        public async Task<List<SessionDto>> GetAllAsync(
        string filterBy = null,
        string filterValue = null,
        string sortBy = "Time",
        bool ascending = true,
        int pageNumber = 1,
        int pageSize = 10) 
        {
            var query = _unitOfWork.Sessions.GetAll("Trainer,Room,TrainingProgram");

            
            if (!string.IsNullOrWhiteSpace(filterBy) && !string.IsNullOrWhiteSpace(filterValue))
            {
                if (filterBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    if (DateTime.TryParse(filterValue, out DateTime filterDate))
                    {
                        query = query.Where(s => s.Date.Date == filterDate.Date);
                    }
                }
                else if (filterBy.Equals("TrainerJMBG", StringComparison.OrdinalIgnoreCase))
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
                if (sortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
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

        public async Task<List<SessionDto>> GetSessionsByTrainerJmbgAsync(string trainerJmbg)
        {
            var now = DateTime.Now; 

            var allSessions = _unitOfWork.Sessions.GetAll("Trainer,Room,TrainingProgram")
                .Where(s => s.Trainer.JMBG == trainerJmbg)
                .ToList(); 

            
            var filteredSessions = allSessions
                .Where(s =>
                {
                    
                    var sessionDateTime = s.Date.Date + s.Time;

                   
                    return sessionDateTime > now ||
                           (sessionDateTime.Date == now.Date && sessionDateTime.TimeOfDay >= now.TimeOfDay);
                })
                .OrderBy(s => s.Date.Date + s.Time) 
                .ToList();

            return _mapper.Map<List<SessionDto>>(filteredSessions);
        }



        public async Task<SessionDto> UpdateSessionAsync(int id, SessionUpdateDto sessionUpdateDto)
        {
            var session = await _unitOfWork.Sessions.GetByIdAsync(id);
            if (session == null)
            {
                throw new KeyNotFoundException("Session not found.");
            }

            session.Capacity = sessionUpdateDto.Capacity;
            session.Duration = sessionUpdateDto.Duration;
            session.Date = sessionUpdateDto.Date;
            session.TrainerJMBG = sessionUpdateDto.TrainerJMBG;
            session.Time = sessionUpdateDto.Time;
            
            await _unitOfWork.Sessions.UpdateAsync(session, id);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<SessionDto>(session);
        }

        public async Task<byte[]> GenerateQrCodeAsync(int sessionId)
        {
            var session = await _unitOfWork.Sessions.GetByIdAsync(sessionId);
            if (session == null)
            {
                throw new KeyNotFoundException("Session not found.");
            }

            var qrData = $"{session.SessionId}";


            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q);

          
                using (var qrCode = new PngByteQRCode(qrCodeData))
                {
                    return qrCode.GetGraphic(20);  
                }
            }
        }

    }
}
