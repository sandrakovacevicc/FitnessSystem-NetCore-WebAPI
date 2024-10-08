﻿using Core.Entities;
using FitnessSystem.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITrainingProgramRepository : IRepository<TrainingProgram>
    {
        Task<TrainingProgram> GetByIdAsync(int id);
        Task<IEnumerable<TrainingProgram>> SearchProgramsAsync(string searchTerm);

    }
}
