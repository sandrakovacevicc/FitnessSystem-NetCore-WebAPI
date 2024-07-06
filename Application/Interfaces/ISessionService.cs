using FitnessSystem.Application.DTOs;
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
        Task<List<SessionDto>> GetAllAsync();
        Task<SessionDto> GetByIdAsync(int id);
        Task<SessionAddDto> CreateSessionAsync(SessionAddDto sessionAddDto);
        Task<SessionDeleteDto> DeleteSessionAsync(int id);
        Task<SessionDto> UpdateSessionAsync(int id, SessionUpdateDto sessionUpdateDto);
    }
}
