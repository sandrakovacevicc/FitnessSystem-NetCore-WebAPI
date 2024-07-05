﻿using FitnessSystem.Application.DTOs;
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
        Task<TrainingProgramDto> CreateTrainingProgramAsync(TrainingProgramDto trainingProgramDto);
        Task<TrainingProgramDeleteDto> DeleteTrainingProgramAsync(int id);
        Task<TrainingProgramDeleteDto> UpdateTrainingProgramAsync(int id, TrainingProgramDto trainingProgramDto);
    }
}
