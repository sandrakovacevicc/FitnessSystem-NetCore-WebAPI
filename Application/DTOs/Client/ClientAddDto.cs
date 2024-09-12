using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessSystem.Application.DTOs.User;

namespace FitnessSystem.Application.DTOs.Client
{
    public class ClientAddDto : UserDto
    {
        public int MembershipPackageId { get; set; }
        public DateTime Birthdate { get; set; }
        public string MobileNumber { get; set; }
        public bool? IsPaid { get; set; }
    }
}
