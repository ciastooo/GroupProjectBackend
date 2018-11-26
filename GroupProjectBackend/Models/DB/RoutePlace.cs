using System.ComponentModel.DataAnnotations;

namespace GroupProjectBackend.Models.DB
{
    public class RoutePlace
    {
        [Key]
        public int Id { get; set; }

        public int RouteId { get; set; }

        public int FromPlaceId { get; set; }

        public int ToPlaceId { get; set; }

        public int Order { get; set; }

        public virtual Route Route { get; set; }

        public virtual Place FromPlace { get; set; }

        public virtual Place ToPlace { get; set; }
    }
}
