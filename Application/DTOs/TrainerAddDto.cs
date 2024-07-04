using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.DTOs
{
    public class TrainerAddDto : UserDto
    {
        public string Specialty { get; set; }
    }
}
