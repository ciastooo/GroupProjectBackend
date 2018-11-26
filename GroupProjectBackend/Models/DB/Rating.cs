using System.ComponentModel.DataAnnotations;

namespace GroupProjectBackend.Models.DB
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RouteId { get; set; }
        
        public bool IsFavourite { get; set; }
        
        public int UserRating { get; set; }
        
        [MaxLength(255)]
        public string Comment { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Route Route { get; set; }

    }
}
