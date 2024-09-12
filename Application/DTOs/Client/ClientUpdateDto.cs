using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessSystem.Application.DTOs.User;

namespace FitnessSystem.Application.DTOs.Client
{
    public class ClientUpdateDto : UserUpdateDto
    {
        [Required]
        public int MembershipPackageId { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        [Phone]
        public string MobileNumber { get; set; }
        [Required]
        public bool? IsPaid { get; set; }
    }
}
