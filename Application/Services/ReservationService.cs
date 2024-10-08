﻿using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FitnessSystem.Application.DTOs.Client;
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
            try
            {
                var reservation = _mapper.Map<Reservation>(reservationAddDto);

                Session session = await _unitOfWork.Sessions.GetByIdAsync(reservation.SessionId);
                if (session == null)
                {
                    throw new KeyNotFoundException("Session not found.");
                }

                if (session.Capacity <= 0)
                {
                    throw new InvalidOperationException("No capacity available for the session.");
                }

                session.Capacity--;

                await _unitOfWork.Reservations.CreateAsync(reservation);

                await _unitOfWork.CompleteAsync();

                return _mapper.Map<ReservationAddDto>(reservation);
            }
            catch (KeyNotFoundException ex)
            {
                throw new ArgumentException("Invalid session ID provided.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Cannot create reservation: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while creating the reservation.", ex);
            }
        }


        public async Task<ReservationDeleteDto> DeleteReservationAsync(int id)
        {
            var reservation = await _unitOfWork.Reservations.GetByIdAsync(id);
            if (reservation == null)
            {
                return null;
            }
            var session = await _unitOfWork.Sessions.GetByIdAsync(reservation.SessionId);
            if (session == null)
            {
                return null;
            }
            session.Capacity++;
            await _unitOfWork.Reservations.DeleteAsync(id);

            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ReservationDeleteDto>(reservation);
        }

        public async Task<List<ReservationDto>> GetAllAsync(string? filterBy = null, string? filterValue = null, string? sortBy = null, bool ascending = true, int pageNumber = 1, int pageSize = 10)
        {
            var query = _unitOfWork.Reservations.GetAll("Session,Session.Trainer,Session.Room,Session.TrainingProgram,Client,Client.MembershipPackage");

            if (!string.IsNullOrWhiteSpace(filterBy) && !string.IsNullOrWhiteSpace(filterValue))
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
            return _mapper.Map<List<ReservationDto>>(reservations);
        }

        public async Task<List<ReservationDto>> GetReservationsByClientJmbgAsync(string clientJmbg)
        {
            var reservations = _unitOfWork.Reservations.GetAll("Session,Session.Trainer,Session.Room,Session.TrainingProgram,Client,Client.MembershipPackage")
                .Where(r => r.Client.JMBG == clientJmbg)
                .Where(r => r.Date <= r.Session.Date && r.Session.Date >= DateTime.Now.Date)
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

        public async Task<ReservationDto> ConfirmReservationAsync(int sessionId, string clientJmbg)
        {
            var reservation = _unitOfWork.Reservations.GetAll("Session,Session.Trainer,Session.Room,Session.TrainingProgram,Client,Client.MembershipPackage")
                .FirstOrDefault(r => r.SessionId == sessionId && r.Client.JMBG == clientJmbg);

            if (reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found.");
            }

            reservation.Status = "confirmed";

            await _unitOfWork.Reservations.UpdateAsync(reservation, reservation.ReservationId);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ReservationDto>(reservation);
        }

        public async Task<List<ClientDto>> GetConfirmedClientsBySessionIdAsync(int sessionId)
        {
            var confirmedReservations = _unitOfWork.Reservations
                .GetAll("Session,Session.Trainer,Session.Room,Session.TrainingProgram,Client")
                .Where(r => r.SessionId == sessionId && r.Status == "confirmed")
                .ToList();

            var clients = confirmedReservations.Select(r => r.Client).Distinct().ToList();

            return _mapper.Map<List<ClientDto>>(clients);
        }

       
    }
}
