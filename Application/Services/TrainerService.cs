﻿using AutoMapper;
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

        public async Task<TrainerDeleteDto> DeleteTrainerAsync(string jmbg)
        {
            var trainer = await _trainerRepository.GetByIdAsync(jmbg);
            if (trainer == null)
            {
                return null;
            }

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var deletedTrainer = await _trainerRepository.DeleteAsync(jmbg);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<TrainerDeleteDto>(deletedTrainer);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<List<TrainerDto>> GetAllAsync()
        {
            var trainers =  _trainerRepository.GetAll().ToList();

            var trainersDto = _mapper.Map<List<TrainerDto>>(trainers);

            return trainersDto;

        }

        public async Task<TrainerDto> GetByIdAsync(string jmbg)
        {
            var trainer = await _trainerRepository.GetByIdAsync(jmbg);
            return _mapper.Map<TrainerDto>(trainer);
        }

        public async Task<TrainerAddDto> UpdateTrainerAsync(string jmbg, TrainerUpdateDto trainerUpdateDto)
        {
            var trainer = await _trainerRepository.GetByIdAsync(jmbg);
            if (trainer == null)
            {
                throw new KeyNotFoundException("Trainer not found.");
            }

            trainer.Name = trainerUpdateDto.Name;
            trainer.Surname = trainerUpdateDto.Surname;
            trainer.Email = trainerUpdateDto.Email;
            trainer.Specialty = trainerUpdateDto.Specialty;

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _trainerRepository.UpdateAsync(trainer, jmbg);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                var trainerDto = new TrainerAddDto
                {
                    JMBG = trainer.JMBG,
                    Name = trainer.Name,
                    Surname = trainer.Surname,
                    Email = trainer.Email,
                    Specialty = trainer.Specialty,
                };

                return trainerDto;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
