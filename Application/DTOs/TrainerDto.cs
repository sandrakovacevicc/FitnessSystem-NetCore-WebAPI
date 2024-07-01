using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.DTOs
{
    public class TrainerDto : UserDto
    {
        public string Specialty { get; set; }
        public List<Session> Sessions { get; set; }
    }
}
