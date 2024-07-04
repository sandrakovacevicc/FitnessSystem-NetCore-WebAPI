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
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TrainerService(ITrainerRepository trainerRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _trainerRepository = trainerRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }

        public async Task<TrainerAddDto> CreateTrainerAsync(TrainerAddDto trainerAddDto)
        {
            var trainer = _mapper.Map<Trainer>(trainerAddDto);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _trainerRepository.CreateAsync(trainer);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }

            return _mapper.Map<TrainerAddDto>(trainer);
        }

        public async Task<List<TrainerDto>> GetAllAsync()
        {
            var trainers =  _trainerRepository.GetAll().ToList();

            var trainersDto = _mapper.Map<List<TrainerDto>>(trainers);

            return trainersDto;

        }

        public async Task<TrainerDto> GetByIdAsync(int id)
        {
            var trainer = await _trainerRepository.GetByIdAsync(id);
            return _mapper.Map<TrainerDto>(trainer);
        }
    }
}
