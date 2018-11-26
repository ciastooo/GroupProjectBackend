using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupProjectBackend.Models.Dto
{
    public class RatingModelDto
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public bool IsFavourite { get; set; }
        
        public int UserRating { get; set; }
        
        public int Comment { get; set; }

        [Required]
        public virtual ICollection<UserLoginModelDto> User { get; set; }

        public virtual ICollection<PlaceModelDto> Place { get; set; }

        public virtual ICollection<RouteModelDto> Route { get; set; }
    }
}
