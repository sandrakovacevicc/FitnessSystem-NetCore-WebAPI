using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessSystem.Application.DTOs.User;

namespace FitnessSystem.Application.DTOs.Trainer
{
    public class TrainerAddDto : UserDto
    {
        public string Specialty { get; set; }
    }
}
