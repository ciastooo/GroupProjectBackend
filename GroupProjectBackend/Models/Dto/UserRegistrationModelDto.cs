using System.ComponentModel.DataAnnotations;

namespace GroupProjectBackend.Models.Dto
{
    public class UserRegistrationModelDto
    {
        public string Ig { get; set; }

        [Required]
        [MaxLength(255)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
