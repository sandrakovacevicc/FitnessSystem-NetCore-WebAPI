using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.DTOs.Session
{
    public class SessionUpdateDto
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        [MinLength(1)]
        public int Capacity { get; set; }
        [Required]
        public int TrainingProgramId { get; set; }
        [Required]
        public string TrainerJMBG { get; set; }
        [Required]
        public int RoomId { get; set; }
    }
}
