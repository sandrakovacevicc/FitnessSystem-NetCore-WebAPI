using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Application.DTOs.Reservation;
using FitnessSystem.Application.Interfaces;
using FitnessSystem.Core.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;

        public ReservationService(IReservationRepository reservationRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ReservationAddDto> CreateReservationAsync(ReservationAddDto reservationAddDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationAddDto);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _reservationRepository.CreateAsync(reservation);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }

            return _mapper.Map<ReservationAddDto>(reservation);
        }

        public async Task<ReservationDeleteDto> DeleteReservationAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
            {
                return null;
            }

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var deletedReservation = await _reservationRepository.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<ReservationDeleteDto>(deletedReservation);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<List<ReservationDto>> GetAllAsync(string? filterBy = null,string? filterValue = null,string? sortBy = null,bool ascending = true,int pageNumber = 1,int pageSize = 10)
        {
            var query = _reservationRepository.GetAll("Session,Session.Trainer,Session.Room,Session.TrainingProgram,Client,Client.MembershipPackage");

            
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

            var reservations = query.ToList();
            var reservationsDto = _mapper.Map<List<ReservationDto>>(reservations);
            return reservationsDto;
        }

        public async Task<List<ReservationDto>> GetReservationsByClientJmbgAsync(string clientJmbg)
        {
            var reservations = _reservationRepository.GetAll("Session,Session.Trainer,Session.Room,Session.TrainingProgram,Client,Client.MembershipPackage")
                .Where(r => r.Client.JMBG == clientJmbg);

            var reservationsDto = _mapper.Map<List<ReservationDto>>(reservations);
            return reservationsDto;
        }



        public async Task<ReservationDto> GetByIdAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            return _mapper.Map<ReservationDto>(reservation);
        }

        public async Task<ReservationDto> UpdateReservationAsync(int id, ReservationUpdateDto reservationUpdateDto)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found.");
            }

            reservation.Status = reservationUpdateDto.Status;
            reservation.Date = reservationUpdateDto.Date;
            reservation.SessionId = reservationUpdateDto.SessionId;
            reservation.ClientJMBG = reservationUpdateDto.ClientJMBG;
            reservation.Time = reservationUpdateDto.Time;

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _reservationRepository.UpdateAsync(reservation, id);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                var reservationDto = _mapper.Map<ReservationDto>(reservation);

                return reservationDto;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
