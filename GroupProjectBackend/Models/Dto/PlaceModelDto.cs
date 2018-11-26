using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupProjectBackend.Models.Dto
{
    public class PlaceModelDto
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string FullAddress { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public int Longtitude { get; set; }

        [Required]
        public int Lattitude { get; set; }

        public float AverageRating { get; set; }
        
        public virtual ICollection<CategoryModelDto> Category { get; set; }

        public virtual RatingModelDto UserRating { get; set; }

        public virtual RouteModelDto RouteStart { get; set; }
    }
}
