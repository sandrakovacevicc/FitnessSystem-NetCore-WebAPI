﻿using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Application.DTOs.TrainingProgram;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Services
{
    public class TrainingProgramService : ITrainingProgramService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TrainingProgramService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TrainingProgramDto>> GetAllAsync()
        {
            var programs = _unitOfWork.TrainingPrograms.GetAll().ToList();
            return _mapper.Map<List<TrainingProgramDto>>(programs);
        }

        public async Task<TrainingProgramDto> GetByIdAsync(int id)
        {
            var program = await _unitOfWork.TrainingPrograms.GetByIdAsync(id);
            return _mapper.Map<TrainingProgramDto>(program);
        }

        public async Task<IEnumerable<TrainingProgramDto>> SearchProgramsAsync(string searchTerm)
        {
            var programs = await _unitOfWork.TrainingPrograms.SearchProgramsAsync(searchTerm);
            return _mapper.Map<IEnumerable<TrainingProgramDto>>(programs);
        }

        public async Task<TrainingProgramDto> UpdateTrainingProgramAsync(int id, TrainingProgramDto trainingProgramDto)
        {
            var program = await _unitOfWork.TrainingPrograms.GetByIdAsync(id);
            if (program == null)
            {
                throw new KeyNotFoundException("Training program not found.");
            }

            _mapper.Map(trainingProgramDto, program);
            await _unitOfWork.TrainingPrograms.UpdateAsync(program, id);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<TrainingProgramDto>(program);
        }
    }
}
