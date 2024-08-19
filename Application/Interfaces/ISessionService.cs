using FitnessSystem.Application.DTOs.Reservation;
using FitnessSystem.Application.DTOs.Session;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Interfaces
{
    public interface ISessionService
    {
        Task<List<SessionDto>> GetAllAsync(string filterBy = null, string filterValue = null, string sortBy = null, bool ascending = true, int pageNumber = 1, int pageSize = 10);
        Task<SessionDto> GetByIdAsync(int id);
        Task<SessionAddDto> CreateSessionAsync(SessionAddDto sessionAddDto);
        Task<SessionDeleteDto> DeleteSessionAsync(int id);
        Task<SessionDto> UpdateSessionAsync(int id, SessionUpdateDto sessionUpdateDto);
        Task<List<SessionDto>> GetSessionsByTrainerJmbgAsync(string trainerJmbg);
        Task<byte[]> GenerateQrCodeAsync(int sessionId);
    }
}
