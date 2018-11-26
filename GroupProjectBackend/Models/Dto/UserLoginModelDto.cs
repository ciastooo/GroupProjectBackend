using System.ComponentModel.DataAnnotations;

namespace GroupProjectBackend.Models.Dto
{
    public class UserLoginModelDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual RatingModelDto UserRating { get; set; }
    }
}
