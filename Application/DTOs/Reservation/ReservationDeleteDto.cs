using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.DTOs.Reservation
{
    public class ReservationDeleteDto
    {
        public int ReservationId { get; set; }
        public string ClientJMBG { get; set; }
        public int SessionId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Status { get; set; }
    }
}
