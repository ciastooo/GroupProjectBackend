using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupProjectBackend.Models.DB
{
    public class Place
    {
        public Place()
        {
            UserRatings = new HashSet<Rating>();
            RoutePlaces = new HashSet<RoutePlace>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
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

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<Rating> UserRatings { get; set; }

        public virtual ICollection<RoutePlace> RoutePlaces { get; set; }
    }
}
