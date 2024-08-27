using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FitnessSystem.Application.DTOs.TrainingProgram
{
    public class TrainingProgramDto
    {
        [Required]
        public int TrainingProgramId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int TrainingDurationInMinutes { get; set; }
        [Required]
        public string TrainingType { get; set; }
        //public List<Session> Sessions { get; set; }
    }
}
