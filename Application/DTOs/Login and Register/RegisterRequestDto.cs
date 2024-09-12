using System.ComponentModel.DataAnnotations;

namespace FitnessSystem.Application.DTOs.Login
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string JMBG { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public string MobileNumber { get; set; }
        [Required]
        public int MembershipPackageId { get; set; }
        public bool IsPaid = false;

        public string[] Roles { get; set; }
    }
}
