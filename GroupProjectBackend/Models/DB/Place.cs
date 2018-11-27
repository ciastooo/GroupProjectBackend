using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupProjectBackend.Models.DB
{
    public class Place
    {
        public Place()
        {
            UserRatings = new HashSet<Rating>();
            ToRoutePlaces = new HashSet<RoutePlace>();
            FromRoutePlaces = new HashSet<RoutePlace>();
        }

        [Required]
        public int Id { get; set; }

        public int CategoryId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(255)]
        public string FullAddress { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(255)]
        public bool IsPublic { get; set; }

        [Required]
        public float Longitude { get; set; }

        [Required]
        public float Latitude { get; set; }

        public float AverageRating { get; set; }
        
        public virtual Category Category { get; set; }

        public virtual ICollection<Rating> UserRatings { get; set; }

        public virtual ICollection<RoutePlace> ToRoutePlaces { get; set; }

        public virtual ICollection<RoutePlace> FromRoutePlaces { get; set; }
    }
}
