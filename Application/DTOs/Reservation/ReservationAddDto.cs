using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.DTOs.Reservation
{
    public class ReservationAddDto
    {
        [Required]
        public string ClientJMBG { get; set; }
        [Required]
        public int SessionId { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
