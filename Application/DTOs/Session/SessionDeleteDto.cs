using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.DTOs.Session
{
    public class SessionDeleteDto
    {
        public int SessionId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int Duration { get; set; }
        public int Capacity { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
        public Trainer Trainer { get; set; }
        public Room Room { get; set; }
    }
}
