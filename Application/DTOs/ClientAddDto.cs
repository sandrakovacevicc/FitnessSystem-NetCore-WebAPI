using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.DTOs
{
    public class ClientAddDto :UserDto
    {
        public int MembershipPackageId { get; set; }
        public DateTime Birthdate { get; set; }
        public string MobileNumber { get; set; }
    }
}
