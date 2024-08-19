using FitnessSystem.Application.DTOs.TrainingProgram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Interfaces
{
    public interface ITrainingProgramService
    {
        Task<List<TrainingProgramDto>> GetAllAsync();
        Task<TrainingProgramDto> GetByIdAsync(int id);
        Task<TrainingProgramDto> UpdateTrainingProgramAsync(int id, TrainingProgramDto trainingProgramDto);
        Task<IEnumerable<TrainingProgramDto>> SearchProgramsAsync(string searchTerm);
    }
}
