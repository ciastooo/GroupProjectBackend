using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupProjectBackend.Models.DB
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        
        public int? RouteId { get; set; }

        public int? PlaceId { get; set; }

        public bool IsAddedByThisUser { get; set; }

        public bool IsFavourite { get; set; }
        
        public int UserRating { get; set; }
        
        [MaxLength(255)]
        public string Comment { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("RouteId")]
        public virtual Route Route { get; set; }

        [ForeignKey("PlaceId")]
        public virtual Place Place { get; set; }

    }
}
