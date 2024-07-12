using AutoMapper;
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

        public async Task<TrainingProgramDto> CreateTrainingProgramAsync(TrainingProgramDto trainingProgramDto)
        {
            var trainingProgram = _mapper.Map<TrainingProgram>(trainingProgramDto);
            await _unitOfWork.TrainingPrograms.CreateAsync(trainingProgram);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<TrainingProgramDto>(trainingProgram);
        }

        public async Task<TrainingProgramDeleteDto> DeleteTrainingProgramAsync(int id)
        {
            var program = await _unitOfWork.TrainingPrograms.GetByIdAsync(id);
            if (program == null)
            {
                return null;
            }

            await _unitOfWork.TrainingPrograms.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<TrainingProgramDeleteDto>(program);
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
