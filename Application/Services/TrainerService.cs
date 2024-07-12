using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Application.DTOs.Trainer;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TrainerService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TrainerAddDto> CreateTrainerAsync(TrainerAddDto trainerAddDto)
        {
            var trainer = _mapper.Map<Trainer>(trainerAddDto);
            await _unitOfWork.Trainers.CreateAsync(trainer);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<TrainerAddDto>(trainer);
        }

        public async Task<TrainerDeleteDto> DeleteTrainerAsync(string jmbg)
        {
            var trainer = await _unitOfWork.Trainers.GetByIdAsync(jmbg);
            if (trainer == null)
            {
                return null;
            }

            await _unitOfWork.Trainers.DeleteAsync(jmbg);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<TrainerDeleteDto>(trainer);
        }

        public async Task<List<TrainerDto>> GetAllAsync()
        {
            var trainers = _unitOfWork.Trainers.GetAll().ToList();
            return _mapper.Map<List<TrainerDto>>(trainers);
        }

        public async Task<TrainerDto> GetByIdAsync(string jmbg)
        {
            var trainer = await _unitOfWork.Trainers.GetByIdAsync(jmbg);
            return _mapper.Map<TrainerDto>(trainer);
        }

        public async Task<TrainerAddDto> UpdateTrainerAsync(string jmbg, TrainerUpdateDto trainerUpdateDto)
        {
            var trainer = await _unitOfWork.Trainers.GetByIdAsync(jmbg);
            if (trainer == null)
            {
                throw new KeyNotFoundException("Trainer not found.");
            }

            _mapper.Map(trainerUpdateDto, trainer);
            await _unitOfWork.Trainers.UpdateAsync(trainer, jmbg);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<TrainerAddDto>(trainer);
        }
    }
}
