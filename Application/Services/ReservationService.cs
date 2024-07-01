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
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public ReservationService(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;

        }

        public async Task<List<ReservationDto>> GetAllAsync()
        {
            var reservations = await _reservationRepository.GetAllAsync();

            var reservationsDto = _mapper.Map<List<ReservationDto>>(reservations);

            return reservationsDto;
        }
    }
}
