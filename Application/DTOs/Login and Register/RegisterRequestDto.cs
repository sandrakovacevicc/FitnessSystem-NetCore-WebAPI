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

        public string[] Roles { get; set; }
    }
}
