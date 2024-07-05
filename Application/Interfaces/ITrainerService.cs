using FitnessSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Interfaces
{
    public interface ITrainerService
    {
        Task<List<TrainerDto>> GetAllAsync();
        Task<TrainerDto> GetByIdAsync(string JMBG);
        Task<TrainerAddDto> CreateTrainerAsync(TrainerAddDto trainerAddDto);
        Task<TrainerDeleteDto> DeleteTrainerAsync(string JMBG);
    }
}
