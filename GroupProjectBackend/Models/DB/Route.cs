using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupProjectBackend.Models.DB
{
    public class Route
    {
        public Route()
        {
            RoutePlaces = new HashSet<RoutePlace>();
            UserRatings = new HashSet<Rating>();
        }

        [Required]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string UserId { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public virtual ICollection<RoutePlace> RoutePlaces { get; set; }

        public virtual ICollection<Rating> UserRatings { get; set; }

    }
}
