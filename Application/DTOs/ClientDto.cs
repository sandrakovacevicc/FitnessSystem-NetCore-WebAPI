using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.DTOs
{
    public class ClientDto : UserDto
    {
        
        public MembershipPackage MembershipPackage { get; set; }
        public List<ReservationDto> Reservations { get; set; }
    }
}
