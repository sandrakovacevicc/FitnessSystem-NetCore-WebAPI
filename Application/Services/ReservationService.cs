using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Application.DTOs.Reservation;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ReservationService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ReservationAddDto> CreateReservationAsync(ReservationAddDto reservationAddDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationAddDto);
            await _unitOfWork.Reservations.CreateAsync(reservation);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ReservationAddDto>(reservation);
        }

        public async Task<ReservationDeleteDto> DeleteReservationAsync(int id)
        {
            var reservation = await _unitOfWork.Reservations.GetByIdAsync(id);
            if (reservation == null)
            {
                return null;
            }

            await _unitOfWork.Reservations.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ReservationDeleteDto>(reservation);
        }

        public async Task<List<ReservationDto>> GetAllAsync(string? filterBy = null, string? filterValue = null, string? sortBy = null, bool ascending = true, int pageNumber = 1, int pageSize = 10)
        {
            var query = _unitOfWork.Reservations.GetAll("Session,Session.Trainer,Session.Room,Session.TrainingProgram,Client,Client.MembershipPackage");

            if (string.IsNullOrWhiteSpace(filterBy) == false && string.IsNullOrWhiteSpace(filterValue) == false)
            {
                if (filterBy.Equals("ClientJMBG", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(r => r.Client.JMBG.Contains(filterValue));
                }
                else if (filterBy.Equals("Status", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(r => r.Status == filterValue);
                }
            }


            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    query = ascending ? query.OrderBy(r => r.Date) : query.OrderByDescending(r => r.Date);
                }
                else if (sortBy.Equals("Time", StringComparison.OrdinalIgnoreCase))
                {
                    query = ascending ? query.OrderBy(r => r.Time) : query.OrderByDescending(r => r.Time);
                }
            }
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var reservations =  query.ToList();
            return _mapper.Map<List<ReservationDto>>(reservations);
        }

        public async Task<List<ReservationDto>> GetReservationsByClientJmbgAsync(string clientJmbg)
        {
            var reservations = _unitOfWork.Reservations.GetAll()
                .Where(r => r.Client.JMBG == clientJmbg)
                .ToList();

            return _mapper.Map<List<ReservationDto>>(reservations);
        }

        public async Task<ReservationDto> GetByIdAsync(int id)
        {
            var reservation = await _unitOfWork.Reservations.GetByIdAsync(id);
            return _mapper.Map<ReservationDto>(reservation);
        }

        public async Task<ReservationDto> UpdateReservationAsync(int id, ReservationUpdateDto reservationUpdateDto)
        {
            var reservation = await _unitOfWork.Reservations.GetByIdAsync(id);
            if (reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found.");
            }

            reservation.Status = reservationUpdateDto.Status;
            reservation.Date = reservationUpdateDto.Date;
            reservation.SessionId = reservationUpdateDto.SessionId;
            reservation.ClientJMBG = reservationUpdateDto.ClientJMBG;
            reservation.Time = reservationUpdateDto.Time;
            await _unitOfWork.Reservations.UpdateAsync(reservation, id);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ReservationDto>(reservation);
        }
    }
}
