using Core.Entities;
using FitnessSystem.Application.DTOs.Client;
using FitnessSystem.Application.DTOs.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.DTOs.Reservation
{
    public class ReservationDto
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Status { get; set; }
        public ClientDto Client { get; set; }
        public SessionDto Session { get; set; }
    }
}
