using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.DTOs
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
