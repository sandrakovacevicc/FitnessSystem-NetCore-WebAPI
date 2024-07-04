using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.DTOs
{
    public class SessionDto
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int Duration { get; set; }
        public int Capacity { get; set; }
        public TrainingProgramDto TrainingProgram { get; set; }
        public TrainerDto Trainer { get; set; }
        public RoomDto Room { get; set; }
        
    }
}
