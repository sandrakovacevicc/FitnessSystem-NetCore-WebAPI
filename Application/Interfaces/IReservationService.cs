using FitnessSystem.Application.DTOs.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.Interfaces
{
    public interface IReservationService
    {
        Task<List<ReservationDto>> GetAllAsync(string filterBy = null,string filterValue = null,string sortBy = null,bool ascending = true,int pageNumber = 1,int pageSize = 10);
        Task<ReservationDto> GetByIdAsync(int id);
        Task<ReservationAddDto> CreateReservationAsync(ReservationAddDto reservationAddDto);
        Task<ReservationDeleteDto> DeleteReservationAsync(int id);
        Task<ReservationDto> UpdateReservationAsync(int id, ReservationUpdateDto reservationUpdateDto);
        Task<List<ReservationDto>> GetReservationsByClientJmbgAsync(string clientJmbg);
    }
}
