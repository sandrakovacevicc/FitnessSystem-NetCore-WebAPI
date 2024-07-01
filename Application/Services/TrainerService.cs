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
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMapper _mapper;

        public TrainerService(ITrainerRepository trainerRepository, IMapper mapper)
        {
            _trainerRepository = trainerRepository;
            _mapper = mapper;

        }

        public async Task<List<TrainerDto>> GetAllAsync()
        {
            var trainers = await _trainerRepository.GetAllAsync();

            var trainersDto = _mapper.Map<List<TrainerDto>>(trainers);

            return trainersDto;

        }
    }
}
