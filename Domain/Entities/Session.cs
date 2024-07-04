using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Session
    {
        public int SessionId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int Duration { get; set; }
        public int Capacity { get; set; }
        public int TrainingProgramId { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public List<Client> Clients { get; set; }

    }
}
