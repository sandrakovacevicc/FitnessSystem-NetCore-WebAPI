using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessSystem.Application.DTOs.User;

namespace FitnessSystem.Application.DTOs.Trainer
{
    public class TrainerDeleteDto : UserDeleteDto
    {
        public string Specialty { get; set; }
    }
}
