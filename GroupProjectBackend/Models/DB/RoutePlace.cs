using System.ComponentModel.DataAnnotations;

namespace GroupProjectBackend.Models.DB
{
    public class RoutePlace
    {
        [Key]
        public int Id { get; set; }

        public int RouteId { get; set; }

        public int Order { get; set; }

        [Required]
        public float Longitude { get; set; }

        [Required]
        public float Latitude { get; set; }

        public virtual Route Route { get; set; }
    }
}
