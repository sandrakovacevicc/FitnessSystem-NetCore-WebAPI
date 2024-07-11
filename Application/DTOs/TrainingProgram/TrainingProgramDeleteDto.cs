using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.DTOs.TrainingProgram
{
    public class TrainingProgramDeleteDto
    {
        public int TrainingProgramId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TrainingDurationInMinutes { get; set; }
        public string TrainingType { get; set; }
    }
}
