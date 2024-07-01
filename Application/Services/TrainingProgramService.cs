using AutoMapper;
using Core.Interfaces;
using FitnessSystem.Application.DTOs;
using FitnessSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Services
{
    public class TrainingProgramService : ITrainingProgramService
    {
        private readonly ITrainingProgramRepository _programRepository;
        private readonly IMapper _mapper;

        public TrainingProgramService(ITrainingProgramRepository programRepository, IMapper mapper)
        {
            _programRepository = programRepository;
            _mapper = mapper;
        }
        public async Task<List<TrainingProgramDto>> GetAllAsync()
        {
            var programs = await _programRepository.GetAllAsync();
            
            var programsDto = _mapper.Map<List<TrainingProgramDto>>(programs);

            return programsDto;
        }
    }
}
