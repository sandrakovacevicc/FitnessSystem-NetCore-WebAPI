using Core.Entities;
using FitnessSystem.Application.DTOs.MembershipPackage;
using FitnessSystem.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessSystem.Application.DTOs.Client
{
    public class ClientDto : UserDto
    {

        public MembershipPackageDto? MembershipPackage { get; set; }
        public DateTime Birthdate { get; set; }
        public string? MobileNumber { get; set; }

    }

}
