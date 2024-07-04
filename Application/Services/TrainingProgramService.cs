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
    public class TrainingProgramService : ITrainingProgramService
    {
        private readonly ITrainingProgramRepository _programRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TrainingProgramService(ITrainingProgramRepository programRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _programRepository = programRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TrainingProgramDto> CreateTrainingProgramAsync(TrainingProgramDto trainingProgramDto)
        {
            var trainingProgram = _mapper.Map<TrainingProgram>(trainingProgramDto);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _programRepository.CreateAsync(trainingProgram);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }

            return _mapper.Map<TrainingProgramDto>(trainingProgram);
        }

        public async Task<List<TrainingProgramDto>> GetAllAsync()
        {
            var programs =  _programRepository.GetAll().ToList();
            
            var programsDto = _mapper.Map<List<TrainingProgramDto>>(programs);

            return programsDto;
        }

        public async Task<TrainingProgramDto> GetByIdAsync(int id)
        {
            var program = await _programRepository.GetByIdAsync(id);
            return _mapper.Map<TrainingProgramDto>(program);
        }
    }
}
