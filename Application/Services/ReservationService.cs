using AutoMapper;
using Core.Entities;
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
            var reservations = _reservationRepository.GetAll("Session.Room,Session.TrainingProgram,Session.Clients").ToList();

            var reservationsDto = reservations.Select(reservation => new ReservationDto
            {
                Date = reservation.Date,
                Time = reservation.Time,
                Status = reservation.Status,
                Client = _mapper.Map<ClientDto>(reservation.Client), 
                Session = _mapper.Map<SessionDto>(reservation.Session) 
            }).ToList();



            // var reservationsDto = _mapper.Map<List<ReservationDto>>(reservations);

            return reservationsDto;
        }

        public async Task<ReservationDto> GetByIdAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            return _mapper.Map<ReservationDto>(reservation);
        }
    }
}
